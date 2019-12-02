using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Services.ServiceResults
{
    public class BaseResult
    {
        public BaseResult(IEnumerable<string> errors)
        {
            Errors = string.Join("\n", errors);
            IsValid = false;
        }

        public BaseResult(string errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public BaseResult()
        {
            IsValid = true;
        }

        public bool IsValid { get; }
        public string Errors { get; }
    }
}
