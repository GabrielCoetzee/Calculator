using Calculator.MVVM.Models;

namespace Calculator.Helpers.Operators
{
    public static class Operators
    {
        public static int GetOperator(object operatorPressed)
        {
            int selectedOperator = default(int);

            switch (operatorPressed.ToString())
            {
                case "+":
                    selectedOperator = (int)ModelCalculator.Operators.Addition;
                    break;
                case "-":
                    selectedOperator = (int)ModelCalculator.Operators.Subtraction;
                    break;
                case "x":
                    selectedOperator = (int)ModelCalculator.Operators.Multiplication;
                    break;
                case "÷":
                    selectedOperator = (int)ModelCalculator.Operators.Division;
                    break;
                case "√":
                    selectedOperator = (int)ModelCalculator.Operators.SquareRoot;
                    break;
                default:
                    selectedOperator = (int)ModelCalculator.Operators.None;
                    break;
            }

            return selectedOperator;
        }
    }
}
