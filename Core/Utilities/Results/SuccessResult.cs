namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(isSuccess: true, message: message)
        {
            
        }
        public SuccessResult() : base(isSuccess: true)
        {
            
        }
    }
}
