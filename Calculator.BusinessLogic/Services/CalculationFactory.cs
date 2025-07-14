using Calculator.BusinessLogic.Operations;
using Calculator.Model.Enums;

namespace Calculator.ViewModel.Calculations
{
    public class CalculationFactory
    {
        private readonly IEnumerable<IOperation> _operations;

        public CalculationFactory(IEnumerable<IOperation> operations)
        {
            _operations = operations;
        }

        public IOperation GetOperation(Operator selectedOperator)
        {
            return _operations.Single(x => x.Operator == selectedOperator);
        }
    }
}
