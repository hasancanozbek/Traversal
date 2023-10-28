namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(string message, T data) : base(isSuccess: true, message: message, data: data)
        {
            
        }
        public SuccessDataResult(T data) : base(isSuccess: true, data: data)
        {
            
        }
    }
}
