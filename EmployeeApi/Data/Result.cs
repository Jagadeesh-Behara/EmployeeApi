namespace EmployeeApi.Data
{
    public class Result<T>
    {
        public bool Success { get; }
        public T? Data { get; }
        public string? Message { get; }

        public Result(bool success, T? data = default, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        public static Result<T> Ok(T data, string? message) => new Result<T>(true, data, message);
        public static Result<T> Fail(string message) => new Result<T>(false, default, message);
    }

}
