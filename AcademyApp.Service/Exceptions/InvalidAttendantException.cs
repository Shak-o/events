using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AcademyApp.Service.Exceptions
{
    public class InvalidAttendantException : BaseException
    {
        public override string Code => "BadRequest";
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string Type => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";

        public InvalidAttendantException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
