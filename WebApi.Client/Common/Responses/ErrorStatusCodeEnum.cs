﻿namespace WebApi.Client.Common.Responses
{
    public enum ErrorStatusCodeEnum
    {
        OK = 0,
        UNKNOWN_ERROR = 1,
        SERVICE_UNAVAILABLE = 2,
        API_DEPRECATED = 3,
        RESOURCE_NOT_FOUND = 4,
        ERROR_AUTH = 5,
        FORBIDDEN = 6,
        TOKEN_EXPIRED = 7,
        TOO_MANY_REQUESTS = 8,
        NEED_TO_UPGRADE = 9,
        TIMEOUT = 10,
        BAD_REQUEST = 12,
        DUPLICATE_REQUEST = 13,
        LIMIT_EXCEEDED = 14,
        USER_TEMPORARILY_BLOCKED = 15,
        TOKEN_REFRESH_EXPIRED = 16,
        UNHANDLED_EXCEPTION = 17,
    }
}