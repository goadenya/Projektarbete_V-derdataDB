using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projektarbete_VäderdataDB.Models
{
    class Temperature
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        [MaxLength(32)]
        public string Place { get; set; }
        
        [Column("Temperature")]
        public double? Temp { get; set; }

        public int? Humidity { get; set; }
    }
}
