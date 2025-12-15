#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 5. 28)

***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-05-28     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈.
**********************************************************************************************************************/
#pragma endregion

#pragma comment(lib, "fltLib.lib")

#include "StdAfx.h"
#include <fltUser.h>
#include "FilterMessaging.h"

// E-FAM 관련
using namespace Link::EFAM::Engine::Filter;

#pragma region 생성자

/// <summary>
/// <see cref="FilterMessaging"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
FilterMessaging::FilterMessaging()
    : m_port(IntPtr::Zero)
{
}

#pragma endregion

#pragma region 메소드

/// <summary>
/// 미니필터 드라이버에 메시지를 전송한다.
/// </summary>
/// <param name="bufferPtr">전송할 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
/// <param name="bufferSize">bufferPtr 매개 변수가 가리키고 있는 버퍼의 크기 (in bytes)</param>
/// 
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
inline void FilterMessaging::SendMessage(IntPtr bufferPtr, int bufferSize)
{
    HANDLE hPort = this->CommunicationPort.ToPointer();     // Native 포인터
    DWORD bytesReturned = 0;
    HRESULT hResult;

    // 미니필터 드라이버에 메시지를 전송한다. (Native 함수 호출)
    hResult = FilterSendMessage(hPort, bufferPtr.ToPointer(), bufferSize, NULL, 0, &bytesReturned);
    if (hResult != S_OK) InterOp::Marshal::ThrowExceptionForHR(hResult);
}

/// <summary>
/// 미니필터 드라이버에 응답 메시지를 전송한다.
/// </summary>
/// <param name="bufferPtr">전송할 응답 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
/// <param name="bufferSize">bufferPtr 매개 변수가 가리키고 있는 버퍼의 크기 (in bytes)</param>
/// 
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
inline void FilterMessaging::ReplyMessage(IntPtr bufferPtr, int bufferSize)
{
    HANDLE hPort = this->CommunicationPort.ToPointer();     // Native 포인터
    HRESULT hResult;

    // 미니필터 드라이버에 응답 메시지를 전송한다. (Native 함수 호출)
    hResult = FilterReplyMessage(hPort, (PFILTER_REPLY_HEADER)bufferPtr.ToPointer(), bufferSize);
    if (hResult != S_OK) InterOp::Marshal::ThrowExceptionForHR(hResult);
}

#pragma endregion
