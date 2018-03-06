using System;
using System.Collections.Generic;

namespace Calculator_Methods.Calculation_Classes
{
    public class SquareRoot : ICalculable
    {
        public double Calculate(List<double> values)
        {
            return Math.Sqrt(values[values.Count - 1]);
        }
    }
}
