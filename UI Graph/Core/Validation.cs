using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Graph.Core
{
    class Validation
    {
        public static bool CheckSim(double[,] Matr)//проверка симметрии относительно главной диагонали 
        {
            
            for(int i = 0; i < Matr.GetLength(0);i++)
                for(int j=0; j<Matr.GetLength(1);j++)
                    if (Matr[i, j] != Matr[j,i]) return false;
            return true;


        }
        public static bool CheckZeros(double[,] Matr)//проверка на нули на главной диагонали
        {

            for (int i = 0; i < Matr.GetLength(0); i++)
                    if (Matr[i, i] != 0) return false;
            return true;
        }
        public static bool CheckNegative(double[,] Matr)//проверка на отрицательные значения в матрице
        {
            for (int i = 0; i < Matr.GetLength(0); i++)
                for (int j = 0; j < Matr.GetLength(1); j++)
                    if (Matr[i, j] < 0) return false;
            return true;
        }
        public static bool CheckAllInfo(double[,] Matr)//проверка наличия нарушений по всем параметрам
        {
            return true;
        }
    }
}
