using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public class SquareRoot : IOperation
    {
        public Operator Operator => Operator.SquareRoot;

        public decimal Calculate(decimal? firstValue, decimal? secondValue)
        {
            return (decimal) Math.Sqrt(double.Parse(firstValue.GetValueOrDefault().ToString()));
        }
    }
}
