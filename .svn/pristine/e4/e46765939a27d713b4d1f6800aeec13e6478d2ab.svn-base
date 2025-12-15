#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 4. 16)
 
***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-04-16     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈.
**********************************************************************************************************************/
#pragma endregion

#pragma once

using namespace System::Resources;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// 어셈블리에 포함된 리소스에 간편하게 액세스할 수 있다.
    /// </summary>
    public ref class AssemblyResource abstract sealed
    {
        static ResourceManager^ m_resManager;

#pragma region 속성

    private:
        /// <summary>
        /// 어셈블리에 포함된 리소스를 검색하는 <see cref="ResourceManager"/> 를 가져온다.
        /// </summary>
        /// <value>어셈블리에 포함된 리소스를 검색하는 <see cref="ResourceManager"/> 개체</value>
        static property ResourceManager^ Resource
        {
            ResourceManager^ get()
            {
                if (m_resManager == nullptr)
                {
                    m_resManager = gcnew ResourceManager(
                        "FilterOperator.Resources", (AssemblyResource::typeid)->Assembly);
                }

                return m_resManager;
            } // get()
        }

#pragma endregion

#pragma region 메소드

    public:
        /// <summary>
        /// 지정된 <see cref="String"/> 리소스의 값을 반환합니다.
        /// </summary>
        /// <param name="name">가져올 리소스 이름</param>
        /// <returns>현재 문화권 설정에 대해 지역화된 리소스의 값. 일치하지 않으면 nullptr</returns>
        /// 
        /// <exception cref="ArgumentNullException">name이 nullptr인 경우</exception>
        /// <exception cref="InvalidOperationException">지정된 리소스의 값이 문자열이 아닌 경우</exception>
        /// <exception cref="MissingManifestResourceException">
        /// 사용할 수 있는 리소스 집합을 찾을 수 없으며 중립 문화권 리소스가 없는 경우
        /// </exception>
        static String^ GetString(String^ name)
        {
            return Resource->GetString(name);
        }

#pragma endregion
    };
}
DEFAULT_NAMESPACE_END
