using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AttaxxPlus.Model
{
    // EVIP: base class for INPC, Notify using CallerMemberName
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = "")
        {
            // EVIP: ?. for invoking event. Event is null if there are not subscribers.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
