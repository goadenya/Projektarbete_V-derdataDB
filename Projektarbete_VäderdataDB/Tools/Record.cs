using System;
using System.Collections.Generic;
using System.Text;

namespace Projektarbete_VäderdataDB.Models.Tools
{
    class Record
    {
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public decimal AvgTemp { get; set; }
        public decimal AvgHumidity { get; set; }
        public double MoldRisk { get; set; }
    }
}
