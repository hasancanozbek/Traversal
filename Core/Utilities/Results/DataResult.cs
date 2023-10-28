namespace Core.Utilities.Results
{
    public class DataResult<T> : Result
    {
        public T Data { get; set; }
        public DataResult(bool isSuccess, string message, T data) : base(isSuccess: isSuccess, message: message)
        {
            Data = data;
        }
        public DataResult(bool isSuccess, T data) : base(isSuccess: isSuccess)
        {
            Data = data;
        }
    }
}
