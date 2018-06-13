using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Eyler
{
    class Program
    {       
        private const int Nmax = 5, N_St = Nmax * (Nmax - 1) / 2;  //здесь указывается количество абонентов в сети

        private static int[,] A = new int[Nmax, Nmax];
        private static int[,] A_Eiler = new int[Nmax, Nmax];
        private static int[] Stack = new int[0];
        private static int[][] Ribs = new int[Nmax - 1][];

        // Выполняет чтение матрицы весов из файла или консоли
        private static void Init(int[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                string enterString = Console.ReadLine();
                string[] massiveString = enterString.Split(new Char[] { ' ' });
                for (int j = 0; j < massiveString.Length; j++)
                {
                    A[i, j] = int.Parse(massiveString[j]);
                }
            }

            /*StreamReader sReader = new StreamReader("input.txt");
            for (int i = 0; i < A.GetLength(0); i++)
            {
                string[] str = sReader.ReadLine().Split(' ');
                for (int j = 0; j < A.GetLength(1); j++)
                    A[i, j] = int.Parse(str[j]);
            }
            sReader.Close();*/
                            
        }

        // Строит каркас минимального веса
        private static void FindTree(int[,] A_Eiler)
        {
            Set Sp = new Set();
            int min = 100;
            int l = 0, t = 0;
            for (int i = 0; i < Nmax - 1; i++)
                for (int j = 1; j < Nmax; j++)
                    if ((A[i, j] < min) && (A[i, j] != 0))
                    {
                        min = A[i, j];
                        l = i;
                        t = j;
                    }
            A_Eiler[l, t] = A[l, t];
            A_Eiler[t, l] = A[t, l];
            Sp.Add(l + 1);
            Sp.Add(t + 1);

            int ribIndex = 0;
            Ribs[ribIndex] = new int[2];
            Ribs[ribIndex][0] = l + 1;
            Ribs[ribIndex][1] = t + 1;
            ribIndex++;

            while (!Sp.Contains(1, Nmax))
            {
                min = 100;
                l = 0; t = 0;
                for (int i = 0; i < Nmax; i++)
                    if (Sp.Contains(i + 1))
                        for (int j = 0; j < Nmax; j++)
                            if (!Sp.Contains(j + 1) && (A[i, j] < min) && (A[i, j] != 0))
                            {
                                min = A[i, j];
                                l = i;
                                t = j;
                            }
                A_Eiler[l, t] = A[l, t];
                A_Eiler[t, l] = A[t, l];
                Sp.Add(l + 1);
                Sp.Add(t + 1);

                Ribs[ribIndex] = new int[2];
                Ribs[ribIndex][0] = l + 1;
                Ribs[ribIndex][1] = t + 1;
                ribIndex++;
            }
        }

        
        

        // Поиск пути
        private static void FindWay(int v)
        {
            for (int i = 0; i < Nmax; i++)
                if (A_Eiler[v, i] != 0)
                {
                    A_Eiler[v, i] = 0;
                    FindWay(i);
                }
            int[] temp = (int[])Stack.Clone();
            Stack = new int[Stack.Length + 1];
            for (int i = 0; i < temp.Length; i++)
                Stack[i] = temp[i];
            Stack[Stack.Length - 1] = v + 1;
        }

        // Вывод результата
        private static void OutPut()
        {
            Set Way = new Set();
            int i, pred_v, Cost = 0;
            Console.Write("Порядок соединения абонентов: ");
            Console.Write(" ");
            Console.Write("{0} ", Stack[0]);
            Way.Add(Stack[0]);
            pred_v = Stack[0];
            for (i = 1; i < Stack.Length; i++)
                if (!Way.Contains(Stack[i]))
                {
                    Console.Write("{0} ", Stack[i]);
                    Way.Add(Stack[i]);
                    Cost += A[pred_v - 1, Stack[i] - 1];
                    pred_v = Stack[i];
                }
            //Console.WriteLine("{0} ", Stack[0]);
            //Cost += A[pred_v - 1, Stack[0] - 1];
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.Write("Стоимость прокладки сети: ");
            Console.Write("{0,5}", Cost);
        }

        

        static void Main(string[] args)
        {
                       
            Init(A);
            FindTree(A_Eiler);
            
            FindWay(0);
            OutPut();

            Console.ReadKey();
        }


        // Класс "Множество"
        class Set
        {
            int[] Values;
            public Set()
            {
                Values = new int[0];
            }

            // Добавление элемента в множество
            public void Add(int value)
            {
                for (int j = 0; j < Values.Length; j++)
                    if (value == Values[j])
                        return;
                int[] temp = (int[])Values.Clone();
                Values = new int[Values.Length + 1];
                int i = 0;
                while ((i < temp.Length) && (value > temp[i]))
                {
                    Values[i] = temp[i];
                    i++;
                }
                Values[i++] = value;
                for (int j = i; (j < Values.Length) && (j - 1 >= 0) && (j - 1 < temp.Length); j++)
                    Values[j] = temp[j - 1];
            }

            // Удаление элемента из множества
            public void Delete(int value)
            {
                int i = 0;
                for (i = 0; i < Values.Length; i++)
                    if (value == Values[i])
                        break;
                if (i == Values.Length)
                    return;
                int[] temp = (int[])Values.Clone();
                Values = new int[Values.Length - 1];
                for (int j = 0; j < i; j++)
                    Values[j] = temp[j];
                for (int j = i; (j < Values.Length) && (j + 1 < temp.Length); j++)
                    Values[j] = temp[j + 1];
            }

            // Определяет, содержатся ли все элементы входного массива в множестве
            public bool Contains(int[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (!this.Contains(array[i]))
                        return (false);
                }
                return (true);
            }

            // Определяет, содержатся ли все элементы из указанного отрезка
            public bool Contains(int begin, int end)
            {
                System.Collections.ArrayList tArray = new System.Collections.ArrayList();
                for (int i = begin; i <= end; i++)
                    tArray.Add(i);
                int[] array = new int[tArray.Count];
                for (int i = 0; i < array.Length; i++)
                    array[i] = int.Parse(tArray[i].ToString());
                return (this.Contains(array));
            }

            // Определяет, содержится ли элемент в множестве
            public bool Contains(int value)
            {
                for (int i = 0; i < Values.Length; i++)
                    if (value == Values[i])
                        return (true);
                return (false);
            }
        }

    }
}