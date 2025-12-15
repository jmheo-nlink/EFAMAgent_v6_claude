#pragma once

using namespace System;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// 공통으로 사용할 수 있는 메소드들을 제공한다.
    /// </summary>
    public ref class Utils abstract sealed
    {
    public:
        static String^ ConcatenateRemotePaths(array<String^>^ pathArray, wchar_t separator);
        static void CloseObjectHandle(IntPtr handle);
    };
}
DEFAULT_NAMESPACE_END
