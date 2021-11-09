using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Peminjaman1
    {
        public Peminjaman1()
        {
            Pengembalian1s = new HashSet<Pengembalian1>();
        }

        public int IdPeminjaman { get; set; }
        public DateTime? TglPeminjaman { get; set; }
        public int? IdKendaraan { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdJaminan { get; set; }
        public int? Biaya { get; set; }

        public virtual Customer1 IdCustomerNavigation { get; set; }
        public virtual Jaminan1 IdJaminanNavigation { get; set; }
        public virtual Kendaraan1 IdKendaraanNavigation { get; set; }
        public virtual ICollection<Pengembalian1> Pengembalian1s { get; set; }
    }
}
