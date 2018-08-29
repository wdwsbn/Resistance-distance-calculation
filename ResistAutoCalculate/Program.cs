using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResistCalculate; //添加引用
using MathWorks.MATLAB.NET.Arrays;


namespace ResistAutoCalculate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please input scale");
            int scale = Convert.ToInt32(Console.ReadLine());
            int i = 0;
            int n = scale * scale;
            double[] a = new double[n];
           
            for (i = 0; i < n; i++) {
                Console.Write("Please input Admittance matrix");
                a[i] = Convert.ToDouble(Console.ReadLine()) ;

            }
            Console.Write("Please input node1");
            MWNumericArray nPole = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please input node2");
            MWNumericArray sPole = Convert.ToInt32(Console.ReadLine());
            MWArray A = new MWNumericArray(scale,scale,a);
            ResisrCalcu resisrCalcu = new ResisrCalcu();
            string s = resisrCalcu.Resist(A, nPole, sPole).ToString();
            Console.Write("Resistance distance is");
            Console.Write(s);
            Console.ReadKey();
        }
    }
}
