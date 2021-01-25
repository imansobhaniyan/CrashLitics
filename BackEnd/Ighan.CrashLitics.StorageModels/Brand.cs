using System.Collections.Generic;

namespace Ighan.CrashLitics.StorageModels
{
    public class Brand
    {
        public Brand()
        {
            Models = new List<Model>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public List<Model> Models { get; set; }
    }
}
