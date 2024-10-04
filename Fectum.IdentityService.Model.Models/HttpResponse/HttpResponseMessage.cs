using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.HttpResponse
{
    public class HttpResponseMessage<T>(int StatusCode, string Content, T Data)
    {
        public bool IsSuccess { get; private set; }
        private readonly List<string> _error = [];

        public List<string> Errors => _error;

        public void SetError(string error) { Errors.Add(error); IsSuccess = false; }

        public void SetSuccess(int statuscode, string content, T data) { StatusCode = statuscode; Content = content; Data = data; IsSuccess = true; }
    }
}
