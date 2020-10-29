using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeWydatki.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        public INavigation Navigation { get; set; }
    }
}
