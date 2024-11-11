using ApplicationException = Domain.Exceptions.ApplicationException;

namespace Application.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred")
        {
            ErrorsDictionary = errorsDictionary;
        }
    }
}
