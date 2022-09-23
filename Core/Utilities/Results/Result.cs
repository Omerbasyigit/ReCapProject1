using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success,string message):this(success)
        {
            this.message = message;
        }
        public Result(bool succes)
        {
            this.success= succes;
        }
        public bool success { get; }

        public string message { get; }
    }
}
