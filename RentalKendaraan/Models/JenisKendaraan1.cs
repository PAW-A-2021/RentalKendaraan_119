using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class JenisKendaraan1
    {
        public JenisKendaraan1()
        {
            Kendaraan1s = new HashSet<Kendaraan1>();
        }

        public int IdJenisKendaraan { get; set; }
        public string NamaJenisKendaraan { get; set; }

        public virtual ICollection<Kendaraan1> Kendaraan1s { get; set; }
    }
}
