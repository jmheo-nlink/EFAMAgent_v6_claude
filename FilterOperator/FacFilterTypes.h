#pragma once

#include <fltUser.h>
#include "FileAccCtrl.h"

DEFAULT_NAMESPACE_BEGIN
{
#pragma unmanaged

    //
    // 미니필터 드라이버에서 전송된 메시지
    //
    typedef struct _FILEACCCTRL_MESSAGE {
        FILTER_MESSAGE_HEADER    Header;
        FILEACCCTRL_MESSAGE_BODY Body;
        //
        //  Overlapped structure: this is not really part of the message
        //  However we embed it instead of using a separately allocated overlap structure
        //
        OVERLAPPED Overlapped;
    } FILEACCCTRL_MESSAGE, *PFILEACCCTRL_MESSAGE;

    //
    // 미니필터 드라이버에 응답하는 메시지
    //
    typedef struct _FILEACCCTRL_REPLY {
        FILTER_REPLY_HEADER    Header;
        FILEACCCTRL_REPLY_BODY Body;
    } FILEACCCTRL_REPLY, *PFILEACCCTRL_REPLY;

#pragma managed
}
DEFAULT_NAMESPACE_END
