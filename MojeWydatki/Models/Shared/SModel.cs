using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models.Shared
{
    public class SModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
