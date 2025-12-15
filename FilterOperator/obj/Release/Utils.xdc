<?xml version="1.0"?><doc>
<members>
<member name="T:Link.EFAM.Engine.Filter.Utils" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Utils.h" line="7">
<summary>
공통으로 사용할 수 있는 메소드들을 제공한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.Utils.ConcatenateRemotePaths(System.String[],System.Char)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Utils.cpp" line="24">
<summary>
지정한 원격 경로 문자열들을 지정한 문자로 구분하여 연결한다.
</summary>
<param name="pathArray">연결할 원격 경로들의 배열</param>
<param name="separator">원격 경로들을 구분하는 유니코드 문자</param>
<returns>연결된 문자열. 연결할 수 있는 원격 경로가 없으면 빈 문자열("")</returns>

<exception cref="T:System.ArgumentNullException">pathArray가 nullptr인 경우</exception>
</member>
<member name="M:Link.EFAM.Engine.Filter.Utils.CloseObjectHandle(System.IntPtr)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Utils.cpp" line="50">
<summary>
지정한 핸들을 닫는다.
</summary>
<param name="handle">닫을 핸들</param>
</member>
</members>
</doc>