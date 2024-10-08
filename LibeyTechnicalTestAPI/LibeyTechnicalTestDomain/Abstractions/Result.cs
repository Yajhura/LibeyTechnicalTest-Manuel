namespace LibeyTechnicalTestDomain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; }

        public static Result Success(string message) => new(true, message);

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);

        public static Result<T> Success<T>(T value, string message) => new(true, value, message);
        public static Result<T> Failure<T>(string message) => new(false, message);

        public static ResultQuery<T> SuccessQuery<T>(List<T> value, string message, int total)
                                                 => new(true, value, message, total);

        public static ResultQuery<T> FailureQuery<T>(string error)
                                                 => new(false, error);



        protected Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected internal Result(bool isSuccess, T? value, string message) : base(isSuccess, message)
        {
            Value = value;
        }

        protected internal Result(bool isSuccess, string message) : base(isSuccess, message)
        {
            Value = default;
        }

    }

    public class ResultQuery<T> : Result
    {
        public List<T> Value { get; }
        public int Total { get; }

        protected internal ResultQuery(bool isSuccess, List<T> value, string message, int total) : base(isSuccess, message)
        {
            Value = value;
            Total = total;
        }

        protected internal ResultQuery(bool isSuccess, string message) : base(isSuccess, message)
        {
            Value = new List<T>();
            Total = 0;
        }

    }
}
