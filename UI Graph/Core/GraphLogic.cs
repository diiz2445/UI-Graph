using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Проект1
{
    class GraphLogic
    {
        
        public static Double[,] GetMatrixFromDataGrid(DataView dataView)
        {
            // Проверяем, что источник данных DataGrid - это DataView
           
            if (dataView != null)
            {
                // Получаем количество строк и столбцов из DataGrid
                int rowCount = dataView.Count;
                int colCount = dataView.Table.Columns.Count;

                // Создаем двумерный массив для хранения данных из DataGrid
                double[,] matrix = new double[rowCount, colCount];

                // Заполняем массив данными из DataGrid
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        try {
                            matrix[i, j] = Convert.ToInt32(dataView[i][j]);
                        }
                        catch { matrix[i, j] = 0; }
                        
                    }
                }

                return matrix;
            }
            else
            {
                // Если источник данных не DataView, вернем null или выбросим исключение
                return null;
            }
        }

        public static int[,] MSGS(double[,] Matrix)
        {
            int[,] MSG = new int[Matrix.GetLength(0), Matrix.GetLength(1)];

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    MSG[i, j] = (Matrix[i, j] != 0) ? 1 : 0;
                }
            }

            return MSG;
        }

        public static int[,] MATINc(double[,] Matrix)
        {
            int[,] MINC = new int[Matrix.GetLength(0), Matrix.GetLength(1) * (Matrix.GetLength(1) - 1) / 2];

            int edgeIndex = 0;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        MINC[i, edgeIndex] = 1;
                        MINC[j, edgeIndex] = 1;
                        edgeIndex++;
                    }
                }
            }

            int[,] LastMI = new int[Matrix.GetLength(0), edgeIndex];
            for (int i = 0; i < edgeIndex; i++)
                for (int j = 0; j < edgeIndex; j++)
                {
                    try
                    {
                        if (MINC[i, j] != 0) LastMI[i, j] = 1;
                    }
                    catch { }
                    
                }
            return LastMI;
        }
        public static string[] ListSM(double[,] Matrix)//список смежности
        {
            StringBuilder sb = new StringBuilder();
           
            string[] strings = new string[Matrix.GetLength(0)];

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                sb.Clear().Append($"смежные {i+1} вершине: ");
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != 0 && i != j) { sb.Append($"{j+1} "); }
                }
                strings[i] = sb.ToString();
            }

            return strings;
        }
        public static string GL(int st, bool[] visit, double[,] Matrix, string obx)//obx - строка для передачи в TextBox
        {
            int i;

            double[,] MSG = (Matrix);
            obx = Convert.ToString(st + 1) + " "; //вывод одной из вершин,st
            visit[st] = true; //если посещали вершину st
            for (i = 0; i < visit.Length; i++)
                //если есть связь между вершинами графа(MSG[st,i]!=0) и 
                //если не посещали вершину(!visit[i]), то обращение к рекурсивной процедуре
                if ((MSG[st, i] != 0) && (!visit[i]))
                    obx += GL(i, visit, MSG, obx);
            return obx;
        }

        public static string SHIR(int st, double[,] Matrix)
        {
            Queue<int> OCH = new Queue<int>();
            bool[] Visit = new bool[Matrix.GetLength(0)];
            string obx = "";
            Visit[st] = true; //вершина посещена
            OCH.Enqueue(st);//помещаем вершину в пустую очередь(Enqueue()-метод класса Queue)
            while (OCH.Count != 0) // пока число эл-ов в очереди(метод Сount) не равно нулю - пока очередь не пуста
            {
                st = OCH.Peek();// берет вершину из начала очереди, НО НЕ УДАЛЯЕТ
                OCH.Dequeue(); //удаляет вершину из начала очереди
                obx += Convert.ToString(st + 1) + " ";
                //перебираем все вершины, связныес st 
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    //если есть связь между вершинами графа и вершина не пройдена

                    if (Matrix[st, i] != 0)
                    {
                        if (!Visit[i]) //если вершина не посещена
                        {
                            Visit[i] = true; //отмечаем вершину i как пройденную
                            OCH.Enqueue(i); //и добавляем вершину в очередь

                        }
                    }

                }
            }
            return obx;
        }

        private static uint MinDistance(double[] distance, bool[] visited)
        {
            double min = int.MaxValue;// Изначально минимальное расстояние - бесконечность
            uint minIndex = 0;// Индекс вершины с минимальным весом

            for (uint v = 0; v < distance.Length; v++)
            {
                if (!visited[v] && distance[v] <= min)// Если вершина еще не была посещена и ее вес меньше минимального
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
        /*
         Функция алгоритма Дейкстры - нахождение минимального пути от вершины A до вершины B
         graph - Весовая матрица
         sourse - вершина А
         destination - вершина B
         Функция возвращает строку с минимальным путем от вершины А до вершины B
         */
        public static string Dijkstra(double[,] Matrix, uint source, uint destination)
        {
            double[] distance = new double[Matrix.GetLength(0)]; // Массив расстояний до вершин
            bool[] visited = new bool[Matrix.GetLength(0)]; // Массив, содержащий информацию о посещении вершин
            Dictionary<uint, uint> prev = new Dictionary<uint, uint>(); // Словарь, содержащий предыдущую вершину

            // Инициализация массива расстояний
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                distance[i] = int.MaxValue; // изначально расстояние до всех вершин - бесконечность
            }
            distance[source-1] = 0; // расстояние до начальной вершины равно нулю

            // Поиск кратчайшего пути
            for (int i = 0; i < Matrix.GetLength(0) - 1; i++)
            {
                uint u = MinDistance(distance, visited); // Находим вершину с минимальным расстоянием
                visited[u] = true;

                // Обновляем значения расстояний к соседним вершинам
                for (uint v = 0; v < Matrix.GetLength(0); v++)
                {
                    if (!visited[v] && Matrix[u, v] != 0 && distance[u] != int.MaxValue && distance[u] + Matrix[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + Matrix[u, v]; // обновляем расстояние
                        prev[v] = u; // устанавливаем текущую вершину как предыдущую для следующей
                    }
                }
            }
            // Выводим кратчайший путь
            uint current = destination-1;// вспомогательная переменная для записи последовательности вершин
            List<uint> path = new List<uint>();//Список минимального пути
            while (prev.ContainsKey(current))
            {
                path.Add(current + 1);
                current = prev[current];
            }
            path.Add(current + 1);
            path.Reverse(); // Переворачиваем список, чтобы вывести путь от начала до конца
            string S = $"{String.Join(" -> ", path)}";// Строка, куда записывается минимальный путь
            return S;
        }

    }
}
