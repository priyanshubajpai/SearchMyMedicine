using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMine.Models
{
    public class StoreAddressValidation
    {
        [Required]
        public string AddressLine { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PinCode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }

    public class AddMedicineValidation
    {
        [Required]
        public string MedicineName { get; set; }

        [Required]
        public string MedicineType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }
    public class SearchMedicineValidation
    {
        [Required(ErrorMessage ="Required!!")]
        public string SearchKeyword { get; set; }
    }

    public class DisplaySearch
    {
        public string MedicineType { get; set; }
        public DateTime Expiry { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Address { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}