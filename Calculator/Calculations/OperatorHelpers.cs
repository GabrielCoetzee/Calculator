using Calculator.MVVM.Models;

namespace Calculator.Helpers.Operators
{
    public static class OperatorHelpers
    {
        public static ModelCalculator.Operators GetOperator(object operatorPressed)
        {
            var selectedOperator = default(ModelCalculator.Operators);

            switch (operatorPressed.ToString())
            {
                case "+":
                    selectedOperator = ModelCalculator.Operators.Addition;
                    break;
                case "-":
                    selectedOperator = ModelCalculator.Operators.Subtraction;
                    break;
                case "x":
                    selectedOperator = ModelCalculator.Operators.Multiplication;
                    break;
                case "÷":
                    selectedOperator = ModelCalculator.Operators.Division;
                    break;
                case "√":
                    selectedOperator = ModelCalculator.Operators.SquareRoot;
                    break;
                default:
                    selectedOperator = ModelCalculator.Operators.None;
                    break;
            }

            return selectedOperator;
        }
    }
}
