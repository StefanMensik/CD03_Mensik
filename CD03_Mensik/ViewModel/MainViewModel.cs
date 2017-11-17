using CD03_Mensik.DataSimulation;
using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CD03_Mensik.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {

        private Simulator sim;

        public List<ItemVm> ModelList = new List<ItemVm>();

        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }

        public ObservableCollection<string> SensorModeSelection { get; private set; }
        public ObservableCollection<string> ActorModeSelection { get; private set; }

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
            SensorModeSelection = new ObservableCollection<string>();
            ActorModeSelection = new ObservableCollection<string>();

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {

                //pfusch
                if (item == "Auto" || item == "Manual") break;
                SensorModeSelection.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {

                ActorModeSelection.Add(item);
            }

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 30);
            Timer.Tick += UpdateTime;
            Timer.Start();

            GenerateData();



        }

        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();


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