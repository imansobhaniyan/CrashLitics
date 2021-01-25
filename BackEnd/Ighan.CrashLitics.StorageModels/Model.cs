using System.Collections.Generic;

namespace Ighan.CrashLitics.StorageModels
{
    public class Model
    {
        public Model()
        {
            Devices = new List<Device>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<Device> Devices { get; set; }
    }
}
