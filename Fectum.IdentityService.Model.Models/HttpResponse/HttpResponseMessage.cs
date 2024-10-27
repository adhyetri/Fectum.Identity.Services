using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.HttpResponse
{
    public class HttpResponseMessage<T>
    {
        private readonly List<string> _error = [];
        public int StatusCode { get; private set; }
        public string Content { get; private set; }
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public List<string> Errors => _error;

        public void SetError(string error) { _error.Add(error); IsSuccess = false; }

        public void SetSuccess(int statusCode, string content, T data, bool issuccess) { StatusCode = statusCode; Content = content; Data = data; IsSuccess = true; }
    }
}
