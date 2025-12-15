#pragma once

#include "FacFilterMessaging.h"
#include "FacFilterWorker.h"
#include "AssemblyResource.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Threading;
// .NET용 개발 라이브러리
using namespace Link::Core::Runtime::InteropServices;

DEFAULT_NAMESPACE_BEGIN
{
    /// <summary>
    /// 액세스 제어(remote File Access Control) 미니필터 드라이버와 통신하면서 미니필터 드라이버의 기능을 조작한다.
    /// </summary>
    public ref class FacFilterOperator : FacFilterMessaging, IFacFilterOperator
    {
        IntPtr m_completionPort;
        Thread^ m_monitor;
        List<ManualResetEvent^>^ m_waitEvtList;
        List<AllocatedMemory^>^ m_bufferList;
        //String^ m_filterName;
        String^ m_recycleBinName;
        int m_threadCount;
        int m_requestCount;
        bool m_useRecycleBin;
        bool m_allowSaveAs;
        bool m_connected;

#pragma region 속성

    public:
        /// <summary>
        /// 생성할 작업 스레드의 개수를 가져오거나 설정한다.
        /// </summary>
        /// <value>생성할 작업 스레드의 개수. 기본값은 2</value>
        /// <remarks>작성 스레드는 최대 64개까지 생성할 수 있다.</remarks>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">속성의 값이 1보다 작거나 64보다 큰 경우</exception>
        property int ThreadCount
        {
            virtual int get() { return m_threadCount; }
            virtual void set(int value)
            {
                if (value < 1 || value > 64)
                {
                    throw gcnew ArgumentOutOfRangeException("value", 
                        String::Format(AssemblyResource::GetString("Error_OutOfRangeProperty_Between"), "ThreadCount", "1", "64"));
                }

                m_threadCount = value;
            } // set()
        }

        /// <summary>
        /// 각 작업 스레드에서 처리할 요청의 개수를 가져오거나 설정한다.
        /// </summary>
        /// <value>각 작업 스레드에서 처리할 요청의 개수. 기본값은 5</value>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">속성의 값이 0보다 작거나 같은 경우</exception>
        property int RequestCount
        {
            virtual int get() { return m_requestCount; }
            virtual void set(int value)
            {
                if (value <= 0)
                {
                    throw gcnew ArgumentOutOfRangeException("value", 
                        String::Format(AssemblyResource::GetString("Error_OutOfRangeProperty_Gt"), "RequestCount", "0"));
                }

                m_requestCount = value;
            } // set()
        }

        /// <summary>
        /// 휴지통 기능을 사용할지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>휴지통 기능을 사용하려면 true, 그렇지 않으면 false. 기본값은 true</value>
        property bool UseRecycleBin
        {
            virtual bool get() { return m_useRecycleBin; }
            virtual void set(bool value) { m_useRecycleBin = value; }
        }

        /// <summary>
        /// 파일을 로컬 디스크에 저장할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일을 로컬 디스크에 저장할 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        property bool AllowSaveAs
        {
            virtual bool get() { return m_allowSaveAs; }
            virtual void set(bool value) { m_allowSaveAs = value; }
        }

        /// <summary>
        /// 휴지통 디렉토리의 이름을 가져온다.
        /// </summary>
        /// <value>휴지통 디렉토리의 이름</value>
        property String^ RecycleBinDirectoryName
        {
            virtual String^ get() { return m_recycleBinName; }
        }

#pragma endregion

    public:
        FacFilterOperator();
        //FacFilterOperator(String^ filterName);
        //FacFilterOperator(String^ filterName, int threadCount, int requestCount);
        ~FacFilterOperator();

    public:
        virtual void ConnectFilter();
        virtual void DisconnectFilter();

        virtual void ActivateFilter();
        virtual void DeactivateFilter();
        virtual void SetRemotePaths(array<String^>^ paths);
        virtual void SetCacheDirectoryOfMsOffice();
        virtual void SetTemporaryPaths(array<String^>^ paths);

    private:
        void InitializeMemberVariables();
        void CloseConnectionPorts();
        void PrepareWorkersAndMonitor();

        void SetPathStrings(int command, String^ pathStrings);

    public:
        void Monitor_DoWork();
    };
}
DEFAULT_NAMESPACE_END
