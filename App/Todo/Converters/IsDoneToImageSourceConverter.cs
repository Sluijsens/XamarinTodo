using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo.Converters
{
    public class IsDoneToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var prefix = "";
            var suffix = "";
            var fileName = "ic_check_mark";
            var extension = ".png";
            var imageSource = new FileImageSource();

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    prefix = "Images/";
                    break;
            }
            
            if(value is bool isDone)
            {
                if (isDone == false)
                    suffix = "_gray";
            }

            imageSource.File = prefix + fileName + suffix + extension;

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
