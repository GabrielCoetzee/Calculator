using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Operations
{
    public interface IOperation
    {
        Operator Operator { get; }
        decimal Calculate(decimal? firstValue, decimal? secondValue);
    }
}
