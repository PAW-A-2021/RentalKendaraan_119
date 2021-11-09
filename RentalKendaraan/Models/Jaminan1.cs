using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Jaminan1
    {
        public Jaminan1()
        {
            Peminjaman1s = new HashSet<Peminjaman1>();
        }

        public int IdJaminan { get; set; }
        public string NamaJaminan { get; set; }

        public virtual ICollection<Peminjaman1> Peminjaman1s { get; set; }
    }
}
