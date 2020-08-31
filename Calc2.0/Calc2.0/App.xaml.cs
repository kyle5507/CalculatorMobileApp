using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calc2._0
{
    public partial class App : Application
    {
        CalcViewModel cv;
        public App()
        {
            InitializeComponent();
            cv = new CalcViewModel();
            cv.RestoreState(Current.Properties);

            MainPage = new MainPage(cv);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            cv.SaveState(Current.Properties);
        }

        protected override void OnResume()
        {
        }
    }
}
