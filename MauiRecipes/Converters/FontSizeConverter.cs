using System.Globalization;

namespace MauiRecipes.Converters
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string title && title.Length > 60)
            {
                return 12; // Font size for long titles
            }

            return 15; // Font size for short titles
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
