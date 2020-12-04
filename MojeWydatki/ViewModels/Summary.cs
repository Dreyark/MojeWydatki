using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.ViewModels
{
    public class Summary
    {
        public DateTime Date { get; set; }
        public Double Value { get; set; }
        public Double Budget { get; set; }
        public Double[] ValuePerDay { get; set; }
    }
}
