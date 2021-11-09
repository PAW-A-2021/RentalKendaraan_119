using System;
using System.Collections.Generic;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Gender1
    {
        public Gender1()
        {
            Customer1s = new HashSet<Customer1>();
        }

        public int IdGender { get; set; }
        public string NamaGender { get; set; }

        public virtual ICollection<Customer1> Customer1s { get; set; }
    }
}
