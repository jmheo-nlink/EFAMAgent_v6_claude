<?xml version="1.0"?><doc>
<members>
<member name="T:Link.EFAM.Engine.Filter.FilterMessaging" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FilterMessaging.h" line="7">
<summary>
미니필터 드라이버에 메시지를 전송한다.
</summary>
</member>
<member name="P:Link.EFAM.Engine.Filter.FilterMessaging.CommunicationPort" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FilterMessaging.h" line="17">
<summary>
미니필터 드라이버의 서버 포트와 연결된 통신 포트에 대한 핸들을 가져오거나 설정한다.
</summary>
<value>통신 포트에 대한 핸들</value>
</member>
<member name="M:Link.EFAM.Engine.Filter.FilterMessaging.#ctor" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FilterMessaging.cpp" line="25">
<summary>
<see cref="T:Link.EFAM.Engine.Filter.FilterMessaging"/> 클래스의 새 인스턴스를 초기화한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FilterMessaging.SendMessage(System.IntPtr,System.Int32)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FilterMessaging.cpp" line="37">
<summary>
미니필터 드라이버에 메시지를 전송한다.
</summary>
<param name="bufferPtr">전송할 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
<param name="bufferSize">bufferPtr 매개 변수가 가리키고 있는 버퍼의 크기 (in bytes)</param>

<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FilterMessaging.ReplyMessage(System.IntPtr,System.Int32)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FilterMessaging.cpp" line="57">
<summary>
미니필터 드라이버에 응답 메시지를 전송한다.
</summary>
<param name="bufferPtr">전송할 응답 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
<param name="bufferSize">bufferPtr 매개 변수가 가리키고 있는 버퍼의 크기 (in bytes)</param>

<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
</members>
</doc>