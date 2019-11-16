using System;
using Xamarin.Forms;

namespace GCECDev.Models
{
    public class CarouselImageCaption
    {
        public string ImageSrc { get; set; }
        public string Caption { get; set; }
        public string Next { get; set; }
        public Command TapCommand { get; set; }
        public EventHandler TapClickEventHandler;


        public CarouselImageCaption()
        {
            TapCommand = new Command(() => OnImageClicked());
        }

        public void OnImageClicked()
        {
            TapClickEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
