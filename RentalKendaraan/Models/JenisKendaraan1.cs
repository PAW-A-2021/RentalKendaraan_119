using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class JenisKendaraan1
    {
        public JenisKendaraan1()
        {
            Kendaraan1s = new HashSet<Kendaraan1>();
        }

        [Required(ErrorMessage = "ID Jenis Kendaraan tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdJenisKendaraan { get; set; }

        [Required(ErrorMessage = "Nama Jenis Kendaraan tidak boleh kosong!")]
        public string NamaJenisKendaraan { get; set; }

        public virtual ICollection<Kendaraan1> Kendaraan1s { get; set; }
    }
}
