using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string ArabicMessage { get; }

        public BadRequestException(string messageEn, string messageAr) : base(messageEn)
        {
            ArabicMessage = messageAr;
        }
    }
}
