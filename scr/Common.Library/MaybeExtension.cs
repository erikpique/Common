﻿namespace Common.Library
{
    using System;

    public static class MaybeExtension
    {
        public static Result<T> ToResult<T>(this IMaybe<T> value) =>
            value.HasValue ? new Result<T>(value.Value) : new Result<T>(new Exception($"{nameof(T)} is null or empty"));

        public static Result<T> ToResult<T>(this IMaybe<T> value, Exception exception) =>
            value.HasValue ? new Result<T>(value.Value) : new Result<T>(exception);
    }
}
