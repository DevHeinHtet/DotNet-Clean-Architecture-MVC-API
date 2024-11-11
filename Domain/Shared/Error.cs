namespace Domain.Shared
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The result value is null.");

        public string Code { get; set; }
        public string Message { get; set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static implicit operator string(Error error) => error.Code;

        public override string ToString() => Code;

        public bool Equals(Error other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Code == Code && other.Message == Message;
        }

        public override bool Equals(object obj) => obj is Error other && Equals(other);

        public static bool operator ==(Error a, Error b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Error a, Error b) => !(a == b);

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message);
        }
    }
}
