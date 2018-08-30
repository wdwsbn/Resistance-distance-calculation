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
            double resistout=0;
            int i1 = 0;
            int j1 = 0;
            int nPolego = 0;
            int sPolego = 0;
            CRD crd = new CRD();
            int scale = 7;
            double min = 99999;
            int i = 0;
            int j = 0;
            int k = 0;
            double[] rInBox = { 0, -1, -0.5, -0.2 ,0,0};
            double[,] aMatOut = new double[scale, scale];
            for (k = 0; k <11100; k++)
            {
                Console.WriteLine(k);
                Console.WriteLine(min);
                int horizon = scale * scale;
                double[] aArr = new double[horizon];
                double[] nod = new double[horizon];
                double[,] aMatInMatrix = new double[scale, scale];
                for (i = 0; i < scale; i++)
                {
                    for (j = i; j < scale; j++)
                    {
                        if (i != j)
                        {
                            int rad = rd.Next(0, 6);
                            System.Threading.Thread.Sleep(1);
                            
                            aMatInMatrix[i, j] = rInBox[rad];
                            aMatInMatrix[j, i] = aMatInMatrix[i, j];
                            nod[i] = nod[i] - aMatInMatrix[i, j];
                            nod[j] = nod[j] - aMatInMatrix[i, j];
                        }
                    }
                }
                for (i = 0; i < scale; i++)
                {
                    aMatInMatrix[i, i] = nod[i];
                }

                MatrixToArr(scale, aArr, aMatInMatrix);
                try
                {
                    for (i = 1; i < scale + 1; i++)
                    {
                        for (j = i; j < scale + 1; j++)
                        {
                            if (i != j)
                            {
                                double resist = 0;
                                nPole = i;
                                sPole = j;

                                resist = Double.Parse(crd.CalculateResistanceDistance(scale, aArr, nPole, sPole));


                                double now = Math.Abs(resist - pi);
                                if (now < min)
                                {
                                    resistout = resist ;
                                    
                                        nPolego = i;
                                    sPolego = j;
                                    min = now;
                                    for (i1 = 0; i1 < scale; i1++)
                                    {
                                        for (j1 = 0; j1 < scale; j1++)
                                        {
                                            aMatOut[i1, j1] = aMatInMatrix[i1, j1];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch {
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
            Console.Write("阻值"+ resistout);

            Console.Write("最小偏差" + min);
            Console.Write("进入节点" + nPolego);
            Console.Write("跳出节点" + sPolego);

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
