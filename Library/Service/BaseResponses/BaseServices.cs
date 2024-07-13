using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseResponses
{
    public abstract class BaseServices<T> where T : class
    {
        #region Success
        internal ResponseResult Success(HttpStatusCode statusCode, T data)
        {
            return new ResponseResult
            {
                StatusCode = statusCode,
                IsSuccess = true,
                Data = data
            };
        }
        internal ResponseResult Success()
        {
            return new ResponseResult
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
            };
        }
        internal ResponseResult Success(string Message)
        {
            return new ResponseResult
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = new { Message }
            };
        }
        #endregion

        #region Error
        internal ResponseResult Error()
        {
            return new ResponseResult
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
            };
        }
        internal ResponseResult Error(List<string> errors)
        {
            return new ResponseResult
            {
                StatusCode = HttpStatusCode.BadGateway,
                IsSuccess = false,
                Errors = errors
            };
        }
        #endregion
    }
    public class ResponseResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }
}
