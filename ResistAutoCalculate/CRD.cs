using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResistCalculate; //添加引用
using MathWorks.MATLAB.NET.Arrays;
namespace ResistAutoCalculate
{
    class CRD
    {
        public string CalculateResistanceDistance(int scale, double[] a, MWNumericArray nPole, MWNumericArray sPole)
        {
            MWArray A = new MWNumericArray(scale, scale, a);
            ResisrCalcu resisrCalcu = new ResisrCalcu();
            string s = resisrCalcu.Resist(A, nPole, sPole).ToString();
            return s;
        }
    }
}
