using MojeWydatki.Data;
using MojeWydatki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    class DebtViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DebtRepository debtRep;
        public DebtViewModel()
        {
            debtRep = new DebtRepository();
            SaveDebtCommand = new Command(async () =>
            {
                var debt = new Debt();
                debt.Person = ThePerson;
                debt.Description = TheDescription;
                debt.DebtValue = Convert.ToDouble(TheDebtValue);
                debt.CurrentValue = 0;
                debt.BorrowDate = TheBorrowDate;
                debt.DateOfDelivery = TheDateOfDelivery;
                debt.AmILender = TheAmILender;
                debt.Progress = debt.CurrentValue / debt.DebtValue;
                await debtRep.SaveDebtAsync(debt);
            });
        }

        public DebtViewModel(Debt debt)
        {
            debtRep = new DebtRepository();

            ThePerson = debt.Person;
            TheDescription = debt.Description;
            TheCurrentValue = Convert.ToString(debt.CurrentValue);
            TheBorrowDate = debt.BorrowDate;
            TheDateOfDelivery = debt.DateOfDelivery;
            TheAmILender = debt.AmILender;

            UpdateDebtCommand = new Command(async () =>
            {
                debt.CurrentValue += Convert.ToDouble(TheAddValue);
                debt.Progress = debt.CurrentValue / debt.DebtValue;
                if (debt.Progress > 1)
                {
                    debt.DateOfDelivery = DateTime.Now;
                }
                await debtRep.SaveDebtAsync(debt);
                ThePerson = string.Empty;
                TheDescription = string.Empty;
                TheCurrentValue = string.Empty;
                TheDebtValue = string.Empty;
                TheAmILender = false;
                TheDateOfDelivery = DateTime.MinValue;
                TheBorrowDate = DateTime.MinValue;
            });


            RemoveDebt = new Command(async () =>
            {
                await debtRep.DeleteDebtAsync(debt);
            });
        }

        string person;
        public string ThePerson
        {
            get => person;
            set
            {
                person = value;
                var args = new PropertyChangedEventArgs(nameof(ThePerson));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string description;
        public string TheDescription
        {
            get => description;
            set
            {
                description = value;
                var args = new PropertyChangedEventArgs(nameof(TheDescription));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string currentValue;
        public string TheCurrentValue
        {
            get => currentValue;
            set
            {
                this.currentValue = value;
                var args = new PropertyChangedEventArgs(nameof(TheCurrentValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string debtValue;
        public string TheDebtValue
        {
            get => debtValue;
            set
            {
                this.debtValue = value;
                var args = new PropertyChangedEventArgs(nameof(TheDebtValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        DateTime borrowDate;
        public DateTime TheBorrowDate
        {
            get => borrowDate;
            set
            {
                this.borrowDate = value;
                var args = new PropertyChangedEventArgs(nameof(TheBorrowDate));
                PropertyChanged?.Invoke(this, args);
            }
        }

        String addValue;
        public String TheAddValue
        {
            get => addValue;
            set
            {
                this.addValue = value;
                if (this.addValue == "" || this.addValue == "," || this.addValue == "." || this.addValue == null)
                    {
                    this.addValue = "0";
                }
                else
                {
                    if (this.addValue.Last() == '.')
                    {
                        this.addValue = this.addValue.Remove(this.addValue.Length - 1);
                    }
                }
                var args = new PropertyChangedEventArgs(nameof(TheAddValue));
                PropertyChanged?.Invoke(this, args);
            }
        }

        DateTime dateOfDelivery;
        public DateTime TheDateOfDelivery
        {
            get => dateOfDelivery;
            set
            {
                this.dateOfDelivery = value;
                var args = new PropertyChangedEventArgs(nameof(TheDateOfDelivery));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public bool amILender;
        public bool TheAmILender
        {
            get => amILender;
            set
            {
                amILender = value;
                var args = new PropertyChangedEventArgs(nameof(TheAmILender));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public Command SaveDebtCommand { get; }
        public Command UpdateDebtCommand { get; }
        public Command RemoveDebt { get; }
    }
}
