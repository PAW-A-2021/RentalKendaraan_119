using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class KondisiKendaraan1
    {
        public KondisiKendaraan1()
        {
            Pengembalian1s = new HashSet<Pengembalian1>();
        }

        public int IdKondisi { get; set; }
        public string NamaKondisi { get; set; }

        public virtual ICollection<Pengembalian1> Pengembalian1s { get; set; }
    }
}
