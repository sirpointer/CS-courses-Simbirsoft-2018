using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home2
{
    class Program
    {
        static void Main(string[] args)
        {
            Points.Add(0);
            Points.Add(0);
            Points.Add(0);
            Points.Add(0);

            for (int i = 0; i < 4; i++)
            {
                string name = Console.ReadLine();
                players.Add(name, 0);
                Names[i] = name;
            }

            Console.WriteLine();

            int winnerIndex = -1;

            while (true)
            {
                Console.WriteLine("-----------------------");

                int index = WinnerIndex();
                Console.WriteLine(Environment.NewLine + index + Environment.NewLine);
                Points[index]++;

                foreach (var a in Points)
                    Console.Write(a + " ");

                Console.WriteLine();

                for (int i = 0; i < Points.Count; i++)
                    if (Points[i] == 5)
                        winnerIndex = i;

                foreach (var point in Points)
                    Console.Write(point + " ");

                Console.WriteLine();

                if (winnerIndex >= 0)
                    break;
            }

            Console.WriteLine(Environment.NewLine + "Побеждает " + Names[winnerIndex]);

            Console.ReadKey();
        }

        /// <summary>
        /// Очки игр.
        /// </summary>
        static List<int> Points = new List<int>();

        /// <summary>
        /// Имена игроков.
        /// </summary>
        static string[] Names = new string[4];

        /// <summary>
        /// Игроки.
        /// </summary>
        static Dictionary<string, int> players = new Dictionary<string, int>();

        /// <summary>
        /// Колода.
        /// </summary>
        static Stack<int> Deck = new Stack<int>(52);

        /// <summary>
        /// Количество карт у игроков.
        /// </summary>
        static readonly int cardsCount = 5;

        /// <summary>
        /// Собирает колоду.
        /// </summary>
        static void DeckFill()
        {
            Random random = new Random();

            for (int i = 0; i < 52; i++)
            {
                while (true)
                {
                    int d = random.Next(2, 15);
                    int dCount = 0;

                    foreach (var a in Deck)
                    {
                        if (a == d)
                            dCount++;
                    }

                    if (dCount < 4)
                    {
                        Deck.Push(d);
                        break;
                    }
                }
            }
        }

        // Карты игроков.
        static List<int> firstPlayerCards = new List<int>();
        static List<int> secondPlayerCards = new List<int>();
        static List<int> thirdPlayerCards = new List<int>();
        static List<int> fourthPlayerCards = new List<int>();

        /// <summary>
        /// Проводит одну игру и возвращает индекс победившего игрока.
        /// </summary>
        /// <returns></returns>
        static int WinnerIndex()
        {
            Deck.Clear();

            foreach (var a in Names)
            {
                players[a] = 0;
            }

            DeckFill();
            TakeCards();
            DropCards();

            int winnerIndex = 0;

            for (int i = 1; i < Names.Length; i++)
            {
                if (players[Names[i]] > players[Names[winnerIndex]])
                    winnerIndex = i;
            }

            foreach (var n in Names)
            {
                Console.WriteLine(n + " " + players[n]);
            }

            return winnerIndex;
        }

        /// <summary>
        /// Сброс карт для всех игроков.
        /// </summary>
        static void DropCards()
        {
            DropCards(firstPlayerCards, 0);
            DropCards(secondPlayerCards, 1);
            DropCards(thirdPlayerCards, 2);
            DropCards(fourthPlayerCards, 3);
        }

        /// <summary>
        /// Сброс карт для одного игрока.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="nameIndex"></param>
        static void DropCards(List<int> player, int nameIndex)
        {
            Random cardCount = new Random();
            Random drop = new Random();
            int minIndex = 0;
            int card = 0;

            for (int i = 0; i < cardCount.Next(0, 3); i++)
            {
                for (int j = 1; j < player.Count; j++)
                {
                    if (player[j] < player[minIndex])
                        minIndex = j;
                }

                card = Deck.Pop();
                players[Names[nameIndex]] -= player[minIndex];
                players[Names[nameIndex]] += card;
                player[minIndex] = card;

                minIndex = 0;
            }
        }

        /// <summary>
        /// Раздает карты всем игрокам.
        /// </summary>
        static void TakeCards()
        {
            TakeCards(firstPlayerCards, 0);
            TakeCards(secondPlayerCards, 1);
            TakeCards(thirdPlayerCards, 2);
            TakeCards(fourthPlayerCards, 3);
        }

        /// <summary>
        /// Раздает карты одному игроку.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="NameIndex"></param>
        static void TakeCards(List<int> player, int NameIndex)
        {

            for (int i = 0; i < cardsCount; i++)
            {
                int card = Deck.Pop();
                player.Add(card);
                players[Names[NameIndex]] += card;
            }
        }
    }
}
