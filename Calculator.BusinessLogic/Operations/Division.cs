using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public class Division : IOperation
    {
        public Operator Operator => Operator.Division;

        public decimal Calculate(decimal? firstValue, decimal? secondValue)
        {
            return firstValue.GetValueOrDefault() / secondValue.GetValueOrDefault();
        }
    }
}
