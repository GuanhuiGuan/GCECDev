using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCECDev.Models;
using GCECDev.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCECDev.MasterDetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Root : MasterDetailPage
    {
        public Root()
        {
            InitializeComponent();

            Detail = new NavigationPage(new Home());
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootMenuItem;
            if (item == null)
            {
                // DisplayAlert("Internal Error", "Null MasterDetail Item", "To Home");
                return;
            }

            // var page = (Page)Activator.CreateInstance(item.TargetType);
            Page page;

            switch (item.Id)
            {
                case 0:
                    page = new Home();
                    break;
                case 1:
                    page = new Profile();
                    break;
                case -1:
                    App.User = new User();
                    Navigation.PushModalAsync(new Login());
                    return;
                default:
                    // DisplayAlert("Internal Error", "Unknown MasterDetail Item", "To Home");
                    page = new Home();
                    break;
            }
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
