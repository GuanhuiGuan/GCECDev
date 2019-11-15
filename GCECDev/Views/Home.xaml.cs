using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GCECDev.Models;
using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            loadPhotosCarousel();
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
    }
}
