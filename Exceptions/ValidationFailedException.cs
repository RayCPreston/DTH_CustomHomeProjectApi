namespace DTH.API.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public string Code { get; set; }
        public ValidationFailedException()
        {
            Code = "004";
        }

        public ValidationFailedException(string message)
            : base(message)
        {
            Code = "004";
        }

        public ValidationFailedException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}
