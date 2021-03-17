using AttaxxPlus.Boosters;
using AttaxxPlus.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AttaxxPlus.ViewModel
{
    public class BoosterListViewModel : ObservableObject
    {
        public ObservableCollection<IBooster> Boosters = new ObservableCollection<IBooster>();

        public BoosterListViewModel(GameViewModel gameViewModel)
        {
            // EVIP: using reflection to find all non-abstract implementations of an interface
            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            // EVIP: using Linq for searching
            var allIBoosterTypes = currentAssembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IBooster)))
                .Where(t => !t.IsAbstract).ToList();
            foreach (var boosterType in allIBoosterTypes)
            {
                // EVIP: using reflection to instantiate objects
                IBooster booster = Activator.CreateInstance(boosterType.AsType()) as IBooster;
                booster.GameViewModel = gameViewModel;
				Boosters.Add(booster);
            }
        }
    }
}
