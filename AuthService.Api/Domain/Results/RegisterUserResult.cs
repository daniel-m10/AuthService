namespace AuthService.Api.Domain.Results
{
    public class RegisterUserResult
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }

        private RegisterUserResult(bool success, string? error)
        {
            IsSuccess = success;
            ErrorMessage = error;
        }

        public static RegisterUserResult Success() => new(true, null);
        public static RegisterUserResult Failure(string errorMessage) => new(false, errorMessage);
    }
}
