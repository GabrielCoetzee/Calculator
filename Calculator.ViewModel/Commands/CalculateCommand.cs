using Calculator.BusinessLogic.Exceptions;
using Calculator.BusinessLogic.Services;
using Calculator.Model.Enums;
using Calculator.ViewModel.Constants;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class CalculateCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;
        private readonly ICalculationService _calculationService;

        public CalculateCommand(CalculatorViewModel viewModel, ICalculationService calculationService)
        {
            _viewModel = viewModel;
            _calculationService = calculationService;
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedOperator != Operator.None && !_viewModel.MainDisplay.Contains(CalculatorConstants.InvalidInputMessage);
        }

        public void Execute(object? parameter)
        {
            try
            {
                var currentValue = decimal.Parse(_viewModel.MainDisplay);

                if (_viewModel.CalculatorModel.SecondValue.HasValue)
                    _viewModel.CalculatorModel.FirstValue = currentValue;

                _viewModel.CalculatorModel.SecondValue ??= currentValue;
                _viewModel.OnPropertyChanged(nameof(_viewModel.CalculationHistory));

                _viewModel.MainDisplay = _calculationService.Calculate(_viewModel.CalculatorModel.FirstValue.GetValueOrDefault(), _viewModel.CalculatorModel.SecondValue.GetValueOrDefault(), _viewModel.SelectedOperator).ToString();
            }
            catch (InvalidInputException)
            {
                _viewModel.MainDisplay = CalculatorConstants.InvalidInputMessage;
            }
        }
    }
}
