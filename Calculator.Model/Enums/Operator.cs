using System.ComponentModel.DataAnnotations;

namespace Calculator.Model.Enums
{
    public enum Operator 
    {
        [Display(Name = "")]
        None,

        [Display(Name = "+")]
        Addition,

        [Display(Name = "-")]
        Subtraction,

        [Display(Name = "x")]
        Multiplication,

        [Display(Name = "÷")]
        Division,

        [Display(Name = "√")]
        SquareRoot
    };
}
