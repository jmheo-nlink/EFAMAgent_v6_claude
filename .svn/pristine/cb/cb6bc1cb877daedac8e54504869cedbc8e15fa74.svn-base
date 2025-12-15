#region 변경 이력
/*
 * Author : Link mskoo (2012. . )
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012--   mskoo           최초 작성.
 * 
 * 2012--   mskoo           5. 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Globalization;
using System.Resources;

namespace Link.EFAM.AppResources
{
    /// <summary>
    /// 지역화된 리소스를 검색하기 위한 리소스 클래스이다.
    /// </summary>
    public sealed class AppResource
    {
        private ResourceManager m_resManager = null;
        private CultureInfo m_culture = null;

        #region 생성자

        /// <summary>
        /// <see cref="AppResource"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private AppResource()
        {
        }

        #endregion

        #region 정적 속성
        #region Singleton 인스턴스

        private static AppResource m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="AppResource"/> 인스턴스를 반환한다.
        /// </summary>
        /// <value>캐시된 <see cref="AppResource"/> 개체</value>
        internal static AppResource Resource
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new AppResource();
                    m_instance.m_resManager = new ResourceManager("Link.EFAM.Properties.Resources",
                                                                  typeof(AppResource).Assembly);
                    m_instance.m_culture = null;
                } // if (m_instance == null)

                return m_instance;
            } // get
        }

        #endregion
        #endregion

        #region 정적 메소드

        /// <summary>
        /// 지역화된 리소스 문자열 또는 오류 메시지를 검색하고 서식을 지정한다.
        /// </summary>
        /// <param name="key">검색할 문자열 또는 오류 메시지의 식별자</param>
        /// <param name="values">문자열 또는 오류 메시지의 자리 표시자를 대체할 매개 변수의 배열</param>
        /// <returns>서식 지정된 리소스 문자열 또는 오류 메시지</returns>
        public static string GetString(string key, params object[] values)
        {
            if (values == null) values = new object[0];

            string format = Resource.GetResourceString(key);

            return String.Format(CultureInfo.CurrentCulture, format, values);
        }

        /// <summary>
        /// 지역화된 리소스 문자열 또는 오류 메시지를 검색한다.
        /// </summary>
        /// <param name="key">검색할 문자열 또는 오류 메시지의 식별자</param>
        /// <returns>리소스 문자열 또는 오류 메시지</returns>
        public static string GetString(string key)
        {
            return Resource.GetResourceString(key);
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지역화된 문자열 리소스를 가져온다.
        /// </summary>
        /// <param name="name">가져올 리소스 이름</param>
        /// <returns>지역화된 문자열 리소스</returns>
        private string GetResourceString(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return "[Resource lookup failed - null or empty resource name]";
            }

            string value = m_resManager.GetString(name, m_culture);

            return ((value != null) ? value : "[Resource lookup failed - resource not found]");
        }

        #endregion
    }
}
