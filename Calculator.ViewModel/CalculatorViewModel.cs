using System.ComponentModel;
using System.Windows.Input;
using Calculator.Model;
using Calculator.ViewModel.Commands;
using Calculator.Model.Enums;
using Calculator.ViewModel.Constants;
using Calculator.BusinessLogic.Services;
using Calculator.Model.Extensions;
using System.Globalization;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties 

        private CalculatorModel _calculatorModel;
        private ICommand _updateDisplayCommand;
        private ICommand _negateCommand;
        private ICommand _backspaceCommand;
        private ICommand _selectOperatorCommand;
        private ICommand _calculateCommand;
        private ICommand _clearAllCommand;
        private string _mainDisplay;
        private Operator _selectedOperator;

        public string MainDisplay
        {
            get => _mainDisplay;
            set
            {
                _mainDisplay = value;
                OnPropertyChanged(nameof(MainDisplay));

                (BackspaceCommand as BackspaceCommand)?.RaiseCanExecuteChanged();
                (NegateCommand as NegateCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string CalculationHistory
        {
            get
            {
                if (SelectedOperator == Operator.None)
                    return string.Empty;

                if (SelectedOperator == Operator.SquareRoot)
                    return "√ " + "( " + CalculatorModel.FirstValue.GetValueOrDefault().ToString() + " ) ";

                if (CalculatorModel.FirstValue.HasValue && !CalculatorModel.SecondValue.HasValue)
                    return CalculatorModel.FirstValue.Value.ToString() + " " + SelectedOperator.GetDisplayName();

                return CalculatorModel.FirstValue.Value.ToString() + " " + SelectedOperator.GetDisplayName() + " " + CalculatorModel.SecondValue.Value.ToString() + " = ";

            }
        }

        public Operator SelectedOperator
        {
            get => _selectedOperator;
            set
            {
                _selectedOperator = value;
                OnPropertyChanged(nameof(SelectedOperator));
                OnPropertyChanged(nameof(CalculationHistory));

                (CalculateCommand as CalculateCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand ClearAllCommand
        {
            get => _clearAllCommand;
            set
            {
                _clearAllCommand = value;
                OnPropertyChanged(nameof(ClearAllCommand));
            }
        }

        public ICommand CalculateCommand
        {
            get => _calculateCommand;
            set
            {
                _calculateCommand = value;
                OnPropertyChanged(nameof(CalculateCommand));
                OnPropertyChanged(nameof(CalculationHistory));
            }
        }

        public ICommand SelectOperatorCommand
        {
            get => _selectOperatorCommand;
            set
            {
                _selectOperatorCommand = value;
                OnPropertyChanged(nameof(SelectOperatorCommand));
            }
        }

        public ICommand BackspaceCommand
        {
            get => _backspaceCommand;
            set
            {
                _backspaceCommand = value;
                OnPropertyChanged(nameof(BackspaceCommand));
            }
        }


        public ICommand NegateCommand
        {
            get => _negateCommand;
            set
            {
                _negateCommand = value;
                OnPropertyChanged(nameof(NegateCommand));
            }
        }


        public ICommand UpdateDisplayCommand
        {
            get => _updateDisplayCommand;
            set
            {
                _updateDisplayCommand = value;
                OnPropertyChanged(nameof(UpdateDisplayCommand));
                OnPropertyChanged(nameof(CalculationHistory));
            }
        }

        public CalculatorModel CalculatorModel
        {
            get => _calculatorModel;
            set
            {
                _calculatorModel = value;
                OnPropertyChanged(nameof(CalculatorModel));
            }
        }

        #endregion Properties

        public CalculatorViewModel(ICalculationService calculationService)
        {
            SelectOperatorCommand = new SelectOperatorCommand(this, calculationService);
            CalculateCommand = new CalculateCommand(this, calculationService);
            ClearAllCommand = new ClearAllCommand(this);
            BackspaceCommand = new BackspaceCommand(this);
            NegateCommand = new NegateCommand(this);
            UpdateDisplayCommand = new UpdateDisplayCommand(this);

            MainDisplay = CalculatorConstants.MainDisplayDefault;

            CalculatorModel = new CalculatorModel() { FirstValue = null, SecondValue = null };
        }
    }
}
