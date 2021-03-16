using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AttaxxPlus.View
{
    public class IsSelected2BrushConverter : IValueConverter
    {
        // EVIP: reusing brushes, named constants
        readonly private static SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        readonly private static SolidColorBrush gray = new SolidColorBrush(Colors.Gray);
        readonly private static SolidColorBrush aqua = new SolidColorBrush(Colors.Aqua);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? aqua : gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
