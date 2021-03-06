﻿using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpenseView : ContentPage
    {
        public AddExpenseView()
        {
            InitializeComponent();
            ExpenseDate.Date = DateTime.Now;
            ExpenseTime.Time = DateTime.Now.TimeOfDay;
        }
        void Value_Changed(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            if (e.NewTextValue == null)
            {

            }
            else
            {
                bool is2 = Regex.IsMatch(Value.Text, @"^[0-9]+((\.|\,)[0-9]{0,2})?$|^$");
                if (!is2)
                {

                    Value.Text = e.OldTextValue;

                }
            }
        }

        public void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}