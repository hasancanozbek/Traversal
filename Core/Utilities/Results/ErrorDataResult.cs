namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(string message, T data) : base(isSuccess: false, message: message, data: data)
        {
            
        }
        public ErrorDataResult(T data) : base(isSuccess: false, data: data)
        {
            
        }
    }
}
