using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Gender1
    {
        public Gender1()
        {
            Customer1s = new HashSet<Customer1>();
        }

        [Required(ErrorMessage = "ID Gender tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdGender { get; set; }

        [Required(ErrorMessage = "Nama Gender tidak boleh kosong!")]
        public string NamaGender { get; set; }

        public virtual ICollection<Customer1> Customer1s { get; set; }
    }
}
