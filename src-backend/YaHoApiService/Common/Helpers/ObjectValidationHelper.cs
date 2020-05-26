using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.Common.Helpers
{
    public class ObjectValidationHelper
    {
        public static void CheckObjectNotNull(object obj, string name, object key)
        {
            if (obj is null)
            {
                throw new NotFoundException(name, key);
            }
        }
    }
}
