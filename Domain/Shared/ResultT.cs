﻿namespace Domain.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        protected internal Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The failure value can not be accessed.");

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}
