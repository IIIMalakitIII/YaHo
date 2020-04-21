using System;

namespace YaHo.YaHoApiService.Common.Exceptions
{
    public class DataLogicException : Exception
    {
        public DataLogicException(string message)
            : base(message)
        {
        }
  
        public DataLogicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
