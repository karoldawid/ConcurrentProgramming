using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Presentation.Model;
using Presentation.ModelView.MVVMCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;
using System.Windows;

namespace Presentation.ModelView
{
    public class ModelView : BaseViewModel, INotifyPropertyChanged
    {
        private MainAPI modelLayer;

        private string _ballCount = "";
        public double _tableWidth = 1000;
        public double _tableHeight = 600;
        private double _canvasHeight = 600;
        private double _canvasWidth = 1000;

        private bool _isRunning = false;
        private bool _canClear = false;

        public ModelView()
        {
            this.modelLayer = MainAPI.GenerateTable(_tableWidth, _tableHeight);

            AddCommand = new RelayCommand(AddBalls, () => !_canClear);
            ClearCommand = new RelayCommand(ClearBalls, () => _canClear);
            StartCommand = new RelayCommand(StartSimulation, () => !_isRunning);
            StopCommand = new RelayCommand(StopSimulation, () => _isRunning);
        }

        public ObservableCollection<BallModelAPI> Balls => this.modelLayer.Balls;

        public RelayCommand AddCommand { get; }
        public RelayCommand ClearCommand { get; }
        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }

        public double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                _canvasWidth = value;
                RaisePropertyChanged();
            }

        }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                RaisePropertyChanged();
            }

        }

        public string BallCount
        {
            get => _ballCount;
            set
            {
                _ballCount = value;
                RaisePropertyChanged();
            }
        }

        private void AddBalls()
        {
            try
            {
                int count = int.Parse(_ballCount);
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