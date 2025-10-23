using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alamana.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ArabicMessage { get; }

        public NotFoundException(string messageEn, string messageAr) : base(messageEn)
        {
            ArabicMessage = messageAr;
        }
    }
}
