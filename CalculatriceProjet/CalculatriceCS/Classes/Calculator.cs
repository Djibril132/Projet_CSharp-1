using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatriceCS.Classes
{
    internal class Calculator
    {
        //Racine carré 
        public Double RootOf2(double Num)
        {
            return Math.Pow(Num, (double) 1 / 2);
        }

        //Racine Cube
        public Double RootOf3(double Num)
        {
            return Math.Pow(Num, (double)1 / 3);
        }

        //Puissance de 2 
        public Double PowerOf2(double Num)
        {
            return Math.Pow(Num, 2);
        }

        //Puissance de n
        public Double PowerOf(double Num, double power)
        {
            return Math.Pow(Num, power);
        }

        //Tangente
        public Double Tan(double Num)
        {
            return Math.Tan(Num);
        }

        //Arc Tangente
        public Double Atan(double Num)
        {
            return Math.Atan(Num);
        }

        //Sinus
        public Double Sin(double Num)
        {
            return Math.Sin(Num);
        }

        //Arc Sinus
        public Double Asin(double Num)
        {
            return Math.Asin(Num);
        }

        //Cosinus
        public Double Cos(double Num)
        {
            return Math.Cos(Num);
        }

        //Arc Cosinus
        public Double Acos(double Num)
        {
            return Math.Acos(Num);
        }

        //Ln
        public double Ln(double Num)
        {
            return Math.Log(Num);
        }

        //Logarithm
        public double Log(double Num)
        {
            return Math.Log10(Num);
        }

        //Exponentielle
        public double Exponential(double Num)
        {
            return Math.Pow(Math.E, Num);
        }

        //PI
        public double PI()
        {
            return Math.PI;
        }
    }
}
