namespace Airline.Business.Exceptions
{
    public class ImageException : Exception
    {
        public string name { get; set; }
        public ImageException(string? message, string paramName) : base(message)
        {
            name = paramName ?? string.Empty;
        }
    }
}
