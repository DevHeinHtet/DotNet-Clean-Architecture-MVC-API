namespace Domain.Shared
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public Error Error { get; set; }
        public bool IsFailure => !IsSuccess;

        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

        public static Result<TValue> Create<TValue>(TValue value)
        {
            if (value is null)
            {
                return Failure<TValue>(Error.NullValue);
            }
            return Success(value);
        }
    }
}
