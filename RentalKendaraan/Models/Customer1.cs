using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Customer1
    {
        public Customer1()
        {
            Peminjaman1s = new HashSet<Peminjaman1>();
        }

        public int IdCustomer { get; set; }
        public string NamaCustomer { get; set; }
        public string Nik { get; set; }
        public string Alamat { get; set; }
        public string NoHp { get; set; }
        public int? IdGender { get; set; }

        public virtual Gender1 IdGenderNavigation { get; set; }
        public virtual ICollection<Peminjaman1> Peminjaman1s { get; set; }
    }
}
