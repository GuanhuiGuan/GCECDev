using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();

            footer.Text = Constants.Constants.Footer;
        }
    }
}
