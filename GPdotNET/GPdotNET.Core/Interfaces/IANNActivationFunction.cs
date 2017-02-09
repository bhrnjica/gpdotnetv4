using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Core.Interfaces
{

    public interface IANNActivation
    {

        double Calculate(double input);

        double Derivative(double input);

        string StringFormula(string value);
    }
}
