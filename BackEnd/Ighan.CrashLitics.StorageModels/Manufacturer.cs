using System;
using System.Collections.Generic;
using System.Text;

namespace Ighan.CrashLitics.StorageModels
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Brands = new List<Brand>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Brand> Brands { get; set; }
    }
}
