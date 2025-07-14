using Calculator.BusinessLogic.Exceptions;
using Calculator.Model.Enums;
using Calculator.ViewModel.Calculations;

namespace Calculator.BusinessLogic.Services
{
    public class CalculationService : ICalculationService
    {
        readonly CalculationFactory _factory;
        public CalculationService(CalculationFactory factory)
        {
            _factory = factory;
        }

        public decimal Calculate(decimal firstValue, decimal secondValue, Operator selectedOperator)
        {
            try
            {
                return _factory
                    .GetOperation(selectedOperator)
                    .Calculate(firstValue, secondValue);
            }
            catch (FormatException)
            {
                throw new InvalidInputException();
            }
            catch (OverflowException)
            {
                throw new InvalidInputException();
            }
            catch (ArgumentException)
            {
                throw new InvalidInputException();
            }
            catch (ArithmeticException)
            {
                throw new InvalidInputException();
            }
        }
    }
}
