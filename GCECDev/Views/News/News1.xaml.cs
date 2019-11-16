using System;
using System.Collections.Generic;
using GCECDev.MasterDetailViews;
using Xamarin.Forms;

namespace GCECDev.Views.News
{
    public partial class News1 : ContentPage
    {
        public News1()
        {
            InitializeComponent();

            Title.Text = "NEWS 1";
            SubTitle.Text = "author: Jane Doe";
            Image0.Source = "maul_obi";
            Content.Text = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";

            Title.TextColor = Constants.Constants.MainButtonColor;
            SubTitle.TextColor = Constants.Constants.MainTextColor;
            Content.TextColor = Constants.Constants.MainTextColor;

            HomeButton.BackgroundColor = Constants.Constants.MainButtonColor;
            HomeButton.TextColor = Color.GhostWhite;
        }

        async void GoHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Root());
        }
    }
}
