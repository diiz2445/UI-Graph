using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Graph.Core;

namespace UI_Graph.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SmejnViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public SmejnViewModel SmejnVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); } 
            
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            SmejnVM = new SmejnViewModel();


            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView =HomeVM; });
            SmejnViewCommand = new RelayCommand(o => { CurrentView =SmejnVM; });
        }

    }
}
