using Samochody.api;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Samochody {
    public partial class App :Application {
        private static ApiService ApiService = null;
        public static ApiService Service {
            get {
                if(ApiService == null)
                    ApiService = new ApiService();
                return ApiService;
            }
        }

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
