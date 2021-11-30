using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Jaminan1
    {
        public Jaminan1()
        {
            Peminjaman1s = new HashSet<Peminjaman1>();
        }

        [Required(ErrorMessage = "ID Jaminan tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdJaminan { get; set; }

        [Required(ErrorMessage = "Nama Jaminan tidak boleh kosong!")]
        public string NamaJaminan { get; set; }

        public virtual ICollection<Peminjaman1> Peminjaman1s { get; set; }
    }
}
