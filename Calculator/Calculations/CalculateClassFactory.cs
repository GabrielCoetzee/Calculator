using Calculator.MVVM.Models;
using Calculator_Methods;
using Calculator_Methods.Calculation_Classes;

namespace Calculator.Calculations
{
    public static class CalculateClassFactory
    {
        public static ICalculable GetOperationClass(int selectedOperator)
        {
            ICalculable calculateClass = default(ICalculable);

            switch (selectedOperator)
            {
                case (int)ModelCalculator.Operators.Addition:
                    calculateClass = new Addition();
                    break;
                case (int)ModelCalculator.Operators.Division:
                    calculateClass = new Division();
                    break;
                case (int)ModelCalculator.Operators.Multiplication:
                    calculateClass = new Multiplication();
                    break;
                case (int)ModelCalculator.Operators.Subtraction:
                    calculateClass = new Subtraction();
                    break;
                case (int)ModelCalculator.Operators.SquareRoot:
                    calculateClass = new SquareRoot();
                    break;
            }

            return calculateClass;
        }
    }
}
