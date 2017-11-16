using CD03_Mensik.DataSimulation;
using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CD03_Mensik.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {

        private Simulator sim;

        public List<ItemVm> ModelList = new List<ItemVm>();

        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }

        public ObservableCollection<string> ModeSelection { get; private set; }

        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();


        

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }


        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }



        public MainViewModel()
        {

            ActorList = new ObservableCollection<ItemVm>();
            SensorList = new ObservableCollection<ItemVm>();
            ModeSelection = new ObservableCollection<string>();

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelection.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {

                ModeSelection.Add(item);
            }


            



        }

        public void GenerateData()
        {
            sim = new Simulator(ModelList);

            foreach (var item in sim.Items)
            {

                if (item.ItemType.Equals(typeof(ISensor)))
                {

                    SensorList.Add(item);
                }

                else if (item.ItemType.Equals(typeof(IActuator)))
                {

                    ActorList.Add(item);
                }

            }

            


        }

    }

}