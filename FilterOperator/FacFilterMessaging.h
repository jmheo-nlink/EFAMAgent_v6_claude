#pragma once

#include "FilterMessaging.h"

using namespace System;
using namespace System::Diagnostics;
using namespace Link::EFAM::Core;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// 액세스 제어(remote File Access Control) 미니필터 드라이버에 메시지를 전송하거나 메시지를 받는다.
    /// </summary>
    public ref class FacFilterMessaging : FilterMessaging
    {
#pragma region 상수

    protected:
        static const int Error_InvalidHandle = HRESULT_FROM_WIN32(ERROR_INVALID_HANDLE);
        static const int Error_IoPending = HRESULT_FROM_WIN32(ERROR_IO_PENDING);

        literal int CommandMessageSize = sizeof(FILEACCCTRL_COMMAND_MESSAGE);
        literal int ReceiveMessageSize = sizeof(FILEACCCTRL_MESSAGE);
        literal int ReplyMessageSize = sizeof(FILEACCCTRL_REPLY);
        literal int OvlpStructSize = sizeof(OVERLAPPED);

#pragma endregion

    protected:
        static BooleanSwitch^ m_tracing = gcnew BooleanSwitch("traceSwitch", "FilterOperator Module");

        IFacFilterAdapter^ m_adapter;

    private:
        int m_messageSize;

#pragma region 속성

    public:
        /// <summary>
        /// 미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 가져오거나 설정한다.
        /// </summary>
        /// <value>미니필터 드라이버에서 요청하는 작업을 처리하는 <see cref="IFacFilterAdapter"/></value>
        property IFacFilterAdapter^ Adapter
        {
            virtual IFacFilterAdapter^ get() { return m_adapter; }
            virtual void set(IFacFilterAdapter^ value) { m_adapter = value; }
        }

#pragma endregion

    protected:
        FacFilterMessaging();

    protected:
        void GetMessageAsync(IntPtr bufferPtr);
    };
}
DEFAULT_NAMESPACE_END
