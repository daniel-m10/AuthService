﻿namespace AuthService.Api.Domain.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
    }
}
