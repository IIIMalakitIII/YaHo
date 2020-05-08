namespace YaHo.YaHoApiService.Common.Exceptions.DataLogic
{
    public class CreateFailureException : DataLogicException
    {
        public CreateFailureException(string name)
            : base($"Failed to create entity '{name}'")
        {
        }
    }
}
