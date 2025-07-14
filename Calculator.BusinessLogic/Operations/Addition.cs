using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public class Addition : IOperation
    {
        public Operator Operator => Operator.Addition;

        public decimal Calculate(decimal? firstValue, decimal? secondValue)
        {
            return firstValue.GetValueOrDefault() + secondValue.GetValueOrDefault();
        }
    }
}
