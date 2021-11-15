using System.Collections.Generic;

namespace Axon.Application.Common.Models
{
    public static class ServiceResponse
    {
        public static ServiceResponse<T> Failure<T>(string message = "Failure", IDictionary<string, string[]> errors = default, T data = default) =>
            new ServiceResponse<T>(data, message, true, errors);

        public static ServiceResponse<T> OK<T>(T data , string message = "Success") =>
            new ServiceResponse<T>(data, message, false);
    }

    public class ServiceResponse<T>
    {
        public ServiceResponse(T data, string msg, bool err, IDictionary<string, string[]> errors = default)
        {
            Data = data;
            Message = msg;
            Failure = err;
            Errors = errors;
        }
        public ServiceResponse()
        {

        }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Failure { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
