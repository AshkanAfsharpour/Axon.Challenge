using System;

namespace Axon.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string value, object key)
            : base($" \"{key}\" ({value}) was not found.")
        {
        }
    }
}