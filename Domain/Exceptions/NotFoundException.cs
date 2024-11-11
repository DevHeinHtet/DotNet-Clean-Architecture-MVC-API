namespace Domain.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) 
            : base("Not Found Failure", message)
        {

        }
    }
}