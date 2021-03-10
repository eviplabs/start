using System.Collections.ObjectModel;

namespace AttaxxPlus.ViewModel
{
    // EVIP: need to bind to a generic container, using wrapper class
    //  without additional content. We cannot bind an ItemControl element
    //  to List<FieldViewModel> so we create a wrapper class.
    public class FieldViewModelList : ObservableCollection<FieldViewModel>
    {
    }
}
