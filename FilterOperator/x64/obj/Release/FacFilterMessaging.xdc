<?xml version="1.0"?><doc>
<members>
<member name="T:_FAC_PROCESS_KIND" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="66">
<summary>
프로세스의 종류를 지정한다.
</summary>
</member>
<member name="T:_FILEACCCTRL_COMMAND" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="82">
<summary>
응용 프로그램에서 미니필터 드라이버로 전송하는 명령을 지정한다.
</summary>
</member>
<member name="T:_FILEACCCTRL_COMMAND_MESSAGE" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="93">
<summary>
응용 프로그램에서 미니필터 드라이버로 전송하는 명령 메시지를 나타낸다.
</summary>
</member>
<member name="T:_FILEACCCTRL_MESSAGE_TYPE" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="147">
<summary>
미니필터 드라이버에서 응용 프로그램으로 전송하는 메시지의 타입을 지정한다.
</summary>
</member>
<member name="T:_FILEACCCTRL_MESSAGE_BODY" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="157">
<summary>
미니필터 드라이버에서 응용 프로그램으로 전송하는 메시지를 나타낸다.
</summary>
</member>
<member name="T:_FILEACCCTRL_REPLY_BODY" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FileAccCtrl.h" line="204">
<summary>
미니필터 드라이버에서 전송한 메시지에 응답하는 메시지를 나타낸다.
</summary>
</member>
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
<member name="T:Link.EFAM.Engine.Filter.FacFilterMessaging" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterMessaging.h" line="11">
<summary>
액세스 제어(remote File Access Control) 미니필터 드라이버에 메시지를 전송하거나 메시지를 받는다.
</summary>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterMessaging.Adapter" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterMessaging.h" line="40">
<summary>
미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 가져오거나 설정한다.
</summary>
<value>미니필터 드라이버에서 요청하는 작업을 처리하는 <see cref="T:Link.EFAM.Core.IFacFilterAdapter"/></value>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterMessaging.#ctor" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterMessaging.cpp" line="25">
<summary>
<see cref="T:Link.EFAM.Engine.Filter.FacFilterMessaging"/> 클래스의 새 인스턴스를 초기화한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterMessaging.GetMessageAsync(System.IntPtr)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterMessaging.cpp" line="37">
<summary>
미니필터 드라이버에서 전송된 메시지를 비동기로(asynchronous) 가져온다.
</summary>
<param name="bufferPtr">메시지를 받을 버퍼에 대한 포인터</param>

<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
</members>
</doc>