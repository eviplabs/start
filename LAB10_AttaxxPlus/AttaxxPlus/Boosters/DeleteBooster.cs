using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    public class DeleteBooster : BoosterBase
    {
        private int usableCounter = 1;
        private int usableCounterPlayer1 = 1;
        private int usableCounterPlayer2 = 1;

        public DeleteBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/unicorn.png"));
        }
        public override string Title { get => $"Unicorn ({usableCounter})"; }

        public override void InitializeGame()
        {
            usableCounter = 1;
            usableCounterPlayer1 = 1;
            usableCounterPlayer2 = 1;
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();

            if (GameViewModel.CurrentPlayer == 1)
            {
                usableCounter = usableCounterPlayer1;
            }
            else
            {
                usableCounter = usableCounterPlayer2;
            }

            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if(usableCounter > 0)
            {
                usableCounter = 0;

                if (GameViewModel.CurrentPlayer == 1)
                {
                    usableCounterPlayer1 = usableCounter;
                }
                else
                {
                    usableCounterPlayer2 = usableCounter;
                }

                foreach (var i in GameViewModel.Model.Fields)
                { 
                    if(i.Row == selectedField.Row)
                        i.Owner = 0;
                
                    if(i.Column == selectedField.Column)
                        i.Owner = 0;
                }
            }

            Notify(nameof(Title));
            return true;
        }
    }
}
