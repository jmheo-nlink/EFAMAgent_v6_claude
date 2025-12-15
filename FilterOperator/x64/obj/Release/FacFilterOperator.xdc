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
<member name="T:Link.EFAM.Engine.Filter.FacFilterOperator" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="15">
<summary>
액세스 제어(remote File Access Control) 미니필터 드라이버와 통신하면서 미니필터 드라이버의 기능을 조작한다.
</summary>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterOperator.ThreadCount" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="35">
<summary>
생성할 작업 스레드의 개수를 가져오거나 설정한다.
</summary>
<value>생성할 작업 스레드의 개수. 기본값은 2</value>
<remarks>작성 스레드는 최대 64개까지 생성할 수 있다.</remarks>

<exception cref="T:System.ArgumentOutOfRangeException">속성의 값이 1보다 작거나 64보다 큰 경우</exception>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterOperator.RequestCount" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="57">
<summary>
각 작업 스레드에서 처리할 요청의 개수를 가져오거나 설정한다.
</summary>
<value>각 작업 스레드에서 처리할 요청의 개수. 기본값은 5</value>

<exception cref="T:System.ArgumentOutOfRangeException">속성의 값이 0보다 작거나 같은 경우</exception>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterOperator.UseRecycleBin" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="78">
<summary>
휴지통 기능을 사용할지 여부를 나타내는 값을 가져오거나 설정한다.
</summary>
<value>휴지통 기능을 사용하려면 true, 그렇지 않으면 false. 기본값은 true</value>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterOperator.AllowSaveAs" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="88">
<summary>
파일을 로컬 디스크에 저장할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
</summary>
<value>파일을 로컬 디스크에 저장할 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
</member>
<member name="P:Link.EFAM.Engine.Filter.FacFilterOperator.RecycleBinDirectoryName" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.h" line="98">
<summary>
휴지통 디렉토리의 이름을 가져온다.
</summary>
<value>휴지통 디렉토리의 이름</value>
</member>
<member name="T:Link.EFAM.Engine.Filter.Utils" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Utils.h" line="7">
<summary>
공통으로 사용할 수 있는 메소드들을 제공한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.#ctor" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="38">
<summary>
<see cref="T:Link.EFAM.Engine.Filter.FacFilterOperator"/> 클래스의 새 인스턴스를 초기화한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.Dispose" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="104">
<summary>
인스턴스가 할당한 리소스를 모두 해제한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.InitializeMemberVariables" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="117">
<summary>
멤버 변수들을 초기화한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.CloseConnectionPorts" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="134">
<summary>
미니필터 드라이버와 통신하기 위해 생성한 통신 포트와 I/O 완료 포트를 닫는다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.ConnectFilter" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="150">
<summary>
액세스 제어 미니필터 드라이버에 연결한다.
</summary>
<remarks>미니필터 드라이버에서 생성된 서버 포트에 연결한다.</remarks>

<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 이미 연결되어 있는 경우<br/>
- 또는 -<br/>
미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
<exception cref="T:System.ComponentModel.Win32Exception">
시스템 API에 액세스하는 동안 오류가 발생한 경우
</exception>
<exception cref="T:System.OutOfMemoryException">
새 스레드를 시작할 충분한 메모리가 없는 경우<br/>
- 또는 -<br/>
관리되지 않는 메모리에 메시지 버퍼에 할당할 충분한 메모리가 없는 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.DisconnectFilter" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="224">
<summary>
액세스 제어 미니필터 드라이버와 연결을 해제한다.
</summary>
<remarks>미니필터 드라이버의 서버 포트와 연결된 통신 포트를 닫아서 연결을 끊는다.</remarks>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.PrepareWorkersAndMonitor" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="241">
<summary>
작업 스레드와 모니터 스레드를 시작한다.
</summary>

<exception cref="T:System.InvalidOperationException">
드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
</exception>
<exception cref="T:System.OutOfMemoryException">
새 스레드를 시작할 충분한 메모리가 없는 경우<br/>
- 또는 -<br/>
관리되지 않는 메모리에 메시지 버퍼에 할당할 충분한 메모리가 없는 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.ActivateFilter" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="315">
<summary>
원격 파일 및 디렉토리에 대한 액세스 제어를 활성화한다.
</summary>

<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우<br/>
- 또는 -<br/>
프로세스 정보를 얻는 데 사용되는 성능 카운터 API에 액세스하는 데 문제가 있는 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.DeactivateFilter" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="390">
<summary>
원격 파일 및 디렉토리에 대한 액세스 제어를 비활성화한다.
</summary>

<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.SetRemotePaths(System.String[])" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="418">
<summary>
원격 파일 및 디렉토리에 대한 액세스를 제어할 원격 경로들을 설정한다.
</summary>
<param name="paths">액세스를 제어할 원격 경로들의 배열</param>
<remarks>
Example : "\\192.168.0.19\Shared"<br/>
          "\\192.168.0.45\Shared2\doc_folder"<br/>
          "\\192.168.0.50"
</remarks>

<exception cref="T:System.ArgumentNullException">paths가 nullptr인 경우</exception>
<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.SetCacheDirectoryOfMsOffice" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="444">
<summary>
"Microsoft Office" 응용 프로그램의 캐시 디렉토리의 경로를 설정한다.
</summary>

<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.SetTemporaryPaths(System.String[])" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="471">
<summary>
응용 프로그램들의 임시 파일이 생성되는 경로들을 설정한다.
</summary>
<param name="paths">임시 파일이 생성되는 경로들의 배열</param>

<exception cref="T:System.ArgumentNullException">paths가 nullptr인 경우</exception>
<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.SetPathStrings(System.Int32,System.String)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="518">
<summary>
NULL 문자로 구분된 경로 문자열들을 설정한다.
</summary>
<param name="command">명령의 종류를 나타내는 NETACCCTRL_COMMAND 값 중 하나</param>
<param name="pathStrings">NULL 문자로 구분된 경로 문자열들</param>

<exception cref="T:System.ArgumentNullException">pathStrings가 nullptr인 경우</exception>
<exception cref="T:System.InvalidOperationException">
액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
</exception>
<exception cref="T:System.Runtime.InteropServices.COMException">
인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.FacFilterOperator.Monitor_DoWork" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\FacFilterOperator.cpp" line="575">
<summary>
실행 중인 모든 작업 스레드들이 종료될 때까기 대기한다.
</summary>
</member>
</members>
</doc>