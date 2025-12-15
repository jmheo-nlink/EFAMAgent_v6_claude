using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WFASCGetFileTypeHelper
{
    [Guid("42033931-1FD4-486A-916C-6FA1521CD0D3"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    interface ISCGetGrade // cannot list any base interfaces here 
    {        
        [DispId(1)]
        // [propput, id(1), helpstring("property FileName")] HRESULT FileName([in] BSTR newVal);
        void FileName([In, MarshalAs(UnmanagedType.BStr)] string Filename);

        [DispId(2)]
        // [propget, id(2), helpstring("property Grade")] HRESULT Grade([out, retval] short *pVal);
		// [propput, id(2), helpstring("property Grade")] HRESULT Grade([in] short newVal);
        Int16 Grade { get; set;}

        [DispId(3)]
        // [id(3), helpstring("method Run")] HRESULT Run();
        void Run();

        [DispId(4)]
        // [id(4), helpstring("method GetFileType")] HRESULT GetFileType(int *access);
        //int GetFileType([Out] out int access);
        void GetFileType([Out] out int access);

        [DispId(5)]
        // [propput, id(5), helpstring("property NewFileName")] HRESULT NewFileName([in] BSTR newVal);
        void NewFileName([In, MarshalAs(UnmanagedType.BStr)] string Filename);

        [DispId(6)]
        // [propget, id(6), helpstring("property IsReadOnly")] HRESULT IsReadOnly([out, retval] short *pVal);
        Int16 IsReadOnly { get; }

        [DispId(7)]
        // [propget, id(7), helpstring("property IsSom")] HRESULT IsSom([out, retval] short *pVal);
        Int16 IsSom { get; }

        [DispId(8)]
        // [propget, id(8), helpstring("property IsAuth")] HRESULT IsAuth([out, retval] short *pVal);
        Int16 IsAuth { get; }

		[DispId(9)]
        // [propget, id(9), helpstring("property UserID")] HRESULT UserID([out, retval] BSTR* pVal);
        string UserID { get; }

        [DispId(10)]
        // [propput, id(10), helpstring("property MemberID")] HRESULT MemberID([in] BSTR newVal);
		string MemberID { set; }

        [DispId(11)]
        // [propget, id(11), helpstring("property GetLoginInfo")] HRESULT GetLoginInfo([out, retval] short *pVal);
		Int16 GetLoginInfo { get; }

        [DispId(12)]
        // [propput, id(12), helpstring("property MemberPW")] HRESULT MemberPW([in] BSTR newVal);
		string MemberPW { set; }

        [DispId(13)]
        // [propget, id(13), helpstring("property GetEncodingPW")] HRESULT GetEncodingPW([out, retval] BSTR *pVal);
		string GetEncodingPW { get; }

        [DispId(14)]
        // [propget, id(14), helpstring("property ClearEditControl")] HRESULT ClearEditControl([out, retval] short *pVal);
		Int16 ClearEditControl { get; }

        [DispId(15)]
        // [propget, id(15), helpstring("property ModuleCheck")] HRESULT ModuleCheck([out, retval] short *pVal);
		Int16 ModuleCheck { get; }

        [DispId(16)]
        // [propget, id(16), helpstring("property GetAllKeyAuth")] HRESULT GetAllKeyAuth([out, retval] short *pVal);
        Int16 GetAllKeyAuth { get; }

        [DispId(17)]
        // [id(17), helpstring("method UserLogin")] HRESULT UserLogin([in]BSTR UserID, [out, retval]BOOL *bRe);
        Boolean UserLogin([In, MarshalAs(UnmanagedType.BStr)] string UserID);

        [DispId(18)]
        // [id(18), helpstring("method IsValidUser")] HRESULT IsValidUser([out, retval] BOOL *bRe);
        Boolean IsValidUser();

		[DispId(19)]
        // [id(19), helpstring("method Save")] HRESULT Save([in] BSTR path, [out, retval] BOOL *bRe);
		Boolean Save([In, MarshalAs(UnmanagedType.BStr)] string path);
        
        [DispId(20)]
        // [id(20), helpstring("method Open")] HRESULT Open([in] BSTR path, [out, retval] BOOL *bRe);
		Boolean Open([In, MarshalAs(UnmanagedType.BStr)] string path);
        
        [DispId(21)]
        // [id(21), helpstring("method PrintAuth")] HRESULT PrintAuth([out, retval] BOOL *bRe);
		Boolean PrintAuth();
        
        [DispId(22)]
        // [id(22), helpstring("method PrintLog")] HRESULT PrintLog([in] BSTR data, [out, retval] BOOL *bRe);
		Boolean PrintLog([In, MarshalAs(UnmanagedType.BStr)] string data);

        [DispId(23)]
        // [id(23), helpstring("method StartDown")] HRESULT StartDown([in] BSTR KeyUrl, [out, retval] BOOL *bRe);
		Boolean StartDown([In, MarshalAs(UnmanagedType.BStr)] string KeyUrl);

        [DispId(24)]
        // [id(24), helpstring("method SystemLogin")] HRESULT SystemLogin([in]int Type, BSTR UserID, [out, retval] BOOL *bRe);
		Boolean SystemLogin([In, MarshalAs(UnmanagedType.I4)] int Type, [In, MarshalAs(UnmanagedType.BStr)] string UserID);

        [DispId(25)]
        // [id(25), helpstring("method MatchDocID")] HRESULT MatchDocID([in] BSTR UserID, [out, retval] BOOL *bRe);
		Boolean MatchDocID([In, MarshalAs(UnmanagedType.BStr)] string UserID);

        [DispId(26)]
        // [id(26), helpstring("method SytemValidUser")] HRESULT SytemValidUser([in]int Type, [out, retval] BOOL *bRe);
		Boolean SytemValidUser([In, MarshalAs(UnmanagedType.I4)] int Type);

        [DispId(27)]
        // [id(27), helpstring("method SystemSave")] HRESULT SystemSave([in] int Type, BSTR path, [out, retval] BOOL *bRe);
		Boolean SystemSave([In, MarshalAs(UnmanagedType.I4)] int Type, [In, MarshalAs(UnmanagedType.BStr)] string path);

        [DispId(28)]
        // [id(28), helpstring("method SystemOpen")] HRESULT SystemOpen([in] int Type, BSTR path, [out, retval] BOOL *bRe);
		Boolean SystemOpen([In, MarshalAs(UnmanagedType.I4)] int Type, [In, MarshalAs(UnmanagedType.BStr)] string path);

        [DispId(29)]
        // [id(29), helpstring("method SystemPrintAuth")] HRESULT SystemPrintAuth([in] int Type, [out, retval] BOOL *bRe);
        Boolean SystemPrintAuth([In, MarshalAs(UnmanagedType.I4)] int Type);

        [DispId(30)]
        // [id(30), helpstring("method SystemPrintLog")] HRESULT SystemPrintLog([in] int Type, BSTR data, [out, retval] BOOL *bRe);
		Boolean SystemPrintLog([In, MarshalAs(UnmanagedType.I4)] int Type, [In, MarshalAs(UnmanagedType.BStr)] string data);

        [DispId(31)]
        // [id(31), helpstring("method UserLogin2")] HRESULT UserLogin2([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR UserID, [in]BSTR Reserved, [out, retval] BOOL *bRe  );
		Boolean UserLogin2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string UserID, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(32)]
        // [id(32), helpstring("method IsValidUser2")] HRESULT IsValidUser2([in]BSTR ServerID, [in]BSTR CategoryID,  [in]BSTR Reserved, [out, retval] BOOL *bRe);
		Boolean IsValidUser2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(33)]
        // [id(33), helpstring("method Save2")] HRESULT Save2([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR path, [in]BSTR Reserved, [out, retval] BOOL *bRe );
		Boolean Save2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string path, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(34)]
        // [id(34), helpstring("method Open2")] HRESULT Open2([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR path, [in]BSTR Reserved, [out, retval] BOOL *bRe );
		Boolean Open2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string path, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(35)]
        // [id(35), helpstring("method PrintAuth2")] HRESULT PrintAuth2([in]BSTR ServerID, [in]BSTR CategoryID,  [in]BSTR Reserved, [out, retval] BOOL *bRe);
		Boolean PrintAuth2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(36)]
        // [id(36), helpstring("method PrintLog2")] HRESULT PrintLog2([in]BSTR ServerID, [in]BSTR CategoryID, [in] BSTR data, [in]BSTR Reserved, [out, retval] BOOL *bRe);
		Boolean PrintLog2([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string data, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(37)]
        // [id(37), helpstring("method IsAuthority")] HRESULT IsAuthority([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR item, [in]BSTR Reserved, [out, retval]BOOL *bRe);
		Boolean IsAuthority([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string item, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(38)]
        // [id(38), helpstring("method SendLog")] HRESULT SendLog([in] BSTR LogInfo, [in] BSTR Reserved, [out, retval]BOOL *bRe);
		Boolean SendLog([In, MarshalAs(UnmanagedType.BStr)] string LogInfo, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(39)]
        // [id(39), helpstring("method IMG_Control_Func")] HRESULT IMG_Control_Func([in] int Msg_ID, [in] BSTR szPath, [out, retval] BOOL *bRe);
		Boolean IMG_Control_Func([In, MarshalAs(UnmanagedType.BStr)] string Msg_ID, [In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(40)]
        // [id(40), helpstring("method GetUserIDAndGrpHierarchy")] HRESULT GetUserIDAndGrpHierarchy([out] BSTR *UserID, [out] BSTR *Hierarchy, [out, retval] BOOL *bRe);
		Boolean GetUserIDAndGrpHierarchy([Out, MarshalAs(UnmanagedType.BStr)] string UserID, [Out, MarshalAs(UnmanagedType.BStr)] string Hierarchy);

        [DispId(41)]
        // [id(41), helpstring("method GetUserID")] HRESULT GetUserID([out, retval] BSTR *UserID);
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetUserID();

        [DispId(42)]
        // [id(42), helpstring("method GetGrpHierarchy")] HRESULT GetGrpHierarchy([out, retval] BSTR *Hierarchy);
		[return: MarshalAs(UnmanagedType.BStr)]
        string GetGrpHierarchy();

        [DispId(43)]
        // [id(43), helpstring("method GetGradeName")] HRESULT GetGradeName([in] BSTR szPath, [out, retval] BSTR *GradeName);
		[return: MarshalAs(UnmanagedType.BStr)]
        string GetGradeName([In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(44)]
        // [id(44), helpstring("method IsSecuFileFromServer")] HRESULT IsSecuFileFromServer([in] BSTR szPath, [out, retval] BOOL *bRe);
		Boolean IsSecuFileFromServer([In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(45)]
        // [id(45), helpstring("method DeleteTempFile")] HRESULT DeleteTempFile([in] BSTR path, [out, retval] BOOL *bRe);
		Boolean DeleteTempFile([In, MarshalAs(UnmanagedType.BStr)] string path);

        [DispId(46)]
        // [id(46), helpstring("method GetGradeID")] HRESULT GetGradeID([in] BSTR szPath, [out, retval] BSTR* GradeID);
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetGradeID([In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(47)]
        // [id(47), helpstring("method GetClassCategoryInfoList")] HRESULT GetClassCategoryInfoList([in] BSTR userid, [out, retval] BSTR* cidList);
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetClassCategoryInfoList([In, MarshalAs(UnmanagedType.BStr)] string userid);

        [DispId(48)]
        // [id(48), helpstring("method GetPrivateCategoryInfoList")] HRESULT GetPrivateCategoryInfoList([in] BSTR userid, [out, retval] BSTR* cidList);
        [return: MarshalAs(UnmanagedType.BStr)]
		string GetPrivateCategoryInfoList([In, MarshalAs(UnmanagedType.BStr)] string userid);

        [DispId(49)]
        // [id(49), helpstring("method GetClassCategoryAuth")] HRESULT GetClassCategoryAuth([in] BSTR cid, [out, retval] BSTR* cAuth);
        [return: MarshalAs(UnmanagedType.BStr)]
		string GetClassCategoryAuth([In, MarshalAs(UnmanagedType.BStr)] string cid);

        [DispId(50)]
        // [id(50), helpstring("method GetPrivateCategoryAuth")] HRESULT GetPrivateCategoryAuth([in] BSTR cid, [out, retval] BSTR* cAuth);
        [return: MarshalAs(UnmanagedType.BStr)]
		string GetPrivateCategoryAuth([In, MarshalAs(UnmanagedType.BStr)] string cid);

        [DispId(51)]
        // [id(51), helpstring("method GetCategoryName")] HRESULT GetCategoryName([in] BSTR cid, [out, retval] BSTR* cName);
        [return: MarshalAs(UnmanagedType.BStr)]
		string GetCategoryName([In, MarshalAs(UnmanagedType.BStr)] string cid);

        [DispId(52)]
        // [id(52), helpstring("method GetCategoryInfoFromPath")] HRESULT GetCategoryInfoFromPath([in] BSTR path, [out, retval] BSTR* cid);
        [return: MarshalAs(UnmanagedType.BStr)]
		string GetCategoryInfoFromPath([In, MarshalAs(UnmanagedType.BStr)] string path);

        [DispId(53)]
        // [id(53), helpstring("method IsTopGroupDACSecuFile")] HRESULT IsTopGroupDACSecuFile([in] BSTR szPath, [out, retval] BOOL*  bRe);
        Boolean IsTopGroupDACSecuFile([In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(54)]
        // [propget, id(54), helpstring("property PCID")] HRESULT PCID([out, retval] BSTR* pVal);
        string PCID { get; }
        
        [DispId(55)]
        // [id(55), helpstring("method CheckLoginUser")] HRESULT CheckLoginUser([in]BSTR szUserID, [in]BSTR szUserPW, [out, retval] BOOL *bRe);
		Boolean CheckLoginUser([In, MarshalAs(UnmanagedType.BStr)] string szUserID, [In, MarshalAs(UnmanagedType.BStr)] string szUserPW);

        [DispId(56)]
        // [id(56), helpstring("method Save3")] HRESULT Save3([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR path, [in]BSTR orgPath, [in]BOOL bOpenDoc, [in]BOOL bShowFileDialog, [in]BSTR Reserved, [out, retval] BOOL *bRe);
		Boolean Save3([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string path, [In, MarshalAs(UnmanagedType.BStr)] string orgPath, [In, MarshalAs(UnmanagedType.Bool)] Boolean bOpenDoc, [In, MarshalAs(UnmanagedType.Bool)] Boolean bShowFileDialog, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(57)]
        // [id(57), helpstring("method PrintLog3")] HRESULT PrintLog3([in]BSTR ServerID, [in]BSTR CategoryID, [in] BSTR data, [in] BSTR data2, [in]BSTR Reserved, [out, retval] BOOL *bRe);
		Boolean PrintLog3([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string data, [In, MarshalAs(UnmanagedType.BStr)] string data2, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);

        [DispId(58)]
        // [id(58), helpstring("method Save4")] HRESULT Save4([in]BSTR ServerID, [in]BSTR CategoryID, [in]BSTR path, [in]BSTR orgPath, [in]BOOL bOpenDoc, [in]BOOL bShowFileDialog, [in]BSTR defaultDestPath, [in]BSTR Reserved, [out, retval] BOOL *bRe);
        Boolean Save4([In, MarshalAs(UnmanagedType.BStr)] string ServerID, [In, MarshalAs(UnmanagedType.BStr)] string CategoryID, [In, MarshalAs(UnmanagedType.BStr)] string path, [In, MarshalAs(UnmanagedType.BStr)] string orgPath, [In, MarshalAs(UnmanagedType.Bool)] Boolean bOpenDoc, [In, MarshalAs(UnmanagedType.Bool)] Boolean bShowFileDialog, [In, MarshalAs(UnmanagedType.BStr)] string defaultDestPath, [In, MarshalAs(UnmanagedType.BStr)] string Reserved);
        
        [DispId(59)]
        // [propget, id(59), helpstring("property OPT_PC")] HRESULT OPT_PC([out, retval] short* pVal);
        Int16 OPT_PC { get; }

        [DispId(60)]
        // [propget, id(60), helpstring("property LoginMode")] HRESULT DSStatus([out, retval] short* pVal);
        Int16 DSStatus { get; }

        [DispId(61)]
        // [id(61), helpstring("method IsValidUserInfo")] HRESULT IsValidUserInfo([in]short nType, [in]BSTR szUserID, [in]BSTR szUserPW, [in]BSTR szServerID, [out, retval]short *pVal);
		[return: MarshalAs(UnmanagedType.I2)]
        Int16 IsValidUserInfo( [In, MarshalAs(UnmanagedType.I2)] Int16 nType, [In, MarshalAs(UnmanagedType.BStr)] string szUserID, [In, MarshalAs(UnmanagedType.BStr)] string szUserPW, [In, MarshalAs(UnmanagedType.BStr)] string szServerID);

        [DispId(62)]
        // [id(62), helpstring("method IsEncryptFile")] HRESULT IsEncryptFile([in]BSTR szPath, [out, retval]short *pVal);
        [return: MarshalAs(UnmanagedType.I2)]
        Int16 IsEncryptFile([In, MarshalAs(UnmanagedType.BStr)] string szPath);

        [DispId(63)]
        // [propget, id(63), helpstring("property AuthInfo")] HRESULT AuthInfo([out, retval] BSTR *pVal);
        string AuthInfo { get; }

        [DispId(64)]
        // [id(64), helpstring("method WriteAutoLoginInfo")] HRESULT WriteAutoLoginInfo([in] BSTR UserID, [in] BSTR UserPW, [in] BOOL bIDCheckByPass, [out, retval] BOOL *bRe);
		Boolean WriteAutoLoginInfo([In, MarshalAs(UnmanagedType.BStr)] string UserID, [In, MarshalAs(UnmanagedType.BStr)] string UserPW, [In, MarshalAs(UnmanagedType.Bool)] Boolean bIDCheckByPass);

        [DispId(65)]
        // [id(65), helpstring("method UpdateFileVFIHashValue")] HRESULT UpdateFileVFIHashValue([in] BSTR path, [out, retval] BOOL *bRe);
		Boolean UpdateFileVFIHashValue([In, MarshalAs(UnmanagedType.BStr)] string path);

        [DispId(66)]
        // [id(66), helpstring("method GetWriterIDFromPath")] HRESULT GetWriterIDFromPath([in] BSTR path, [out, retval] BSTR* bstrUserID);
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetWriterIDFromPath([In, MarshalAs(UnmanagedType.BStr)] string path);
    }

    [ComImport, Guid("95DE89E9-ED44-4C87-A57B-C76FC42532EA")]
    class SCGetGrade
    {

    }
}
