using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class KondisiKendaraan1
    {
        public KondisiKendaraan1()
        {
            Pengembalian1s = new HashSet<Pengembalian1>();
        }

        [Required(ErrorMessage = "ID Kondisi tidak boleh kosong!")]
        public int IdKondisi { get; set; }

        [Required(ErrorMessage = "Nama Kondisi tidak boleh kosong!")]
        public string NamaKondisi { get; set; }

        public virtual ICollection<Pengembalian1> Pengembalian1s { get; set; }
    }
}
