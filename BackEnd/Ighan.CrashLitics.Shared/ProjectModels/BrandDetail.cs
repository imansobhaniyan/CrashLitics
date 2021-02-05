namespace Ighan.CrashLitics.Shared.ProjectModels
{
    public class BrandDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ManufacturerDetail Manufacturer { get; set; }
    }
}
