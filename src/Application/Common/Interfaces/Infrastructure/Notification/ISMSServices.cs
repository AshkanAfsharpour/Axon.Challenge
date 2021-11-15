using System.Collections.Generic;

namespace Axon.Application.Common.Interfaces
{
    public interface ISMSServices
    {
        bool SendSMS(string message, long mobile);
        bool SendSMS(string message, List<long> mobile);
    }
}
