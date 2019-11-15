using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCECDev.MasterDetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootMaster : ContentPage
    {
        public ListView ListView;

        public RootMaster()
        {
            InitializeComponent();

            BindingContext = new RootMasterViewModel();
            ListView = MenuItemsListView;

            GridSubtitle.BackgroundColor = Constants.Constants.MainButtonColor;
        }

        class RootMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<RootMenuItem> MenuItems { get; set; }

            public RootMasterViewModel()
            {
                MenuItems = new ObservableCollection<RootMenuItem>(new[]
                {
                    new RootMenuItem { Id = 0, Title = "Home" },
                    new RootMenuItem { Id = 1, Title = "Profile" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
