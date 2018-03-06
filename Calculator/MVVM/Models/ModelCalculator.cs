using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Calculator.MVVM.Models
{
    public class ModelCalculator : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties

        public enum Operators { None, Addition, Subtraction, Multiplication, Division, SquareRoot };

        private string _mainDisplay;
        private Func<List<double>, double> _calculateFunction;
        private int _selectedOperator;
        private List<double> _valuesToCalculate;
        private string _calculationLabel;
        private double? _lastValueUsed;

        public double? LastValueUsed
        {
            get { return _lastValueUsed; }
            set
            {
                _lastValueUsed = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.LastValueUsed);
            }
        }


        public string CalculationLabel
        {
            get { return _calculationLabel; }
            set
            {
                _calculationLabel = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.CalculationLabel);
            }
        }


        public List<double> ValuesToCalculate
        {
            get { return _valuesToCalculate; }
            set
            {
                _valuesToCalculate = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.ValuesToCalculate);
            }
        }


        public int SelectedOperator
        {
            get { return _selectedOperator; }
            set
            {
                _selectedOperator = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.SelectedOperator);
            }
        }

        public Func<List<double>, double> CalculateFunction
        {
            get { return _calculateFunction; }
            set
            {
                _calculateFunction = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.Calculate);
            }
        }

        public string MainDisplay
        {
            get { return _mainDisplay; }
            set
            {
                _mainDisplay = value;
                OnPropertyChanged(Helpers.Constants.ModelProperties.MainDisplay);
            }
        }


        #endregion Properties
    }
}
