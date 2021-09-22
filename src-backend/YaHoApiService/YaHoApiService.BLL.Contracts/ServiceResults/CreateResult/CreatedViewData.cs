namespace YaHo.YaHoApiService.BLL.Contracts.ServiceResults.CreateResult
{
    public class CreatedViewData
    {

        public CreatedViewData(object createdId)
        {
            CreatedId = createdId;
        }

        public object CreatedId { get; }
    }
}
