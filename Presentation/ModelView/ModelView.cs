using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Presentation.Model;
using Presentation.ModelView.MVVMCore;
using Data;

namespace Presentation.ModelView
{
    public class ModelView : BaseViewModel, INotifyPropertyChanged
    {
        private readonly MainAPI modelLayer;
        private string _ballCount = "";
        private bool _isRunning = false;
        private bool _canClear = false;

        public ModelView()
        {
            modelLayer = MainAPI.GenerateTable(1000, 600);
            InitializeCommands();
        }

        public double CanvasWidth { get; set; } = 1000;
        public double CanvasHeight { get; set; } = 600;

        public string BallCount
        {
            get => _ballCount;
            set { _ballCount = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<BallModelAPI> Balls => modelLayer.Balls;

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddBalls, () => !_canClear);
            ClearCommand = new RelayCommand(ClearBalls, () => _canClear);
            StartCommand = new RelayCommand(StartSimulation, () => !_isRunning);
            StopCommand = new RelayCommand(StopSimulation, () => _isRunning);
        }

        private void AddBalls()
        {
            try
            {
                var count = int.Parse(_ballCount);
                if (count < 0) throw new ArgumentException();

                _canClear = true;
                modelLayer.GenerateBalls(count);
                UpdateCommandStates();
            }
            catch
            {
                BallCount = "";
            }
        }

        private void StartSimulation()
        {
            _isRunning = true;
            modelLayer.Update();
            UpdateCommandStates();
        }

        private void StopSimulation()
        {
            _isRunning = false;
            modelLayer.PauseMovement();
            UpdateCommandStates();
        }

        private void ClearBalls()
        {
            modelLayer.ClearMap();
            _isRunning = false;
            _canClear = false;
            BallCount = "";
            UpdateCommandStates();
        }

        private void UpdateCommandStates()
        {
            AddCommand.RaiseCanExecuteChanged();
            ClearCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }
    }
}