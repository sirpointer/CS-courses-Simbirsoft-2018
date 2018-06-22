using System;

namespace Home4
{
    class CrossZero
    {
        public CrossZero()
        {
            MinCapasity = 3;
            MaxCapasity = 10;
            Capacity = MinCapasity;
            StepsLeft = Capacity * Capacity;
            CurrentPlayer = 0;
        }

        public CrossZero(int capacity) : this()
        {
            Capacity = capacity;
        }

        private int matrix_capacity;

        public int CurrentPlayer { get; private set; }

        /// <summary>
        /// Игровое поле.
        /// </summary>
        public int[,] Matrix { get; set; }

        /// <summary>
        /// Количество свободных ячеек.
        /// </summary>
        public int StepsLeft { get; private set; }

        /// <summary>
        /// Минимальный размер игрового поля.
        /// </summary>
        public readonly int MinCapasity;

        /// <summary>
        /// Максимальный размер игрового поля.
        /// </summary>
        public readonly int MaxCapasity;

        /// <summary>
        /// Возвращает или задает размер игрового поля.
        /// При задании нового размера поле заполняется значениями -1.
        /// Минимальный размер поля 3, Максимальный размер поля 10.
        /// </summary>
        public int Capacity
        {
            get
            {
                return matrix_capacity;
            }
            set
            {
                if (value < 2)
                    throw new Exception("Минимальный размер поля 3");
                if (value > 10)
                    throw new Exception("Максимальный размер поля 10");

                matrix_capacity = value;

                Matrix = new int[value, value];
                ClearField();
            }
        }

        /// <summary>
        /// Заполняет матрицу значениями -1.
        /// </summary>
        public void ClearField()
        {
            for (int i = 0; i < matrix_capacity; i++)
            {
                for (int j = 0; j < matrix_capacity; j++)
                    Matrix[i, j] = -1;
            }

            StepsLeft = Capacity * Capacity;
            CurrentPlayer = 0;
        }

        /// <summary>
        /// Присваивает полю значение value на позиции x, y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <returns>Возвращает true, если успешно. false, если позиция уже занята или шагов больше не осталось.</returns>
        public bool Step(int x, int y, int value)
        {
            if (StepsLeft == 0)
                return false;

            if (x >= 0 && x < Capacity && y >= 0 && y < Capacity)
            {
                if (Matrix[x, y] == -1)
                {
                    Matrix[x, y] = value;
                    StepsLeft--;
                    CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
                    return true;
                }
            }
            else
                throw new IndexOutOfRangeException("x и y должны лежать в диапозоне от 0 до Capacity.");

            return false;
        }

        /// <summary>
        /// Проверяет поля на наличие победителя.
        /// </summary>
        /// <returns>Возвращает номер победителя или -1, если его нет.</returns>
        public int Winner()
        {
            int winner = -1;

            for (int i = 0; i < Capacity; i++)
            {
                winner = Matrix[i, 0];

                if (winner != -1)
                {
                    for (int j = 1; j < Capacity; j++)
                    {
                        if (Matrix[i, j] != winner)
                        {
                            break;
                        }
                        else
                        {
                            if (j == Capacity - 1)
                                return winner;
                        }
                    }
                }
            }

            for (int i = 0; i < Capacity; i++)
            {
                winner = Matrix[0, i];

                if (winner != -1)
                {
                    for (int j = 1; j < Capacity; j++)
                    {
                        if (Matrix[j, i] != winner)
                        {
                            break;
                        }
                        else
                        {
                            if (j == Capacity - 1)
                                return winner;
                        }
                    }
                }
            }

            winner = Matrix[0, 0];

            for (int i = 0; i < Capacity; i++)
            {
                if (winner != -1)
                {
                    if (Matrix[i, i] != winner)
                        break;
                    else
                    {
                        if (i == Capacity - 1)
                            return winner;
                    }
                }
            }

            winner = Matrix[Capacity - 1, 0];

            for (int i = Capacity - 2; i >= 0; i--)
            {
                if (winner != -1)
                {
                    if (Matrix[i, Capacity - i - 1] != winner)
                        break;
                    else
                    {
                        if (i == 0)
                            return winner;
                    }
                }
            }

            return -1;
        }
    }
}

