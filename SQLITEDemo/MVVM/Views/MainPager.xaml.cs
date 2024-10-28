 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLITEDemo.MVVM.ViewModels;

namespace SQLITEDemo.MVVM.Views;

public partial class MainPager : ContentPage
{
    public MainPager()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}