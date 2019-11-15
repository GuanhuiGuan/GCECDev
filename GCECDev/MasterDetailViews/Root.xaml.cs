using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);

            switch (item.Id)
            {
                case 0:
                    page = new SignUp();
                    break;
            }

            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
