using Microsoft.EntityFrameworkCore;


namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    abstract class BaseDataBuilder
    {
        protected ModelBuilder ModelBuilder;

        protected BaseDataBuilder(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        public abstract void SetData();
    }
}
