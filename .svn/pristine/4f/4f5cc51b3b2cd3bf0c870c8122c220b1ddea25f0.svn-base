#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 5. 30)
 
***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-05-30     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈.
**********************************************************************************************************************/
#pragma endregion

#include "StdAfx.h"
#include <Windows.h>
#include "Utils.h"

using namespace System::Text;
// E-FAM 관련
using namespace Link::EFAM::Engine::Filter;

#pragma region 메소드

/// <summary>
/// 지정한 원격 경로 문자열들을 지정한 문자로 구분하여 연결한다.
/// </summary>
/// <param name="pathArray">연결할 원격 경로들의 배열</param>
/// <param name="separator">원격 경로들을 구분하는 유니코드 문자</param>
/// <returns>연결된 문자열. 연결할 수 있는 원격 경로가 없으면 빈 문자열("")</returns>
/// 
/// <exception cref="ArgumentNullException">pathArray가 nullptr인 경우</exception>
String^ Utils::ConcatenateRemotePaths(array<String^>^ pathArray, wchar_t separator)
{
    if (pathArray == nullptr) throw gcnew ArgumentNullException("pathArray");

    StringBuilder^ concatStrBuilder = gcnew StringBuilder();

    for each (String^ path in pathArray)
    {
        if (String::IsNullOrEmpty(path)) continue;
        if (path->Length < 3) continue;

        concatStrBuilder->Append(path->Substring(1));
        concatStrBuilder->Append(separator);
    }

    return concatStrBuilder->ToString();
}

/// <summary>
/// 지정한 핸들을 닫는다.
/// </summary>
/// <param name="handle">닫을 핸들</param>
void Utils::CloseObjectHandle(IntPtr handle)
{
    HANDLE hObject = handle.ToPointer();

    if (hObject != NULL && hObject != INVALID_HANDLE_VALUE)
    {
        // 핸들을 닫는다. (Native 함수 호출)
        CloseHandle(hObject);
    }
}

#pragma endregion
