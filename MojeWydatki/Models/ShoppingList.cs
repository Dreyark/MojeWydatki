using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class ShoppingList : SModel
    {
        public String ListName { get; set; }

        public String Products { get; set; }

        public ShoppingList()
        {
        }
    }
}
