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
        // 1. feladat saját szín beállítása, talán az alap sárga látszik a legjobban
        readonly private static SolidColorBrush owncolor = new SolidColorBrush(Colors.Wheat);
        readonly private static SolidColorBrush gray = new SolidColorBrush(Colors.Gray);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? owncolor : gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
