using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Home3
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            crossZero = new CrossZero(3);

            CapacityCB.SelectionChanged += CapacityCB_SelectionChanged;

            SetField(3);
        }

        internal CrossZero crossZero;

        /// <summary>
        /// Создает пустое поле заданной размерности.
        /// </summary>
        /// <param name="capacity"></param>
        private void SetField(int capacity)
        {
            FieldGrid.ColumnDefinitions.Clear();
            FieldGrid.RowDefinitions.Clear();
            FieldGrid.Children.Clear();

            StatusTextBlock.Text = "Ход игрока " + PlayerOne.Text + " (O)";

            for (int i = 0; i < capacity; i++)
            {
                FieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                FieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    Button button = new Button
                    {
                        Content = "",
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Margin = new Thickness(2)
                    };
                    button.Click += Button_Click;
                    
                    FieldGrid.Children.Add(button);

                    Grid.SetRow(button, j);
                    Grid.SetColumn(button, i);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (crossZero.StepsLeft > 0)
            {
                Button button = sender as Button;
                int y = Grid.GetColumn(button);
                int x = Grid.GetRow(button);
                string playerSymbol = crossZero.CurrentPlayer == 0 ? "O" : "X";
                string player = crossZero.CurrentPlayer == 0 ? PlayerOne.Text : PlayerTwo.Text;

                if (crossZero.Step(x, y, crossZero.CurrentPlayer))
                {
                    button.Content = playerSymbol;
                    int winner = crossZero.Winner();

                    if (winner != -1)
                    {
                        StatusTextBlock.Text = "Победил " + player + " " + playerSymbol;

                        NotEnabledFild();
                    }
                    else
                    {
                        playerSymbol = crossZero.CurrentPlayer == 0 ? "O" : "X";
                        player = crossZero.CurrentPlayer == 0 ? PlayerOne.Text : PlayerTwo.Text;

                        StatusTextBlock.Text = "Ход игрока " + player + " " + playerSymbol;

                        if (crossZero.StepsLeft == 0)
                        {
                            StatusTextBlock.Text = "Ничья";
                            WarningTextBlock.Text = "";
                            NotEnabledFild();
                        }
                    }
                    
                    WarningTextBlock.Text = "";
                }
                else
                {
                    WarningTextBlock.Text = "Ячейка уже занята.";
                }
            }
            else
            {
                StatusTextBlock.Text = "Ничья";
                WarningTextBlock.Text = "";
                NotEnabledFild();
            }
        }

        private void NotEnabledFild()
        {
            foreach (var tb in FieldGrid.Children)
                if (tb is Button)
                    (tb as Button).IsEnabled = false;
        }

        private void CapacityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            ComboBoxItem cbi = cb.SelectedItem as ComboBoxItem;
            string s = cbi.Content as string;

            crossZero.Capacity = int.Parse(s);
            SetField(crossZero.Capacity);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SetField(crossZero.Capacity);
            crossZero.ClearField();
        }
    }
}
