namespace UserWebAPI.Models
{
    public class ReturnMessage<T>
    {
        public ReturnMessage()
        {
        }
        public ReturnMessage(T value, int code, string message)
        {
            this.Value = value;
            this.Code = code;
            this.Message = message;
        }
        public T Value { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}