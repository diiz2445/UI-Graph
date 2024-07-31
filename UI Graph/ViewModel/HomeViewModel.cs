using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI_Graph.ViewModel
{
    class HomeViewModel
    {
        Core.GraphLogic GL = new Core.GraphLogic();
        public static uint number = 0;//Кол-во вершин
        public static double[,] Matr;//Весовая матрица
        DataTable dataTable;
        private void CreateMatrix_Click(object sender, RoutedEventArgs e,string amount="0")
        {
            try
            {
                int rowCount = int.Parse(amount);
                int colCount = rowCount; // Количество столбцов равно количеству строк

                // Создаем матрицу с заданным количеством строк и столбцов
                int[,] matrix = new int[rowCount, colCount];

                DataTable dT = new DataTable();

                // Добавляем столбцы в DataTable
                for (int i = 0; i < colCount; i++)
                {
                    dT.Columns.Add("V" + (i + 1), typeof(int));
                }

                // Добавляем строки в DataTable
                for (int i = 0; i < rowCount; i++)
                {
                    dT.Rows.Add(dT.NewRow());
                }


                dataTable= dT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                
            }
        }
    }
}
