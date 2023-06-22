using System.Globalization;

namespace TestMauiMap.Converter;

public class RoundCornerRadiusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(value is double doubleValue) ? 0 : (object)(int)(doubleValue / 2);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}