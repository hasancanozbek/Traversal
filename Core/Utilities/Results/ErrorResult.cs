namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(isSuccess: false, message: message)
        {
            
        }
        public ErrorResult() : base(isSuccess: false)
        {
            
        }
    }
}
