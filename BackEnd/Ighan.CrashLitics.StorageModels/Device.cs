namespace Ighan.CrashLitics.StorageModels
{
    public class Device
    {
        public int Id { get; set; }

        public string Release { get; set; }

        public int SDK { get; set; }

        public string DeviceUniqueIdentifier { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }
    }
}
