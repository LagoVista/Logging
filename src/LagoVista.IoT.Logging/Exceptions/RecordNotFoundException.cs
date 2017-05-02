using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Exceptions
{
    public class RecordNotFoundException : Exception
    {


        public Error Error { get; private set; }

        public string RecordType { get; private set; }

        public string RecordId { get; private set; }

        public static RecordNotFoundException FromErrorCode(ErrorCode errorCode, string recordType, string recordId)
        {
            return new RecordNotFoundException()
            {
                Error = errorCode.ToError(),
                RecordType = recordType,
                RecordId = recordId
            };
        }
    }
}
