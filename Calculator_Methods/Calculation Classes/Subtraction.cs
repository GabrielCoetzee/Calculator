using System;
using System.Collections.Generic;

namespace Calculator_Methods.Calculation_Classes
{
    public class Subtraction : ICalculable
    {
        public double Calculate(List<double> values)
        {
            return values[values.Count - 2] - values[values.Count - 1];
        }
    }
}
