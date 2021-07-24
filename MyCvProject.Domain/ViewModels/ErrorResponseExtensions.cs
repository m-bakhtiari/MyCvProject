using Microsoft.AspNetCore.Mvc;

namespace MyCvProject.Domain.ViewModels
{
    public static class ErrorResponseExtensions
    {
        public static BadRequestObjectResult ToBadRequestError(this OpRes opRes)
        {
            var err = new ErrorResponse()
            {
                Error = "bad_request",
                ErrorDescription = opRes.Error.Message
            };

            return new BadRequestObjectResult(err);
        }

        public static ErrorResponse ToApiError(this OpRes opRes)
        {
            return new ErrorResponse()
            {
                Error = "bad_request",
                ErrorDescription = opRes.Error.Message
            };
        }

        public static ErrorResponse ToApiError(this OpRes opRes, string errorCode, string desc)
        {
            return new ErrorResponse()
            {
                Error = errorCode,
                ErrorDescription = desc
            };
        }

        public static ErrorResponse ToApiError(this OpRes opRes, string desc)
        {
            return new ErrorResponse()
            {
                Error = "bad_request",
                ErrorDescription = desc
            };
        }
    }
}
