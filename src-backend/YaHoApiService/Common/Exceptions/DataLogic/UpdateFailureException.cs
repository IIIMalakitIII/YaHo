namespace YaHo.YaHoApiService.Common.Exceptions.DataLogic
{
    public class UpdateFailureException : DataLogicException
    {
        public UpdateFailureException(string name, object key) : base($"Failed to update entity '{name}' with id '{key}'")
        {
        }
    }
}
