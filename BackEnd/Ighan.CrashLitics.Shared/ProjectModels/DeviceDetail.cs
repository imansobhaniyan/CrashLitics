namespace Ighan.CrashLitics.Shared.ProjectModels
{
    public class DeviceDetail
    {
        public int Id { get; set; }

        public string Release { get; set; }

        public int SDK { get; set; }

        public string DeviceUniqueIdentifier { get; set; }

        public ModelDetail Model { get; set; }
    }
}
