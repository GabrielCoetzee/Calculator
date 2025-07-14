using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public class Subtraction : IOperation
    {
        public Operator Operator => Operator.Subtraction;

        public decimal Calculate(decimal? firstValue, decimal? secondValue)
        {
            return firstValue.GetValueOrDefault() - secondValue.GetValueOrDefault();
        }
    }
}
