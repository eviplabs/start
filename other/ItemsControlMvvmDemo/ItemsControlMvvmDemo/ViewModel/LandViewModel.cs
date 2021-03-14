using ItemsControlMvvmDemo.Model;
using System.Linq;
using System;

namespace ItemsControlMvvmDemo.ViewModel
{
    public class LandViewModel
    {
        private Land land;

        public LandViewModel(Land land)
        {
            this.land = land;
        }

        public string LandNameAndCapital =>
            $"{land.Name} ({land.Cities.Single(c=>c.IsCapital)})";

        public string HufOrOther =>
            (land.Currency == "HUF") ? "HUF" : "Other";
    }
}
