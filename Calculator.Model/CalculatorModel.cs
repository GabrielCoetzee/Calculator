using System.ComponentModel;

namespace Calculator.Model
{
    public class CalculatorModel : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties

        private decimal? _firstValue;
        private decimal? _secondValue;

        public decimal? FirstValue
        {
            get => _firstValue;
            set
            {
                _firstValue = value;
                OnPropertyChanged(nameof(FirstValue));
            }
        }

        public decimal? SecondValue
        {
            get => _secondValue;
            set
            {
                _secondValue = value;
                OnPropertyChanged(nameof(SecondValue));
            }
        }

        #endregion Properties
    }
}
