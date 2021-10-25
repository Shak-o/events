using System;
using System.Net;

namespace AcademyApp.Service.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract string Code { get; }
        public abstract HttpStatusCode StatusCode { get; }
        public abstract string Type { get; }

        protected BaseException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
