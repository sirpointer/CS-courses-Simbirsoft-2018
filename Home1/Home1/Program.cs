using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool win = false;
            int player = 0;
            int winner = -1;
            Capacity = GetCapacity();

            Matrix = new int[Capacity, Capacity];
            FillMatrix();

            PrintMatrix();

            for (int i = 0; i < Capacity * Capacity; i++)
            {
                Step(player);

                winner = Winner();

                if (winner != -1)
                {
                    break;
                }
                else
                {
                    player = player == 0 ? player = 1 : player = 0;
                }

                PrintMatrix();
            }

            Console.WriteLine();
            PrintMatrix();

            Console.WriteLine("Победил " + winner);
            Console.ReadKey();

        }

        static int Winner()
        {
            int winner = -1;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                winner = Matrix[i, 0];

                if (winner != -1)
                {
                    for (int j = 1; j < Matrix.GetLength(1); j++)
                    {
                        if (Matrix[i, j] != winner)
                        {
                            break;
                        }
                        else
                        {
                            if (j == Matrix.GetLength(1) - 1)
                                return winner;
                        }
                    }

                    winner = -1;
                }
            }

            winner = -1;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                winner = Matrix[0, i];

                if (winner != -1)
                {
                    for (int j = 1; j < Matrix.GetLength(1); j++)
                    {
                        if (Matrix[j, i] != winner)
                        {
                            break;
                        }
                        else
                        {
                            if (j == Matrix.GetLength(1) - 1)
                                return winner;
                        }
                    }

                    winner = -1;
                }
            }

            winner = Matrix[0, 0];

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                if (winner != -1)
                {
                    if (Matrix[i, i] != winner)
                    {
                        break;
                    }
                    else
                    {
                        if (i == Matrix.GetLength(0) - 1)
                            return winner;
                    }
                }
            }

            winner = Matrix[Matrix.GetLength(0) - 1, 0];

            for (int i = Matrix.GetLength(0) - 2; i >= 0; i--)
            {
                if (winner != -1)
                {
                    if (Matrix[Matrix.GetLength(0) - i - 1, i] != winner)
                    {
                        break;
                    }
                    else
                    {
                        if (i == 0)
                            return winner;
                    }
                }
            }

            return -1;
        }

        static void Step(int value)
        {
            int x = 0;
            int y = 0;
            bool correct = false;

            while (!correct)
            {
                x = UserChoice("x");
                y = UserChoice("y");

                if (Matrix[y, x] == -1)
                {
                    Matrix[y, x] = value;
                    correct = true;
                }
            }
        }

        static int UserChoice(string coordName = "x")
        {
            bool correct = false;
            int coordinate = 0;
            string num = "";

            while (!correct)
            {
                Console.WriteLine("Выберите ячейку матрицы по " + coordName + " от 1 до " + Capacity);
                num = Console.ReadLine();
                if (int.TryParse(num, out coordinate))
                {
                    if (coordinate >= 1 && coordinate <= Capacity)
                        correct = true;
                }
            }

            return coordinate - 1;
        }

        static int GetCapacity()
        {
            string num = "";
            int capacity = 0;
            bool correct = false;

            Console.WriteLine("Введите размер матрицы");
            num = Console.ReadLine();

            if (int.TryParse(num, out capacity))
            {
                if (capacity > 0)
                {
                    correct = true;
                }
            }

            while (!correct)
            {
                Console.WriteLine("Введите корректный размер матрицы");
                num = Console.ReadLine();

                if (int.TryParse(num, out capacity))
                {
                    if (capacity > 0)
                    {
                        correct = true;
                    }
                }
            }

            return capacity;
        }

        static int Capacity { get; set; } = 0;

        static int[,] Matrix { get; set; }

        static void FillMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                    Matrix[i, j] = -1;
            }
        }

        static void PrintMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                    Console.Write(string.Format(" {0,2}", Matrix[i, j]));
                
                Console.WriteLine();
            }
        }
    }
}
