using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
#if (!NET20 && !NET30)
using System.Linq;
#endif
using Link.Core.IO;

namespace Link.EFAM.Security
{
    using Link.EFAM.Security.AccessControl;
    using Link.EFAM.Security.Principal;
    using AccessRuleCollectionCollection = System.Collections.Generic.Dictionary<string, Link.EFAM.Security.AccessManager.AccessRuleCollection>;
    using Debug = System.Diagnostics.Debug;
    using DirectorySecurityCollection = System.Collections.Generic.Dictionary<string, Link.EFAM.Security.AccessControl.DirectorySecurity>;
    using ICollection = System.Collections.ICollection;

    public sealed class AccessManager
    {
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Engine Module");

        private AccessRuleCollectionCollection m_accRulesColl = null;
        private AccessControlPolicy m_currentPolicy = null;
        private Object m_syncRoot = new Object();
        private Dictionary<string, string> m_fixedDirDic = null;
        private Dictionary<string, string> m_nbNameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #region 속성

        /// <summary>
        /// 액세스 제어 정책을 가져온다.
        /// </summary>
        /// <value><see cref="AccessControlPolicy"/> 개체</value>
        public AccessControlPolicy Policy
        {
            get { return m_currentPolicy; }
        }

        public Dictionary<string, string> NetBIOSNameMapping
        {
            get { return m_nbNameMapping; }
        }

        /// <summary>
        /// 고정 디렉토리 목록을 가져오거나 설정한다.
        /// </summary>
        /// <value>고정 디렉토리 목록</value>
        public IEnumerable<string> FixedDirectories
        {
            get { return m_fixedDirDic.Keys; }
            set
            {
                Dictionary<string, string> dirDic 
                    = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                if (value != null)
                {
                    foreach (string path in value)
                    {
                        dirDic[path] = "";

                        if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Trace] set_FixedDirectories() # fixed directory: " + path);
                    }
                } // if (value != null)

                m_fixedDirDic = dirDic;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="AccessManager"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private AccessManager()
        {
            m_accRulesColl = new AccessRuleCollectionCollection(StringComparer.OrdinalIgnoreCase);
            m_currentPolicy = new AccessControlPolicy();
            m_fixedDirDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region 정적 메소드
        #region Singleton 인스턴스

        private static AccessManager m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="AccessManager"/> 인스턴스를 반환한다.
        /// </summary>
        /// <returns>캐시된 <see cref="AccessManager"/> 개체</returns>
        public static AccessManager GetManager()
        {
            if (m_instance == null) m_instance = new AccessManager();

            return m_instance;
        }

        #endregion
        #endregion

        //public void SetFixedDirectories(IEnumerable<string> paths)
        //{
        //    if (paths == null) throw new ArgumentNullException("paths");

        //    lock (((ICollection)m_fixedDirDic).SyncRoot)
        //    {
        //        m_fixedDirDic.Clear();

        //        foreach (string path in paths)
        //        {
        //            m_fixedDirDic[path] = "";

        //            if (m_tracing.Enabled)
        //            {
        //                Trace.WriteLine("[EFAM.Trace] AccessManager # directory: " + path);
        //            }
        //        } // foreach ( string )
        //    } // lock
        //}

        private string ReplaceNBNameToIPAddr(string path)
        {
            int foundIndex = path.IndexOf('\\', 2);
            string serverName = path.Substring(0, foundIndex);

            if (serverName.IndexOf('.') == -1)
            {
                string ipAddr;

                if (m_nbNameMapping.TryGetValue(serverName, out ipAddr))
                {
                    path = ipAddr + path.Substring(foundIndex);
                }
            }

            return path;
        }

        /// <summary>
        /// 지정한 디렉토리가 고정 디렉토리인지 여부를 확인한다.
        /// </summary>
        /// <param name="path">확인할 디렉토리의 경로</param>
        /// <returns>지정한 디렉토리가 고정 디렉토리이면 <b>true</b>, 그렇지 않으면 <b>false</b></returns>
        public bool IsFixedDirectory(string path)
        {
            bool isFixed = false;

            //lock (((ICollection)m_fixedDirDic).SyncRoot)
            //{
            if (!String.IsNullOrEmpty(path))
            {
                path = ReplaceNBNameToIPAddr(path);
                isFixed = m_fixedDirDic.ContainsKey(path);
            }
            //} // lock
            if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Trace] IsFixedDirectory() # directory: " + path);

            return isFixed;
        }

        public void OnDirectoryMoved(string path, string oldPath)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (oldPath == null) throw new ArgumentNullException("oldPath");

            path = ReplaceNBNameToIPAddr(path);
            oldPath = ReplaceNBNameToIPAddr(oldPath);

            string path2 = PathHelper.AppendTerminalBackslash(path);
            string oldPath2 = PathHelper.AppendTerminalBackslash(oldPath);
            string[] fixedDirList = null;

            lock (((ICollection)m_fixedDirDic).SyncRoot)
            {
                fixedDirList = new string[m_fixedDirDic.Keys.Count];
                m_fixedDirDic.Keys.CopyTo(fixedDirList, 0);

                foreach (string fixedDir in fixedDirList)
                {
                    if (fixedDir.Equals(oldPath, StringComparison.OrdinalIgnoreCase))
                    {
                        m_fixedDirDic.Remove(fixedDir);

                        m_fixedDirDic[path] = "";
                    }
                    else if (fixedDir.StartsWith(oldPath2, StringComparison.OrdinalIgnoreCase))
                    {
                        m_fixedDirDic.Remove(fixedDir);

                        m_fixedDirDic[path2 + fixedDir.Substring(oldPath2.Length)] = "";
                    }
                } // foreach ( string )
            } // lock
        }

        // 문서가 열려 있는 경우 문서에 테마를 적용합니다.
        // 적용할 테마 이름과 테마 서식 옵션입니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="hostNames"></param>
        public bool ApplyPolicy(AccessControlPolicy policy, System.Collections.Specialized.NameValueCollection hostNames)
        {
            if (policy == null) throw new ArgumentNullException("policy");

            DirectorySecurityCollection securityColl
                = new DirectorySecurityCollection(StringComparer.OrdinalIgnoreCase);
            DirectorySecurity security = null;
            string ipAddrPrefix = null;
            string hostNamePrefix = null;

            if (EqualsPolicy(policy, m_currentPolicy)) return false;

            foreach (string path in policy.Directories)
            {
                security = policy.GetAccessControl(path);
                securityColl[path] = security;

                // 호스트 이름으로 시작하는 경로에 ACL 항목을 적용한다.
                foreach (string ipAddr in hostNames.AllKeys)
                {
                    ipAddrPrefix = "\\\\" + ipAddr + "\\";
                    if (!path.StartsWith(ipAddrPrefix)) continue;

                    foreach (string hostName in hostNames.GetValues(ipAddr))
                    {
                        hostNamePrefix = "\\\\" + hostName + "\\";

                        securityColl[path.Replace(ipAddrPrefix, hostNamePrefix)] = security;
                    }
                } // foreach ( string )
            }
            
            m_accRulesColl = RuleHelper.GetAccessRulesCollection(securityColl);
            m_currentPolicy = ClonePolicy(policy);

            return true;
        }

        private bool EqualsPolicy(AccessControlPolicy policyA, AccessControlPolicy policyB)
        {
            DirectorySecurity securityA = null;
            DirectorySecurity securityB = null;
            bool found = false;

            if (policyA.Directories.Length != policyB.Directories.Length) return false;

            foreach (string path in policyA.Directories)
            {
                securityA = policyA.GetAccessControl(path);
                securityB = policyB.GetAccessControl(path);
                if (securityB == null) return false;

                foreach (FileSystemAccessRule ruleA in securityA.GetAccessRules())
                {
                    found = false;

                    foreach (FileSystemAccessRule ruleB in securityB.GetAccessRules())
                    {
                        if (ruleA.Identity == ruleB.Identity
                            && ruleA.Rights == ruleB.Rights
                            && ruleA.NoInherit == ruleB.NoInherit
                            && ruleA.NoPropagate == ruleB.NoPropagate)
                        {
                            found = true;
                            break;
                        }
                    } // foreach ( FileSystemAccessRule )

                    if (!found) return false;
                } // foreach ( FileSystemAccessRule )
            } // foreach ( string )

            return true;
        }

        private AccessControlPolicy ClonePolicy(AccessControlPolicy policy)
        {
            AccessControlPolicy policyNew = new AccessControlPolicy();
            DirectorySecurity securityNew = null;
            FileSystemAccessRule ruleNew = null;

            foreach (string path in policy.Directories)
            {
                securityNew = new DirectorySecurity();
                policyNew.SetAccessControl(path, securityNew);

                foreach (FileSystemAccessRule rule in policy.GetAccessControl(path).GetAccessRules())
                {
                    ruleNew = new FileSystemAccessRule(rule.Identity, rule.Rights,
                                                       rule.NoInherit, rule.NoPropagate);
                    securityNew.SetAccessRule(ruleNew);
                }
            } // foreach ( string )

            return policyNew;
        }

        /*
        public bool IsAuthorized(string filePath)
        {
            return true;
        }

        /// <summary>
        /// 지정한 디렉토리에 대한 사용 권한을 가져온다.
        /// 지정한 디렉토리에 허용된 권한을 가져온다.
        /// </summary>
        /// <param name="shareName"></param>
        /// <param name="dirPath"></param>
        /// <returns>디렉토리에 대한 사용 권한을 나타내는 <see cref="FileIOPermissionAccess"/> 열거형 값의 비트 조합</returns>
        public FileIOPermissionAccess GetFileIOPermission(string shareName, string dirPath)
        {
            FileIOPermissionAccess access = FileIOPermissionAccess.AllAccess;

            return access;
        }
         */

        public FileIOPermissionAccess GetFileIOPermission(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            AccessRuleCollection rules = null;
            //FileIOPermissionAccess access = FileIOPermissionAccess.NoAccess;
            FileSystemRights rights = (FileSystemRights)0;

            Debug.WriteLine("[EFAM.Security] query permission: before normalize : " + path);
            path = NtPath.NormalizePath(path);
            Debug.WriteLine("[EFAM.Security] query permission: after normalize : " + path);

            path = ReplaceNBNameToIPAddr(path);

            lock (m_syncRoot)
            {
                if (m_accRulesColl.TryGetValue(path, out rules))
                {
                    foreach (FileSystemAccessRule rule in rules)
                    {
                        rights |= rule.Rights;
                    }
                } // if (m_accRulesColl.TryGetValue(path, out rules))
                else
                {
                    do
                    {
                        path = NtPath.GetDirectoryName(path);
                        if (String.IsNullOrEmpty(path)) break;
                    } while (!m_accRulesColl.TryGetValue(path, out rules));

                    if (rules != null)
                    {
                        foreach (FileSystemAccessRule rule in rules)
                        {
                            if (!rule.NoPropagate)
                            {
                                rights |= rule.Rights;
                            }
                        }
                    } // if (rules != null)
                } // else
            } // lock

#if DEBUG
            StringBuilder sb = new StringBuilder();

            if ((rights & FileSystemRights.Read) == FileSystemRights.Read) sb.Append("Read, ");
            if ((rights & FileSystemRights.Write) == FileSystemRights.Write) sb.Append("Write, ");
            if ((rights & FileSystemRights.CopyFiles) == FileSystemRights.CopyFiles) sb.Append("CopyFiles, ");
            if ((rights & FileSystemRights.ListDirectory) == FileSystemRights.ListDirectory) sb.Append("ListDir, ");
            if ((rights & FileSystemRights.CreateDirectories) == FileSystemRights.CreateDirectories) sb.Append("CreateDirs, ");
            if ((rights & FileSystemRights.Delete) == FileSystemRights.Delete) sb.Append("Delete, ");
            if ((rights & FileSystemRights.Rename) == FileSystemRights.Rename) sb.Append("Rename, ");
            if ((rights & FileSystemRights.Move) == FileSystemRights.Move) sb.Append("Move, ");
            Debug.WriteLine("[EFAM.Security] query permission: " + sb.ToString());
#endif // DEBUG
            return (FileIOPermissionAccess)rights;
        }

        public bool Rename(string oldPath, string newPath)
        {
            oldPath = ReplaceNBNameToIPAddr(oldPath);
            newPath = ReplaceNBNameToIPAddr(newPath);

            Dictionary<string, AccessRuleCollection> newRulesDic
                = new AccessRuleCollectionCollection(StringComparer.OrdinalIgnoreCase);
            List<string> pathList = new List<string>();
            string oldPath2 = PathHelper.AppendTerminalBackslash(oldPath);
            string newPath2 = PathHelper.AppendTerminalBackslash(newPath);
            string path = null;

            lock (m_syncRoot)
            {
                foreach (KeyValuePair<string, AccessRuleCollection> entry in m_accRulesColl)
                {
                    path = entry.Key;

                    if (path.Equals(oldPath, StringComparison.OrdinalIgnoreCase))
                    {
                        pathList.Add(path);

                        newRulesDic[newPath] = entry.Value;
                    }
                    else if (path.StartsWith(oldPath2, StringComparison.OrdinalIgnoreCase))
                    {
                        pathList.Add(path);

                        newRulesDic[newPath2 + path.Substring(oldPath2.Length)] = entry.Value;
                    }
                } // foreach ( KeyValuePair<string, AccessRuleCollection> )

                foreach (string tmpPath in pathList)
                {
                    m_accRulesColl.Remove(tmpPath);
                }

                foreach (KeyValuePair<string, AccessRuleCollection> entry in newRulesDic)
                {
                    m_accRulesColl[entry.Key] = entry.Value;
                }
            } // lock

            return (newRulesDic.Count != 0);
        }

        public bool DeleteRules(string path)
        {
            /*
            List<string> pathList = new List<string>();
            string deletedPath = path;
            string deletedPath2 = PathHelper.AppendTerminalBackslash(path);

            foreach (KeyValuePair<string, AccessRuleCollection> entry in m_accRulesColl)
            {
                path = entry.Key;

                if (path.Equals(deletedPath, StringComparison.OrdinalIgnoreCase))
                {
                    pathList.Add(path);
                }
                else if (path.StartsWith(deletedPath2, StringComparison.OrdinalIgnoreCase))
                {
                    pathList.Add(path);
                }
            } // foreach ( KeyValuePair<string, AccessRuleCollection> )

            foreach (string tmpPath in pathList)
            {
                m_accRulesColl.Remove(tmpPath);
            }
             */
            path = ReplaceNBNameToIPAddr(path);

            string[] rulePaths = null;
            string deletedPath = path;
            string deletedPath2 = PathHelper.AppendTerminalBackslash(path);
            bool modified = false;

            lock (m_syncRoot)
            {
                rulePaths = new string[m_accRulesColl.Count];
                m_accRulesColl.Keys.CopyTo(rulePaths, 0);

                foreach (string rulePath in rulePaths)
                {
                    if (rulePath.Equals(deletedPath, StringComparison.OrdinalIgnoreCase))
                    {
                        m_accRulesColl.Remove(rulePath);
                        modified = true;
                    }
                    else if (rulePath.StartsWith(deletedPath2, StringComparison.OrdinalIgnoreCase))
                    {
                        m_accRulesColl.Remove(rulePath);
                        modified = true;
                    }
                } // foreach ( string )
            } // lock

            return modified;
        }

        #region INNER 클래스
        
        internal class RuleHelper
        {
            internal static AccessRuleCollectionCollection GetAccessRulesCollection(DirectorySecurityCollection securities)
            {
                ///////////////////////////////////////////////////////////////////////////////////
                List<string> sortedPathList = new List<string>(securities.Keys);
                AccessRuleCollectionCollection newRulesColl = new AccessRuleCollectionCollection(StringComparer.OrdinalIgnoreCase);
                AccessRuleCollection newRules = null;
                AccountCollection identityColl = new AccountCollection();
                Account identity = null;
                FileSystemAccessRule ruleOfParent = null;
                FileSystemAccessRule ruleNew = null;
                FileSystemRights rights;

                // 경로를 디렉토리 레벨 순으로 정렬한다.
                sortedPathList.Sort(//new Comparison<string>(
                    delegate(string path1, string path2)
                    {
                        int path1Count = 0;
                        int path2Count = 0;

                        path1 = PathHelper.AppendTerminalBackslash(path1);
                        path2 = PathHelper.AppendTerminalBackslash(path2);
                        path1Count = PathHelper.CountDirectorySeparator(path1);
                        path2Count = PathHelper.CountDirectorySeparator(path2);

                        if (path1Count == path2Count)
                        {
                            return String.Compare(path1, path2, StringComparison.OrdinalIgnoreCase);
                        }

                        return (path1Count - path2Count);
                    }
                    );//));
#if DEBUG
                Debug.WriteLine("[EFAM.Security] sorted path list:");
                foreach (string path in sortedPathList)
                {
                    Debug.WriteLine("[EFAM.Security] " + path);
                }
#endif // DEBUG

                foreach (string path in sortedPathList)
                {
                    if (!newRulesColl.TryGetValue(path, out newRules))
                    {
                        newRules = new AccessRuleCollection();
                        newRulesColl[path] = newRules;
                    }

                    foreach (FileSystemAccessRule rule in securities[path].GetAccessRules())
                    {
                        identity = rule.Identity;
                        if (!identityColl.Contains(identity.Value))
                        {
                            identityColl.Add(identity);
                        }

                        ruleOfParent = GetParentAccessRule(newRulesColl, path, identity);
                        // 상속할 액세스 규칙이 있는 경우
                        if (ruleOfParent != null)
                        {
                            if (ruleOfParent.NoPropagate || rule.NoInherit)
                            {
                                ruleNew = new FileSystemAccessRule(identity, rule.Rights, true, rule.NoPropagate);
                            }
                            else
                            {
                                // 상위 디렉토리에서 가져온 상속할 액세스 규칙과
                                // 현재 디렉토리에 설정된 액세스 규칙을 병합한다.

                                if (rule.AccessMask == 0)
                                {
                                    ruleNew = new FileSystemAccessRule(
                                        identity, rule.Rights, true, rule.NoPropagate);
                                }
                                else
                                {
                                    rights = ruleOfParent.Rights | rule.Rights;
                                    ruleNew = new FileSystemAccessRule(
                                        identity, rights, true, rule.NoPropagate);
                                }
                            } // else
                        } // if (ruleOfParent != null)
                        // 상속할 액세스 규칙이 없는 경우
                        else
                        {
                            ruleNew = new FileSystemAccessRule(identity, rule.Rights, true, rule.NoPropagate);
                        }

                        newRules.Add(ruleNew);
                    } // foreach ( FileSystemAccessRule )

                    // 현재 디렉토리에 설정된 액세스 규칙이 없는 사용자 또는 그룹이 있으면
                    // 상위 디렉토리에서 상속할 액세스 규칙을 가져와서 설정한다.
                    foreach (Account idRef in identityColl)
                    {
                        if (newRules.Contains(idRef)) continue;

                        ruleOfParent = GetParentAccessRule(newRulesColl, path, idRef);
                        if (ruleOfParent != null && !ruleOfParent.NoPropagate)
                        {
                            newRules.Add(ruleOfParent);
                        }
                    } // foreach ( Account )
                } // foreach ( string )

#if DEBUG
                StringBuilder sb = new StringBuilder();

                Debug.WriteLine("[EFAM.Security] setted access rules by account");
                foreach (KeyValuePair<string, AccessRuleCollection> entry in newRulesColl)
                {
                    Debug.WriteLine("[EFAM.Security] " + entry.Key);
                    foreach (FileSystemAccessRule rule in entry.Value)
                    {
                        rights = rule.Rights;
                        sb.Length = 0;

                        sb.Append(rule.NoInherit ? "No Inherit, " : "Inherit, ");
                        sb.Append(rule.NoPropagate ? "No Propagate, " : "Propagate, ");
                        if ((rights & FileSystemRights.Read) == FileSystemRights.Read) sb.Append("Read, ");
                        if ((rights & FileSystemRights.Write) == FileSystemRights.Write) sb.Append("Write, ");
                        if ((rights & FileSystemRights.CopyFiles) == FileSystemRights.CopyFiles) sb.Append("CopyFiles, ");
                        if ((rights & FileSystemRights.ListDirectory) == FileSystemRights.ListDirectory) sb.Append("ListDir, ");
                        if ((rights & FileSystemRights.CreateDirectories) == FileSystemRights.CreateDirectories) sb.Append("CreateDirs, ");
                        if ((rights & FileSystemRights.Delete) == FileSystemRights.Delete) sb.Append("Delete, ");
                        if ((rights & FileSystemRights.Rename) == FileSystemRights.Rename) sb.Append("Rename, ");
                        if ((rights & FileSystemRights.Move) == FileSystemRights.Move) sb.Append("Move, ");
                        //sb.Append((rule.AccessControlType == AccessControlType.Allow ? " allowed" : " denied"));
                        Debug.WriteLine("[EFAM.Security] " + sb.ToString());
                    }
                }
#endif // DEBUG

                return newRulesColl;
            }

            private static FileSystemAccessRule GetParentAccessRule(AccessRuleCollectionCollection rulesColl, string path, Account identity)
            {
                Debug.Assert((rulesColl != null && identity != null), "rulesColl and identity cannot be null.");

                AccessRuleCollection rules = null;

                // 상위 디렉토리에 설정된 액세스 규칙들 중에 ID가 일치하는 액세스 규칙을 찾아서 반환한다.
                while (true)
                {
                    path = NtPath.GetDirectoryName(path);
                    if (String.IsNullOrEmpty(path)) break;

                    if (rulesColl.TryGetValue(path, out rules))
                    {
                        // ID가 일치하는 액세스 규칙을 찾는다.
                        if (rules.Contains(identity))
                        {
                            return rules[identity];
                        }
                    } // if (rulesColl.TryGetValue(path, out rules))
                } // while (true)

                return null;
            }

            ///////////////////////////////////////////////////////////////////////////////////
            /*
            Dictionary<string, DirectorySecurityCollection> m_store = null;
            DirectorySecurityCollection dirSecurities = null;

            m_store = new Dictionary<string, DirectorySecurityCollection>(StringComparer.OrdinalIgnoreCase);

            string shareName = null;
            string dirPath = null;
            string uncPath = null;

            foreach (KeyValuePair<string, DirectorySecurity> entry in securities)
            {
                uncPath = PathHelper.AppendTerminalBackslash(entry.Key);

                // 네트워크 공유 이름과 디렉토리 경로를 분리한다.
                shareName = NtPath.GetPathRoot(uncPath);
                dirPath = uncPath.Substring(shareName.Length);

                if (!m_store.TryGetValue(shareName, out dirSecurities))
                {
                    dirSecurities = new DirectorySecurityCollection(StringComparer.OrdinalIgnoreCase);
                    m_store[shareName] = dirSecurities;
                }

                dirSecurities[dirPath] = entry.Value;
            } // foreach ( KeyValuePair<string, DirectorySecurity> )
             */
        }

        /*
        internal sealed class RuleSet
        {
            private AccessRuleCollection m_fsRules = new AccessRuleCollection(StringComparer.OrdinalIgnoreCase);

            internal FileSystemAccessRule GetAccessRule(string path)
            {
                List<string> dirList = new List<string>();
                FileSystemAccessRule foundRule = null;

                // 현재 디렉토리 또는 상위 디렉토리에 적용된 액세스 제어 규칙을 찾는다.
                while (!m_fsRules.TryGetValue(path, out foundRule))
                {
                    dirList.Add(path);

                    path = NtPath.GetDirectoryName(path);
                    if (String.IsNullOrEmpty(path)) break;
                }

                // 찾은 액세스 제어 규칙을 액세스 제어 규칙이 없는 디렉토리들에 설정한다.
                if (foundRule != null)
                {
                    FileSystemAccessRule ruleNew = null;

                    if (foundRule.NoPropagate)
                    {
                        // 액세스 거부 규칙을 생성한다.
                        ruleNew = new FileSystemAccessRule(foundRule.IdentityReference,
                                                           FileSystemRights.FullControl,
                                                           AccessControlType.Deny);
                    } // if (foundRule.NoPropagate)
                    else ruleNew = foundRule;

                    foreach (string tmpPath in dirList)
                    {
                        m_fsRules[tmpPath] = ruleNew;
                    }
                } // if (foundRule != null)

                return foundRule;
            }
        }
         */

        internal static class PathHelper
        {
            /// <summary>
            /// 지정한 경로에 백슬래시('\') 문자를 추가한다.
            /// </summary>
            /// <param name="path">백슬래시 문자를 추가할 경로</param>
            /// <returns>백슬래시 문자가 추가된 경로</returns>
            internal static string AppendTerminalBackslash(string path)
            {
                //if (path == null) throw new ArgumentNullException("path");
                Debug.Assert(path != null, "path cannot be null.");

                if (path.Length > 0 && path[path.Length - 1] != Path.DirectorySeparatorChar)
                {
                    path += Path.DirectorySeparatorChar;
                }

                return path;
            }

            internal static string RemoveTerminalBackslash(string path)
            {
                Debug.Assert(path != null, "path cannot be null.");

                if (path.Length > 0 && path[path.Length - 1] == Path.DirectorySeparatorChar)
                {
                    path = path.Substring(0, path.Length - 1);
                }

                return path;
            }

            internal static int CountDirectorySeparator(string path)
            {
                Debug.Assert(path != null, "path cannot be null.");

                int count = 0;

                foreach (char value in path)
                {
                    if (value == Path.DirectorySeparatorChar) count++;
                }

                return count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal class AccountCollection
            : System.Collections.ObjectModel.KeyedCollection<string, Account>
        {
            #region 생성자

            /// <summary>
            /// <see cref="AccountCollection"/> 클래스의 새 인스턴스를 초기화한다.
            /// </summary>
            public AccountCollection()
            {
            }

            #endregion

            #region KeyedCollection<TKey, TItem> 멤버

            /// <summary>
            /// 지정한 요소에서 키를 추출한다.
            /// </summary>
            /// <param name="identity">키를 추출할 요소</param>
            /// <returns>지정된 요소의 키</returns>
            protected override string GetKeyForItem(Account identity)
            {
                if (identity == null) throw new ArgumentNullException("identity");

                return identity.Value;
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        internal class AccessRuleCollection
            : System.Collections.ObjectModel.KeyedCollection<Account, FileSystemAccessRule>
        {
            #region 생성자

            /// <summary>
            /// <see cref="AccessRuleCollection"/> 클래스의 새 인스턴스를 초기화한다.
            /// </summary>
            public AccessRuleCollection()
            {
            }

            #endregion

            #region KeyedCollection<TKey, TItem> 멤버

            /// <summary>
            /// 지정한 요소에서 키를 추출한다.
            /// </summary>
            /// <param name="rule">키를 추출할 요소</param>
            /// <returns>지정된 요소의 키</returns>
            protected override Account GetKeyForItem(FileSystemAccessRule rule)
            {
                if (rule == null) throw new ArgumentNullException("rule");

                return rule.Identity;
            }

            #endregion
        }

        #endregion
    }
}
