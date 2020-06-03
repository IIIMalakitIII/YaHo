namespace YaHo.YaHoApiService.Common.Exceptions.DataLogic
{
    public class DeleteFailureException : DataLogicException
    {
        public DeleteFailureException(string name, object key)
            : base($"Failed to delete entity '{name}' with id '{key}'")
        {
        }
    }
}
