using Calculator.ViewModel.Constants;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class UpdateDisplayCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;

        public UpdateDisplayCommand(CalculatorViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter) => !_viewModel.MainDisplay.Contains(CalculatorConstants.InvalidInputMessage);

        public void Execute(object? parameter)
        {
            if (parameter is not string input)
                return;

            UpdateDisplay(input);
        }

        private void UpdateDisplay(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            if (input is ".")
            {
                AppendDecimalPoint();
                return;
            }

            UpdateNumber(input);
        }

        private void AppendDecimalPoint()
        {
            if (_viewModel.MainDisplay.Contains('.'))
                return;

            _viewModel.MainDisplay += ".";
        }

        private void UpdateNumber(string number)
        {
            if (_viewModel.MainDisplay == CalculatorConstants.MainDisplayDefault)
            {
                _viewModel.MainDisplay = number;
                return;
            }

            _viewModel.MainDisplay += number;
        }
    }
}
