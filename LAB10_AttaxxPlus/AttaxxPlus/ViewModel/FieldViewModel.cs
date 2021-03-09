using AttaxxPlus.Model;

namespace AttaxxPlus.ViewModel
{
    // EVIP: derived from ObservableObject to have Notify()
    public class FieldViewModel : ObservableObject
    {
        public Field Model { get; internal set; }

        public FieldViewModel(Field model)
        {
            Model = model;
            // EVIP: VM observes Model and translates PropertyChanged events
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            // EVIP: forwarding INPC
            // EVIP: nameof operator
            if (e.PropertyName == nameof(Field.Owner))
                Notify(nameof(Owner));
        }

        // EVIP: read-only property. Needs to emit (forward)
        //  PropertyChanged if underlying model changes.
        public int Owner { get => Model.Owner; }

        public FieldCommand FieldCommand { get; set; }

        private bool isSelected = false;
        /// <summary>
        /// Note: this property is kept up-to-date by GameViewModel.
        /// Being a command tool, it has nothing to do with the
        /// game rules or game logic.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                // EVIP: setter checking for true change and calling
                //  derived Notify with automatic property name
                if (isSelected != value)
                {
                    isSelected = value;
                    // 3. feladat =true-nál csak false értékre futna le, illetve nem itt van a lénye, mivel
                    // ez általában mindig meghívódna, mivel az eredeti is állítja ezt és ott ezt negálva csinálja.
                    // GameViewModel-nél a FieldViewModel propertijének a setter-je a lényeg és ott is van egy notify,
                    // ami nem futna le, ha csak ennek a setter-je hívódna meg.
                    Notify();
                }
            }
        }
    }
}