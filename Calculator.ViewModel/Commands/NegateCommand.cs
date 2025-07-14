using Calculator.ViewModel.Constants;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class NegateCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;

        public NegateCommand(CalculatorViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            return !_viewModel.MainDisplay.Equals(CalculatorConstants.MainDisplayDefault) && !_viewModel.MainDisplay.Contains(CalculatorConstants.InvalidInputMessage);
        }

        public void Execute(object? parameter)
        {
            var currentValue = decimal.Parse(_viewModel.MainDisplay);

            _viewModel.MainDisplay = decimal.Negate(currentValue).ToString();
        }
    }
}
