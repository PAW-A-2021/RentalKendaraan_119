using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Kendaraan1
    {
        public Kendaraan1()
        {
            Peminjaman1s = new HashSet<Peminjaman1>();
        }

        public int IdKendaraan { get; set; }
        public string NamaKendaraan { get; set; }
        public string NoPolisi { get; set; }
        public string NoStnk { get; set; }
        public int? IdJenisKendaraan { get; set; }
        public string Ketersediaan { get; set; }

        public virtual JenisKendaraan1 IdJenisKendaraanNavigation { get; set; }
        public virtual ICollection<Peminjaman1> Peminjaman1s { get; set; }
    }
}
