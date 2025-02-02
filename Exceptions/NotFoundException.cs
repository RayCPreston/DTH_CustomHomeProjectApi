namespace DTH.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Code { get; set; }
        public NotFoundException()
        {
            Code = "004";
        }

        public NotFoundException(string message)
            : base(message)
        {
            Code = "004";
        }

        public NotFoundException(string message, string code) 
            : base(message)
        {
            Code = code;
        }
    }
}
