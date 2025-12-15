<?xml version="1.0"?><doc>
<members>
<member name="T:Link.EFAM.Engine.Filter.Win32Utils" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Win32Utils.h" line="7">
<summary>
Win32 API 함수들을 제공한다.
</summary>
</member>
<member name="M:Link.EFAM.Engine.Filter.Win32Utils.AdjustPrivilege(System.String,System.Boolean)" decl="false" source="C:\Users\HEO\Link\EFAM\[240912]EFAM\03_EFAMv6_Agent_Source\[240315]EFAMAgent_v6)_ONLYINSTALLx86\FilterOperator\Win32Utils.cpp" line="25">
<summary>
현재 프로세스의 권한을 조정(활성화 또는 비활성화)한다.
</summary>
<param name="privilegeName">조정할 권한의 이름</param>
<param name="enable">권한을 활성화하려면 true, 비활성화하려면 false</param>

<exception cref="T:System.ArgumentNullException">privilegeName이 nullptr인 경우</exception>
<exception cref="T:System.ComponentModel.Win32Exception">
시스템 API에 액세스하는 동안 오류가 발생한 경우
</exception>
</member>
</members>
</doc>