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
            const double pi = 3.14159;
            Random rd = new Random();
            MWNumericArray nPole = 1;
            MWNumericArray sPole = 2;
            CRD crd = new CRD();
            int scale = 10;
            double min = 99999;
            int i = 0;
            int j = 0;
            int k = 0;
            double[] rInBox = { 0, -1, -0.5, -0.2 };
            double[,] aMatOut = new double[scale, scale];
            for (k = 0; k <2000; k++)
            {
                Console.WriteLine(k);
                Console.WriteLine(min);
                int horizon = scale * scale;
                double[] aArr = new double[horizon];
                double[] nod = new double[horizon];
                double[,] aMatInMatrix = new double[scale, scale];
                for (i = 0; i < scale; i++)
                {
                    for (j = 0; j < scale; j++)
                    {
                        if (i != j)
                        {
                            int rad = rd.Next(0, 4);
                            System.Threading.Thread.Sleep(1);
                            
                            aMatInMatrix[i, j] = rInBox[rad];
                            aMatInMatrix[j, i] = aMatInMatrix[i, j];
                            nod[i] = nod[i] - 2*aMatInMatrix[i, j];
                        }
                    }
                }
                for (i = 0; i < scale; i++)
                {
                    aMatInMatrix[i, i] = nod[i];
                }

                MatrixToArr(scale, aArr, aMatInMatrix);
                double resist = Double.Parse(crd.CalculateResistanceDistance(scale, aArr, nPole, sPole));
                double now = Math.Abs(resist - pi);
                if (now < min)
                {
                    min = now;
                    for (i = 0; i < scale; i++)
                    {
                        for (j = 0; j < scale; j++)
                        {
                            aMatOut[i, j] = aMatInMatrix[i,j];
                        }
                    }
                }
               
            }
            for (i = 0; i < scale; i++)
            {
                for (j = 0; j < scale; j++)
                {
                    if ((j + 1) == scale)
                    {
                        Console.Write(aMatOut[i, j] + "\n");
                    }
                    else
                    {
                        Console.Write(aMatOut[i, j] + " ");
                    }
                }
            }
            Console.Write("最小偏差"+min);



                    Console.ReadKey();
        }

        private static void MatrixToArr(int scale, double[] aMat, double[,] aMatInMatrix)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            for (a = 0; a < scale; a++)
            {
                for (b = 0; b < scale; b++)
                {
                    aMat[c] = aMatInMatrix[a, b];
                    c++;
                }
            }
        }

    }
}
