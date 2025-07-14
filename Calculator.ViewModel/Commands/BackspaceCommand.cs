using Calculator.ViewModel.Constants;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class BackspaceCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;

        public BackspaceCommand(CalculatorViewModel viewModel)
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
            return !_viewModel.MainDisplay.Equals(CalculatorConstants.MainDisplayDefault) && !_viewModel.MainDisplay.Equals(CalculatorConstants.InvalidInputMessage);
        }

        public void Execute(object? parameter)
        {
            if (_viewModel.MainDisplay.Length == 1)
            {
                _viewModel.MainDisplay = CalculatorConstants.MainDisplayDefault;
                return;
            }

            var currentValue = decimal.Parse(_viewModel.MainDisplay);

            if (decimal.IsNegative(currentValue))
            {
                _viewModel.MainDisplay = CalculatorConstants.MainDisplayDefault;
                return;
            }

            _viewModel.MainDisplay = _viewModel.MainDisplay[..^1];
        }
    }
}
