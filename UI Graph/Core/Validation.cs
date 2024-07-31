using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Проект1
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
        public static bool CheckAllInfo(double[,] Matr)
        {
            if (CheckNegative(Matr) && CheckSim(Matr) &&CheckZeros(Matr)) return false;//проверка наличия нарушений по всем параметрам
            return true;
        }
    }
}
