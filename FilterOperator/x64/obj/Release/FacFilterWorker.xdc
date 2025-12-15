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
<member name="T:Link.EFAM.Engine.Filter.FacFilterWorker" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.h" line="12">
<summary>
액세스 제어(remote File Access Control) 미니필터 드라이버와 통신하면서
미니필터 드라이버에서 요청한 작업을 처리하는 작업 스레드를 나타낸다.
</summary>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterWorker.WaitHandle" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.h" line="26">
<summary>
동기화 개체를 가져온다.
</summary>
<value>동기화 개체를 나타내는 <see cref="T:System.Threading.ManualResetEvent"/></value>
</member>
<member name="T:Link.EFAM.Engine.Filter.AssemblyResource" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\AssemblyResource.h" line="20">
<summary>
어셈블리에 포함된 리소스에 간편하게 액세스할 수 있다.
</summary>
</member>
<member name="P:Link.EFAM.Engine.Filter.AssemblyResource.Resource" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\AssemblyResource.h" line="30">
<summary>
어셈블리에 포함된 리소스를 검색하는 <see cref="T:System.Resources.ResourceManager"/> 를 가져온다.
</summary>
<value>어셈블리에 포함된 리소스를 검색하는 <see cref="T:System.Resources.ResourceManager"/> 개체</value>
</member>
<member name="M:Link.EFAM.Engine.Filter.AssemblyResource.GetString(System.String)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\AssemblyResource.h" line="53">
<summary>
지정된 <see cref="T:System.String"/> 리소스의 값을 반환합니다.
</summary>
<param name="name">가져올 리소스 이름</param>
<returns>현재 문화권 설정에 대해 지역화된 리소스의 값. 일치하지 않으면 nullptr</returns>

<exception cref="T:System.ArgumentNullException">name이 nullptr인 경우</exception>
<exception cref="T:System.InvalidOperationException">지정된 리소스의 값이 문자열이 아닌 경우</exception>
<exception cref="T:System.Resources.MissingManifestResourceException">
사용할 수 있는 리소스 집합을 찾을 수 없으며 중립 문화권 리소스가 없는 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterWorker.#ctor(System.IntPtr,System.IntPtr)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.cpp" line="40">
<summary>
<see cref="T:Link.EFAM.Engine.Filter.FacFilterWorker"/> 클래스의 새 인스턴스를 초기화한다.
</summary>
<param name="commPort">통신 포트에 대한 핸들</param>
<param name="completionPort">I/O 완료 포트에 대한 핸들</param>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterWorker.DoWork" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.cpp" line="59">
<summary>
액세스 제어 미니필터 드라이버와 통신하면서 미니필터 드라이버에서 요청한 작업을 처리한다.
</summary>

<exception cref="T:System.InvalidOperationException">
미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
</exception>
<exception cref="T:System.ComponentModel.Win32Exception">
시스템 API에 액세스하는 동안 오류가 발생한 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterWorker.ProcessMessage(System.IntPtr,System.IntPtr)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.cpp" line="219">
<summary>
미니필터 드라이버에서 전송된 메시지의 내용을 처리하고,
미니필터 드라이버에 전송할 응답 메시지를 반환한다.
</summary>
<param name="messagePtr">전송된 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
<param name="replyPtr">전송할 응답 메시지를 받을 버퍼에 대한 포인터</param>
<returns>전송할 응답 메시지가 있으면 true, 그렇지 않으면 false</returns>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterWorker.SendFileToRecycleBin(System.String)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterWorker.cpp" line="419">
<summary>
지정한 파일을 휴지통으로 보낸다.
</summary>
<param name="path">휴지통으로 보낼 파일의 경로</param>
<returns>휴지통으로 이동된 파일의 경로</returns>

<exception cref="T:System.ArgumentException">
<paramref name="path"/>가 길이가 0인 문자열이거나, 공백만 포함하거나, 잘못된 문자를 포함하는 경우
</exception>
<exception cref="T:System.IO.FileNotFoundException"><paramref name="path"/>에 설명된 파일을 찾을 수 없는 경우</exception>
<exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/>에 지정한 경로가 잘못된 경우</exception>
<exception cref="T:System.IO.IOException">I/O 오류가 발생한 경우</exception>
<exception cref="T:System.UnauthorizedAccessException">파일에 대한 액세스가 거부된 경우</exception>
</member>
</members>
</doc>