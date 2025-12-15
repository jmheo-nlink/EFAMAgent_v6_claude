/**************************************************************************************************

Copyright (c) Link Inc. All rights reserved.

Moudle Name :
    FileAccCtrl.h

Abstract :
    응용 프로그램(사용자 모드)과 미니필터 드라이버(커널 모드) 사이에 공유된
    구조체, 타입 정의, 상수 등을 포함하고 있다.

Author :
    Link mskoo (2010. 6. 30)

Environment :
    Kernel & User Mode

**************************************************************************************************/

/**************************************************************************************************
 Date           Name            Description of Change
---------------------------------------------------------------------------------------------------
 2010-06-30     mskoo           최초 작성.

 2011-09-14     mskoo           v2.0 버전 릴리즈. (변경 이력 정리)
**************************************************************************************************/

#ifndef __FILEACCCTRL_H__
#define __FILEACCCTRL_H__

//
// 파일(디렉토리)에 대한 액세스 권한
//
typedef ULONG FAC_ACCESS_MASK;

#define FAC_RIGHTS_NONE             0x00000000
#define FAC_RIGHTS_READ             0x00000001
#define FAC_RIGHTS_WRITE            0x00000002
#define FAC_RIGHTS_COPY_FILES       0x00000004
#define FAC_RIGHTS_LIST_DIR         0x00000010
#define FAC_RIGHTS_CREATE_DIRS      0x00000020
#define FAC_RIGHTS_DELETE           0x00000100
#define FAC_RIGHTS_RENAME           0x00000200
#define FAC_RIGHTS_MOVE             0X00000400
#define FAC_RIGHTS_ALL              0x0000FFFF
#define FAC_RIGHTS_UNSET            0xF0000000


//
// 파일(디렉토리)에 대해 수행된 작업
//
typedef ULONG FAC_ACTION_TYPE; //FAC_OPERATION_TYPE

#define FAC_FILE_OPENED         0x00000001      // 파일
#define FAC_FILE_MODIFIED       0x00000002      // 파일
#define FAC_FILE_CREATED        0x00000003      // 파일, 디렉토리
#define FAC_FILE_DELETED        0x00000004      // 파일, 디렉토리
#define FAC_FILE_RENAMED        0x00000005      // 파일, 디렉토리
#define FAC_FILE_MOVED          0x00000006      // 파일, 디렉토리
#define FAC_FILE_COPIED         0x00000007      // 파일 
                                                // (to 로컬 디스크, 관리하지 않는 네트워크 공유)
#define FAC_FILE_READING        0x00010000
#define FAC_FILE_WRITING        0x00020000


/// <summary>
/// 프로세스의 종류를 지정한다.
/// </summary>
typedef enum _FAC_PROCESS_KIND {
    FAC_NORMAL_PROCESS = 0,
    FAC_SYSTEM_PROCESS,         // 시스템 프로세스
    FAC_WINDOWS_EXPLORER,       // "Windows 탐색기" 프로세스
    FAC_WINDOWS_COMMAND,        // "Windows 명령 처리기" 프로세스
    FAC_MS_OFFICE,              // "Microsoft Office" 응용 프로그램의 프로세스 (Excel, PowerPoint, Word)
    FAC_ANTI_VIRUS,             // 백신 응용 프로그램의 프로세스
} FAC_PROCESS_KIND, *PFAC_PROCESS_KIND;

//=================================================================================================
//          M E S S A G E   F O R   C O M M U N I C A T I O N
//=================================================================================================

/// <summary>
/// 응용 프로그램에서 미니필터 드라이버로 전송하는 명령을 지정한다.
/// </summary>
typedef enum _FILEACCCTRL_COMMAND {
    ACTIVATE,
    DEACTIVATE,
    SET_REMOTE_PATHS,
    SET_MSO_CACHE_DIR,
    SET_TEMPORARY_PATHS
} FILEACCCTRL_COMMAND, *PFILEACCCTRL_COMMAND;

/// <summary>
/// 응용 프로그램에서 미니필터 드라이버로 전송하는 명령 메시지를 나타낸다.
/// </summary>
typedef struct _FILEACCCTRL_COMMAND_MESSAGE {
    FILEACCCTRL_COMMAND Command;
    union {
        //
        // 액세스 제어를 활성화한다.
        //
        struct {
            BOOLEAN UseRecycleBin;
            BOOLEAN AllowSaveAs;
            BOOLEAN Reserved[2];        // for quad-word alignment on IA64
            ULONG WinExplorer;          // "Windows 탐색기"의 프로세스 ID
            //
            // 우회할 프로세스 ID의 배열
            //
            ULONG Count;
            ULONG PidArray[1];
        } Activate;
        //
        // 액세스 제어를 비활성화한다.
        //
        struct {
            ULONG Useless;
        } Deactivate;
        //
        // 파일(디렉토리)에 대한 액세스를 제어할 원격 경로들을 설정한다.
        //
        struct {
            ULONG Length;
            WCHAR PathBuffer[1];        // NULL 문자로 구분
        } SetRemotePaths;
        //
        // "Microsoft Office" 응용 프로그램의 캐시 디렉토리에 대한 경로를 설정한다.
        //
        struct {
            ULONG Length;
            WCHAR PathBuffer[1];        // NULL 종료 문자열
        } SetMsoCacheDir;
        //
        // 응용 프로그램들의 임시 파일이 생성되는 경로들을 설정한다.
        //
        struct {
            ULONG Length;
            WCHAR PathBuffer[1];        // NULL 문자로 구분
        } SetTemporaryPaths;
    }; // union
} FILEACCCTRL_COMMAND_MESSAGE, *PFILEACCCTRL_COMMAND_MESSAGE;

///////////////////////////////////////////////////////////////////////////////////////////////////

#define FAC_MAX_PATH    1024

/// <summary>
/// 미니필터 드라이버에서 응용 프로그램으로 전송하는 메시지의 타입을 지정한다.
/// </summary>
typedef enum _FILEACCCTRL_MESSAGE_TYPE {
    NOTIFY_PROCESS,
    GET_PERMISSIONS,
    WRITE_LOG,
    DELETE_FILE
} FILEACCCTRL_MESSAGE_TYPE;

/// <summary>
/// 미니필터 드라이버에서 응용 프로그램으로 전송하는 메시지를 나타낸다.
/// </summary>
typedef struct _FILEACCCTRL_MESSAGE_BODY {
    FILEACCCTRL_MESSAGE_TYPE MessageType;
    ULONG ProcessId;
    union {
        //
        // 생성되거나 종료된 프로세스를 통보한다.
        //
        struct {
            BOOLEAN Created;
        } NotifyProcess;
        //
        // 액세스 권한을 검색한다.
        //
        struct {
            BOOLEAN IsDirectory;
#ifdef _WIN64
            BOOLEAN Reserved[7];        // for quad-word alignment on IA64
#else
            BOOLEAN Reserved[3];        // for alignment
#endif
            WCHAR   FullPath[FAC_MAX_PATH];         // NULL 종료 문자열
        } GetPermissions;
        //
        // 로그를 기록한다.
        //
        struct {
            FAC_ACTION_TYPE ActionType;
            BOOLEAN IsDirectory;
            BOOLEAN Reserved[3];        // for quad-word alignment on IA64
            WCHAR   FullPath[FAC_MAX_PATH];         // NULL 종료 문자열
            WCHAR   NewFullPath[FAC_MAX_PATH];      // NULL 종료 문자열
        } WriteLog;
        //
        // 파일을 삭제한다. (파일을 휴지통 디렉토리로 이동)
        //
        struct {
            WCHAR FilePath[FAC_MAX_PATH];           // NULL 종료 문자열
            WCHAR NewFilePath[FAC_MAX_PATH];        // NULL 종료 문자열
        } DeleteFile;
    }; // union
} FILEACCCTRL_MESSAGE_BODY, *PFILEACCCTRL_MESSAGE_BODY;

///////////////////////////////////////////////////////////////////////////////////////////////////

/// <summary>
/// 미니필터 드라이버에서 전송한 메시지에 응답하는 메시지를 나타낸다.
/// </summary>
typedef struct _FILEACCCTRL_REPLY_BODY {
    // 파일(디렉토리)에 대한 액세스 권한과 작업 프로세스의 종류
    FAC_ACCESS_MASK  AccessRights;
    FAC_PROCESS_KIND ProcessKind;
} FILEACCCTRL_REPLY_BODY, *PFILEACCCTRL_REPLY_BODY;

//=================================================================================================
//          C O N S T A N T
//=================================================================================================

/// <summary>
/// 통신하는데 사용할 포트의 이름을 나타낸다.
/// </summary>
const PWSTR FileAccCtrlPortName = L"\\FacFlt_PortXxX";

/// <summary>
/// 휴지통(Recycle Bin) 디렉토리의 이름을 나타낸다.
/// </summary>
const PWSTR RecycleBinName = L"RECYCLE.BIN";

#endif /* __FILEACCCTRL_H__ */
