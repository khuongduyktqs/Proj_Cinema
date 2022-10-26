namespace ProjCinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DISCOUNT_CODE
    {
        [Key]
        [StringLength(10)]
        public string CodeID { get; set; }

        public int? DiscountNumber { get; set; }

        public int? State { get; set; }
    }
}
