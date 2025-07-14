using Calculator.Model.Enums;

namespace Calculator.Model.Helpers
{
    public static class OperatorHelpers
    {
        public static Operator GetOperator(object operatorPressed)
        {
            return operatorPressed.ToString() switch
            {
                "+" => Operator.Addition,
                "-" => Operator.Subtraction,
                "x" => Operator.Multiplication,
                "÷" => Operator.Division,
                "√" => Operator.SquareRoot,
                _ => Operator.None,
            };
        }
    }
}
