using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeeSlotMachine.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.CoffeeSlotMachineModel coffeeSlotMachine = FindResource("CoffeeSlotMachine") as Models.CoffeeSlotMachineModel;

            if (coffeeSlotMachine != null)
            {
                if (sender is Button btn)
                {
                    if (btn.Content.ToString().Equals("einwurf", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var coin = Convert.ToInt32(cmbCoins.SelectedValue);

                        coffeeSlotMachine.InsertCoin(coin);
                    }
                    else if (btn.Content.ToString().Equals("abbruch", StringComparison.CurrentCultureIgnoreCase))
                    {
                        coffeeSlotMachine.CancelOrder();
                    }
                    else if (btn.Content.ToString().Equals("entleeren", StringComparison.CurrentCultureIgnoreCase))
                    {
                        coffeeSlotMachine.EmptyEjection();
                    }
                    else if (btn.Content.ToString().Equals("auswahl", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var product = Convert.ToString(cmbProduct.SelectedValue);

                        coffeeSlotMachine.SelectProduct(product);
                    }
                }
            }
        }
    }
}
