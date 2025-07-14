using Calculator.BusinessLogic.Services;
using Calculator.Model.Helpers;
using Calculator.ViewModel.Constants;
using System.Globalization;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class SelectOperatorCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;
        private readonly ICalculationService _calculationService;

        public SelectOperatorCommand(CalculatorViewModel viewModel, ICalculationService calculationService)
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
            return true;
        }

        public void Execute(object? parameter)
        {
            var currentValue = decimal.Parse(_viewModel.MainDisplay, CultureInfo.InvariantCulture);

            _viewModel.CalculatorModel.FirstValue = currentValue;
            _viewModel.CalculatorModel.SecondValue = null;

            _viewModel.MainDisplay = CalculatorConstants.MainDisplayDefault;
            _viewModel.SelectedOperator = OperatorHelpers.GetOperator(parameter?.ToString());

            if (_viewModel.SelectedOperator == Model.Enums.Operator.SquareRoot)
                _viewModel.MainDisplay = _calculationService.Calculate(_viewModel.CalculatorModel.FirstValue, _viewModel.CalculatorModel.SecondValue, _viewModel.SelectedOperator).ToString();
        }
    }
}
