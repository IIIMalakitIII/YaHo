using System;

namespace YaHo.YaHoApiService.Common.Exceptions.BusinessLogic
{
    public class ValidationException : BusinessLogicException
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
