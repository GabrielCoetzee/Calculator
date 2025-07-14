using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public class Multiplication : IOperation
    {
        public Operator Operator => Operator.Multiplication;

        public decimal Calculate(decimal? firstValue, decimal? secondValue)
        {
            return firstValue.GetValueOrDefault() * secondValue.GetValueOrDefault();
        }
    }
}
