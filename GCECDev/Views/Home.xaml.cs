using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GCECDev.Models;
using GCECDev.Views.News;
using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            loadNewsCarousel();
            loadPhotosCarousel();

            BackgroundColor = Constants.Constants.BackgroundColor;

            setCarouselTitle(newsTitle, "  What's New?");
            newsCarouselView.HeightRequest = 250;

            setCarouselTitle(photosTitle, "  Photos of the Day");
            photoCarouselView.HeightRequest = 250;

            footer.Text = Constants.Constants.Footer;
        }

        private void setCarouselTitle(Label label, string text)
        {
            label.Text = text;
            label.TextColor = Constants.Constants.MainButtonColor;
            label.FontSize = 30;
            label.FontAttributes = FontAttributes.Bold;
        }

        private void loadNewsCarousel()
        {
            ObservableCollection<CarouselImageCaption> news = new ObservableCollection<CarouselImageCaption>
            {
                new CarouselImageCaption
                {
                    ImageSrc = "vader_luke",
                    Caption = "Darth Vader vs Luke Skywalker",
                    Next = "News0",
                    TapClickEventHandler = OnTappedJump
                },
                new CarouselImageCaption
                {
                    ImageSrc = "maul_obi",
                    Caption = "Darth Maul vs Obiwan Kenobi",
                    Next = "News1",
                    TapClickEventHandler = OnTappedJump
                },
            };
            newsCarouselView.ItemsSource = news;
        }

        private void loadPhotosCarousel()
        {
            ObservableCollection<CarouselImageCaption> photos = new ObservableCollection<CarouselImageCaption>
            {
                new CarouselImageCaption
                {
                    ImageSrc = "maul_obi",
                    Caption = "Darth Maul vs Obiwan Kenobi"
                },
                new CarouselImageCaption
                {
                    ImageSrc = "kylo",
                    Caption = "Kylo Ren vs Snowman"
                },
                new CarouselImageCaption
                {
                    ImageSrc = "vader_luke",
                    Caption = "Darth Vader vs Luke Skywalker"
                }
            };
            photoCarouselView.ItemsSource = photos;
        }

        async void OnTappedJump(object sender, EventArgs e)
        {
            var img = (CarouselImageCaption)sender;

            switch (img.Next)
            {
                case "News0":
                    await Navigation.PushModalAsync(new News0());
                    break;
                case "News1":
                    await Navigation.PushModalAsync(new News1());
                    break;
                default:
                    await DisplayAlert("Oops", "Content not found", "Home");
                    break;
            }
            
        }
    }
}
