using Calculator.Helpers.Command_Classes;
using Calculator.MVVM.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Calculator_Methods;
using Calculator_Methods.Calculation_Classes;
using System;
using System.Runtime.Remoting.Messaging;
using Calculator.Calculations;
using Calculator.Helpers.Operators;

namespace Calculator.MVVM.ViewModels
{
    public class ViewModelCalculator : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties 

        private ModelCalculator _modelCalculator;
        private ICommand _enterIntoMainDisplayCommand;
        private ICommand _negateMainDisplayCommand;
        private ICommand _backspaceMainDisplayCommand;
        private ICommand _operaterPressedCommand;
        private ICommand _calculateCommand;
        private ICommand _clearAll;

        public ICommand ClearAllCommand
        {
            get { return _clearAll; }
            set
            {
                _clearAll = value;
                OnPropertyChanged(Helpers.Constants.Commands.ClearAllCommand);
            }
        }

        public ICommand CalculateCommand
        {
            get { return _calculateCommand; }
            set
            {
                _calculateCommand = value;
                OnPropertyChanged(Helpers.Constants.Commands.CalculateCommand);
            }
        }

        public ICommand OperatorPressedCommand
        {
            get { return _operaterPressedCommand; }
            set
            {
                _operaterPressedCommand = value;
                OnPropertyChanged(Helpers.Constants.Commands.OperatorPressedCommand);
            }
        }

        public ICommand BackspaceMainDisplayCommand
        {
            get { return _backspaceMainDisplayCommand; }
            set
            {
                _backspaceMainDisplayCommand = value;
                OnPropertyChanged(Helpers.Constants.Commands.BackspaceMainDisplayCommand);
            }
        }


        public ICommand NegateMainDisplayCommand
        {
            get { return _negateMainDisplayCommand; }
            set
            {
                _negateMainDisplayCommand = value;
                OnPropertyChanged(Helpers.Constants.Commands.NegateMainDisplayCommand);
            }
        }


        public ICommand EnterIntoMainDisplayCommand
        {
            get { return _enterIntoMainDisplayCommand; }
            set
            {
                _enterIntoMainDisplayCommand = value;
                OnPropertyChanged(Helpers.Constants.Commands.EnterIntoMainDisplayCommand);
            }
        }

        public ModelCalculator ModelCalculator
        {
            get { return _modelCalculator; }
            set
            {
                _modelCalculator = value;
                OnPropertyChanged(Helpers.Constants.ViewModelProperties.ModelCalculator);
            }
        }

        #endregion Properties

        #region Constructor 

        public ViewModelCalculator()
        {
            InitializeModelInstance();
            InitializeCommands();
        }

        #endregion Constructor

        #region Initialization

        private void InitializeModelInstance()
        {
            ModelCalculator = new ModelCalculator() { MainDisplay = "0", CalculationLabel = "", ValuesToCalculate = new List<double>(), SelectedOperator = (int)ModelCalculator.Operators.None, LastValueUsed = null };
        }

        private void InitializeCommands()
        {
            EnterIntoMainDisplayCommand = new RelayCommandWithParameter(EnterIntoMainDisplayCommand_Execute, EnterIntoMainDisplayCommand_CanExecute);
            NegateMainDisplayCommand = new RelayCommand(NegateMainDisplayCommand_Execute, NegateMainDisplayCommand_CanExecute);
            BackspaceMainDisplayCommand = new RelayCommand(BackspaceMainDisplayCommand_Execute, BackspaceMainDisplayCommand_CanExecute);
            OperatorPressedCommand = new RelayCommandWithParameter(OperatorPressedCommand_Execute, OperatorPressedCommand_CanExecute);
            CalculateCommand = new RelayCommand(CalculateCommand_Execute, CalculateCommand_CanExecute);
            ClearAllCommand = new RelayCommand(ClearAllCommand_Execute, ClearAllCommand_CanExecute);
        }

        #endregion Initialization

        #region Command Methods

        private bool ClearAllCommand_CanExecute()
        {
            return true;
        }

        private void ClearAllCommand_Execute()
        {
            ModelCalculator = new ModelCalculator() { CalculateFunction = null, CalculationLabel = string.Empty, LastValueUsed = null, MainDisplay = "0", SelectedOperator = (int)ModelCalculator.Operators.None, ValuesToCalculate = new List<double>() };
        }

        private bool CalculateCommand_CanExecute()
        {
            bool canExecute = false;

            if (ModelCalculator.SelectedOperator != (int)ModelCalculator.Operators.None)
                canExecute = true;

            return canExecute;
        }

        private void CalculateCommand_Execute()
        {
            var currentValueDisplayed = double.Parse(ModelCalculator.MainDisplay);
            var functionCalledFrom = "EqualsButton";

            AddValueToCalculationArray(currentValueDisplayed);
            SetLastUsedValue(currentValueDisplayed);
            BeginCalculation(functionCalledFrom);
        }

        private bool OperatorPressedCommand_CanExecute()
        {
            return true;
        }

        private void OperatorPressedCommand_Execute(object operationButtonPressed)
        {
            var button = operationButtonPressed as Button;
            var buttonContent = button.Content;
            var functionCalledFrom = "OperatorButton";

            NullifyLastValueUsed();

            ModelCalculator.SelectedOperator = Operators.GetOperator(buttonContent);
            ModelCalculator.CalculateFunction = CalculateClassFactory.GetOperationClass(ModelCalculator.SelectedOperator).Calculate;

            AddValueToCalculationArray(double.Parse(ModelCalculator.MainDisplay));
            ResetMainDisplay();

            ModelCalculator.CalculationLabel = ModelCalculator.ValuesToCalculate[ModelCalculator.ValuesToCalculate.Count - 1] + " " + buttonContent;

            if (ModelCalculator.SelectedOperator == (int)ModelCalculator.Operators.SquareRoot)
                BeginCalculation(functionCalledFrom);
        }

        private bool BackspaceMainDisplayCommand_CanExecute()
        {
            bool canExecute = false;

            if (!ModelCalculator.MainDisplay.Equals("0"))
                canExecute = true;

            return canExecute;
        }

        private void BackspaceMainDisplayCommand_Execute()
        {
            BackspaceMainDisplayValue();
        }

        private bool NegateMainDisplayCommand_CanExecute()
        {
            bool canExecute = false;

            if (!string.IsNullOrEmpty(ModelCalculator.MainDisplay) && !ModelCalculator.MainDisplay.Equals("0"))
                canExecute = true;

            return canExecute;
        }

        private void NegateMainDisplayCommand_Execute()
        {
            NegateMainDisplayValue();
        }

        private bool EnterIntoMainDisplayCommand_CanExecute()
        {
            return true;
        }

        private void EnterIntoMainDisplayCommand_Execute(object pressedButton)
        {
            var buttonPressedByUser = pressedButton as Button;
            EnterIntoMainDisplay(buttonPressedByUser.Content);
        }

        #endregion Command Methods

        #region Logic Methods - Interaction and manipulation of Model Instance within ViewModel

        private void BeginCalculation(string functionCalledFrom)
        {
            ModelCalculator.CalculateFunction.BeginInvoke(ModelCalculator.ValuesToCalculate, CalculateFunctionCallback, functionCalledFrom);
        }

        private void CalculateFunctionCallback(IAsyncResult res)
        {
            AsyncResult result = res as AsyncResult;
            var caller = result.AsyncDelegate as Func<List<double>, double>;
            var functionCalledFrom = result.AsyncState as string;

            if (functionCalledFrom.Equals("EqualsButton"))
                ModelCalculator.CalculationLabel = "";

            ModelCalculator.MainDisplay = caller.EndInvoke(result).ToString();
        }

        private void NullifyLastValueUsed()
        {
            ModelCalculator.LastValueUsed = null;
        }

        private void ResetMainDisplay()
        {
            ModelCalculator.MainDisplay = "0";
        }

        private void SetLastUsedValue(double value)
        {
            if (ModelCalculator.LastValueUsed == null)
                ModelCalculator.LastValueUsed = value; 
            else
                AddLastValueUsedToCalculationArray();
        }       

        private void AddValueToCalculationArray(double value)
        {
            ModelCalculator.ValuesToCalculate.Add(value);
        }

        private void AddLastValueUsedToCalculationArray()
        {
            ModelCalculator.ValuesToCalculate.Add(ModelCalculator.LastValueUsed.Value);
        }

        private void BackspaceMainDisplayValue()
        {
            if (ModelCalculator.MainDisplay.Length == 1 || (ModelCalculator.MainDisplay.StartsWith("-") && ModelCalculator.MainDisplay.Length == 2))
                ModelCalculator.MainDisplay = "0";
            else
                ModelCalculator.MainDisplay = ModelCalculator.MainDisplay.Substring(0, ModelCalculator.MainDisplay.Length - 1);
        }

        private void NegateMainDisplayValue()
        {
            var currentDisplayText = ModelCalculator.MainDisplay;

            if (!ModelCalculator.MainDisplay.StartsWith("-"))
                ModelCalculator.MainDisplay = "-" + currentDisplayText;
            else
                ModelCalculator.MainDisplay = currentDisplayText.Substring(1, ModelCalculator.MainDisplay.Length - 1);
        }

        private void EnterIntoMainDisplay(object contentOfButtonPressedByUser)
        {
            string buttonText = contentOfButtonPressedByUser.ToString();

            if (buttonText.Equals("."))
                EnterPeriodIntoMainDisplay(buttonText);
            else
                EnterNumberIntoMainDisplay(buttonText);
        }

        private void EnterNumberIntoMainDisplay(string buttonText)
        {
            if (ModelCalculator.MainDisplay.Equals("0"))
                ModelCalculator.MainDisplay = buttonText;
            else
                ModelCalculator.MainDisplay += buttonText;
        }

        private void EnterPeriodIntoMainDisplay(string buttonText)
        {
            if (!ModelCalculator.MainDisplay.Contains("."))
                ModelCalculator.MainDisplay += buttonText;
        }

        #endregion Logic Methods - Interaction and manipulation of Model Instance within ViewModel
    }
}
