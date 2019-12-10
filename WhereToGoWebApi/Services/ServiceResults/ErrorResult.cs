using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Services.ServiceResults
{
    public class ErrorResult : BaseResult
    {
        public ErrorResult(string errors) : base(errors) { }

        public ErrorResult(IEnumerable<string> errors) : base(errors) { }
    }
}
