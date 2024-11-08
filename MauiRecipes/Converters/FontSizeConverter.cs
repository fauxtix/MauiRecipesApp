using System.Globalization;

namespace MauiRecipes.Converters
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Ensure the value is a string and check its length
            if (value is string title && title.Length > 70)
            {
                return 14; // Font size for long titles
            }
            return 12; // Font size for short titles
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
