using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace View
{
    /// <summary>
    /// Interaction logic for SearchAllProductsView.xaml
    /// </summary>
    public partial class SearchAllProductsView : Window
    {
        public ObservableCollection<Product> listOfChosenProducts = new ObservableCollection<Product>();
        ObservableCollection<Product> listOfAllProducts;
        public SearchAllProductsView(ObservableCollection<Product> listOfAll)
        {
            listOfAllProducts = listOfAll;
            InitializeComponent();
            productDataGrid.ItemsSource = listOfAllProducts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (productDataGrid.SelectedItems != null || productDataGrid.SelectedItems.Count == 0)
            {
                foreach(var item in productDataGrid.SelectedItems)
                {
                    listOfChosenProducts.Add((Product)item);
                }
            }
            DialogResult = true;
            Close();
        }
    }
}
