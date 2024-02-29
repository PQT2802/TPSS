using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Common
{
    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }
        private Result(bool isSuccess, List<Error> errors)
        {
            if (isSuccess && (errors == null || errors.Count > 0) ||
                !isSuccess && (errors != null && errors.Count == 0))
            {
                throw new ArgumentException("Invalid errors", nameof(errors));
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }
        public List<Error> Errors { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result Failures(List<Error> errors) => new(false, errors);


        public static Error CreateError(string code, string errorMessage)
        {
            return new Error(code, errorMessage);
        }

    }
}
