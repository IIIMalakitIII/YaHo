namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class MediaDbo
    {

        public MediaDbo()
        {
        }

        public MediaDbo(int productId, string contentType)
        {
            ProductId = productId;
            ContentType = contentType;
        }

        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public string ContentType { get; set; }

        public byte[] Picture { get; set; }

        public ProductDbo Product { get; set; }
    }
}
