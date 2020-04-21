namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class MediaDbo
    {

        public MediaDbo()
        {
        }

        public MediaDbo(int productId, string filePath, string contentType)
        {
            ProductId = productId;
            FilePath = filePath;
            ContentType = contentType;
        }

        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public ProductDbo Product { get; set; }
    }
}
