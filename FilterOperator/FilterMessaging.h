#pragma once

using namespace System;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// 미니필터 드라이버에 메시지를 전송한다.
    /// </summary>
    public ref class FilterMessaging
    {
        IntPtr m_port;

#pragma region 속성

    public:
        /// <summary>
        /// 미니필터 드라이버의 서버 포트와 연결된 통신 포트에 대한 핸들을 가져오거나 설정한다.
        /// </summary>
        /// <value>통신 포트에 대한 핸들</value>
        property IntPtr CommunicationPort
        {
            IntPtr get() { return m_port; }
            void set(IntPtr value) { m_port = value; }
        }

#pragma endregion

    public:
        FilterMessaging();

    public:
        virtual void SendMessage(IntPtr bufferPtr, int bufferSize);
        virtual void ReplyMessage(IntPtr bufferPtr, int bufferSize);
    };
}
DEFAULT_NAMESPACE_END
