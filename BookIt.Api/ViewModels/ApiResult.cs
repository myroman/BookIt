namespace BookIt.Api.ViewModels
{
    public class ApiResult<T>
    {
        public T Value { get; private set; }

        public ApiResult()
        {
        }

        public ApiResult(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        public ApiResult(T value)
        {
            Value = value;
        }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(ErrorMsg); }
        }
        public string ErrorMsg { get; set; }
    }
}