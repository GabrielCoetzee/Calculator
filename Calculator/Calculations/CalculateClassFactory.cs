using Calculator.MVVM.Models;
using Calculator_Methods;
using Calculator_Methods.Calculation_Classes;

namespace Calculator.Calculations
{
    public static class CalculateClassFactory
    {
        public static ICalculable GetOperationClass(ModelCalculator.Operators selectedOperator)
        {
            ICalculable calculateClass = default(ICalculable);

            switch (selectedOperator)
            {
                case ModelCalculator.Operators.Addition:
                    calculateClass = new Addition();
                    break;
                case ModelCalculator.Operators.Division:
                    calculateClass = new Division();
                    break;
                case ModelCalculator.Operators.Multiplication:
                    calculateClass = new Multiplication();
                    break;
                case ModelCalculator.Operators.Subtraction:
                    calculateClass = new Subtraction();
                    break;
                case ModelCalculator.Operators.SquareRoot:
                    calculateClass = new SquareRoot();
                    break;
            }

            return calculateClass;
        }
    }
}
