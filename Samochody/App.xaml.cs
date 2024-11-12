using Samochody.api;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Samochody {
    public partial class App :Application {
        public static ApiService Service=new ApiService();
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
