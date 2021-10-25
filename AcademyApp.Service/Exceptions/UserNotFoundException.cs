using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AcademyApp.Service.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string errorMessage) : base(errorMessage)
        {
        }

        public override string Code => "NotFound";
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Type => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
    }
}
