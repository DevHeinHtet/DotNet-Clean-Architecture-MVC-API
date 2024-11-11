namespace Domain.Shared
{
    public class ValidationResult : Result, IValidationResult
    {
        protected internal ValidationResult(Error[] errors)
            : base(false, IValidationResult.ValidationError)
        {
        }
        public Error[] Errors { get; }
        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}