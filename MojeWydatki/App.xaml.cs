﻿using MojeWydatki.Data;
using MojeWydatki.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki
{
    public partial class App : Application
    {

        static Database database;
        PlannedExpenseJob plannedExpenseJob;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyExpensesDb.db3"));
                }
                return database;
            }
        }

        public static string DbPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyExpensesDb.db3");
            }
        }

        public App()
        {
            plannedExpenseJob = new PlannedExpenseJob();
            InitializeComponent();
            MainPage = new AppMasterDetailPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
