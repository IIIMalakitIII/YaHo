namespace YaHo.YaHoApiService.ViewModels.MediaViewModels
{
    public class MediaViewModel
    {
        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public string ContentType { get; set; }

        public byte[] Picture { get; set; }
    }
}
