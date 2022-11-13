using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Application.Client.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string propertyName, string errorMessage)
            : this(new List<ValidationFailure>()
            {
                new ValidationFailure(propertyName, errorMessage)
            })
        {
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(
                    failureGroup => failureGroup.Key.Length > 0
                        ? char.ToLower(failureGroup.Key[0]) +
                          (failureGroup.Key.Length > 1 ? failureGroup.Key.Substring(1) : "")
                        : failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}