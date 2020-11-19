using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MojeWydatki.ViewModels
{
    public class DebtListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public ObservableCollection<ListDebt> DebtList { get; set; }

        DebtRepository DebtRep;

        public DebtListViewModel()
        {
            DebtRep = new DebtRepository();
        }
        public async Task MakeDebtList()
        {
            DebtList = new ObservableCollection<ListDebt>();
            var iList = await DebtRep.GetDebtsAsync();

            foreach (Debt i in iList)
            {
                DebtList.Add(new ListDebt()
                {
                    Debt = i,
                    Color = i.AmILender ? "Green" : "Red"
                }) ;
            }
        }
    }

    public class ListDebt
    {
        public Debt Debt { get; set; }
        public String Color { get; set; }
        public ListDebt()
        {

        }
    }
}
