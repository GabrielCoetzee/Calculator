using Calculator.Model;
using Calculator.Model.Enums;
using Calculator.ViewModel.Constants;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
    public class ClearAllCommand : ICommand
    {
        private readonly CalculatorViewModel _viewModel;

        public ClearAllCommand(CalculatorViewModel viewModel)
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
            return true;
        }

        public void Execute(object? parameter)
        {
            _viewModel.MainDisplay = CalculatorConstants.MainDisplayDefault;
            _viewModel.SelectedOperator = Operator.None;

            _viewModel.CalculatorModel = new CalculatorModel() 
            { 
                FirstValue = null,
                SecondValue = null
            };
        }
    }
}
