using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Peminjaman1
    {
        public Peminjaman1()
        {
            Pengembalian1s = new HashSet<Pengembalian1>();
        }

        [Required(ErrorMessage = "ID Peminjaman tidak boleh kosong!")]
        public int IdPeminjaman { get; set; }

        [Required(ErrorMessage = "Tanggal Peminjaman tidak boleh kosong!")]
        public DateTime? TglPeminjaman { get; set; }

        [Required(ErrorMessage = "ID Kendaraan tidak boleh kosong!")]
        public int? IdKendaraan { get; set; }

        [Required(ErrorMessage = "ID Customer tidak boleh kosong!")]
        public int? IdCustomer { get; set; }

        [Required(ErrorMessage = "ID Jaminan tidak boleh kosong!")]
        public int? IdJaminan { get; set; }

        [Required(ErrorMessage = "Biaya tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int? Biaya { get; set; }

        public virtual Customer1 IdCustomerNavigation { get; set; }
        public virtual Jaminan1 IdJaminanNavigation { get; set; }
        public virtual Kendaraan1 IdKendaraanNavigation { get; set; }
        public virtual ICollection<Pengembalian1> Pengembalian1s { get; set; }
    }
}
