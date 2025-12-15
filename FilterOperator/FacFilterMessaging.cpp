#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 5. 29)

***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-05-29     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈. (변경 이력 정리)
**********************************************************************************************************************/
#pragma endregion

#pragma comment(lib, "fltLib.lib")

#include "StdAfx.h"
#include "FacFilterTypes.h"
#include "FacFilterMessaging.h"

// E-FAM 관련
using namespace Link::EFAM::Engine::Filter;

#pragma region 생성자

/// <summary>
/// <see cref="FacFilterMessaging"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
FacFilterMessaging::FacFilterMessaging()
{
    m_messageSize = FIELD_OFFSET(FILEACCCTRL_MESSAGE, Overlapped);
}

#pragma endregion

#pragma region 메소드

/// <summary>
/// 미니필터 드라이버에서 전송된 메시지를 비동기로(asynchronous) 가져온다.
/// </summary>
/// <param name="bufferPtr">메시지를 받을 버퍼에 대한 포인터</param>
/// 
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterMessaging::GetMessageAsync(IntPtr bufferPtr)
{
    HANDLE hPort = this->CommunicationPort.ToPointer();     // Native 포인터
    PFILEACCCTRL_MESSAGE lpMessage = (PFILEACCCTRL_MESSAGE)bufferPtr.ToPointer();   // Native 포인터
    HRESULT hResult;

    ZeroMemory(&lpMessage->Overlapped, OvlpStructSize);     // Native 함수 호출

    // 미니필터 드라이버에서 전송된 메시지를 비동기로(asynchronous) 가져온다. (Native 함수 호출)
    hResult = FilterGetMessage(hPort, &lpMessage->Header, m_messageSize, &lpMessage->Overlapped);
    if (hResult != Error_IoPending) InterOp::Marshal::ThrowExceptionForHR(hResult);
}

#pragma endregion
