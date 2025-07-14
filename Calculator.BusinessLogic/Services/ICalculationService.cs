using Calculator.Model.Enums;

namespace Calculator.BusinessLogic.Services
{
    public interface ICalculationService
    {
        decimal Calculate(decimal? firstValue, decimal? secondValue, Operator selectedOperator);
    }
}
