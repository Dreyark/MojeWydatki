﻿using MojeWydatki.Models;
using MojeWydatki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseView : ContentPage
    {
        public ExpenseView()
        {
            InitializeComponent();
        }
    }
}