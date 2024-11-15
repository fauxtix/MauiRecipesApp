using System.Globalization;

namespace MauiRecipes.Converters
{
    public class BoolToFavoriteImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool favorite)
            {
                return favorite ? "favorite_filled.png" : "favorite_outline.png";
            }

            return "favorite_outline.png";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null; // Not needed in this scenario
        }
    }
}
