#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 3. 26)

***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-03-26     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈.
**********************************************************************************************************************/
#pragma endregion

#pragma comment(lib, "Advapi32.lib")

#include "StdAfx.h"
#include "Win32Utils.h"

using namespace msclr::interop;
// E-FAM 관련
using namespace Link::EFAM::Engine::Filter;

#pragma region 메소드

/// <summary>
/// 현재 프로세스의 권한을 조정(활성화 또는 비활성화)한다.
/// </summary>
/// <param name="privilegeName">조정할 권한의 이름</param>
/// <param name="enable">권한을 활성화하려면 true, 비활성화하려면 false</param>
/// 
/// <exception cref="ArgumentNullException">privilegeName이 nullptr인 경우</exception>
/// <exception cref="System::ComponentModel::Win32Exception">
/// 시스템 API에 액세스하는 동안 오류가 발생한 경우
/// </exception>
void Win32Utils::AdjustPrivilege(String^ privilegeName, bool enable)
{
    if (privilegeName == nullptr) throw gcnew ArgumentNullException("privilegeName");

    pin_ptr<const wchar_t> lpPrivilegeName = PtrToStringChars(privilegeName);
    HANDLE hAccessToken = NULL;         // Native 포인터
    BOOL succeeded;

    try
    {
        TOKEN_PRIVILEGES newState;
        LUID privLuid;

        // 현재 프로세스와 관련된 액세스 토큰을 연다. (Native 함수 호출)
        succeeded = OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY,
                                     &hAccessToken);
        if (!succeeded) throw gcnew System::ComponentModel::Win32Exception(GetLastError());

        // 권한 이름의 LUID를 검색한다. (Native 함수 호출)
        succeeded = LookupPrivilegeValueW(NULL, lpPrivilegeName, &privLuid);
        if (!succeeded) throw gcnew System::ComponentModel::Win32Exception(GetLastError());

        //
        // 조정할 권한에 대한 정보를 설정한다.
        //
        newState.PrivilegeCount = 1;
        newState.Privileges[0].Luid = privLuid;
        newState.Privileges[0].Attributes = enable ? SE_PRIVILEGE_ENABLED : 0;

        // 권한을 조정한다. (Native 함수 호출)
        succeeded = AdjustTokenPrivileges(hAccessToken, FALSE, &newState, 0, NULL, NULL);
        if (!succeeded) throw gcnew System::ComponentModel::Win32Exception(GetLastError());
    } // try
    finally
    {
        // 핸들을 닫는다. (Native 함수 호출)
        if (hAccessToken != NULL) CloseHandle(hAccessToken);
    }
}

#pragma endregion
