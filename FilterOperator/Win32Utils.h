#pragma once

using namespace System;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// Win32 API 함수들을 제공한다.
    /// </summary>
    public ref class Win32Utils
    {
    private:
        Win32Utils() { }

    public:
        static void AdjustPrivilege(String^ privilegeName, bool enable);
    };
}
DEFAULT_NAMESPACE_END
