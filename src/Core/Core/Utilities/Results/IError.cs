using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Messages;

namespace Core.Utilities.Results
{
    public interface IError
    {
        List<ErrorDetail>? Errors { get; }
    }
}
