namespace DTH.API.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public string Code { get; set; }
        public AlreadyExistsException()
        {
            Code = "004";
        }

        public AlreadyExistsException(string message)
            : base(message)
        {
            Code = "004";
        }

        public AlreadyExistsException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}
