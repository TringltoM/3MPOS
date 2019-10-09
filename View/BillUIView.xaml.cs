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
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace View
{
    /// <summary>
    /// Interaction logic for BillUIView.xaml
    /// </summary>
    public partial class BillUIView : UserControl
    {
        ObservableCollection<Product> selectedProductList = new ObservableCollection<Product>();

        List<Product> listOfAllProducts = new List<Product>();

        public BillUIView()
        {
            InitializeComponent();

            selectedProductList.Add(new Product { Name = "Product one", Price = 5.5, Description = "A short description of Product one" });
            selectedProductList.Add(new Product { Name = "Product two", Price = 75, Description = "A short description of Product two" });
            selectedProductList.Add(new Product { Name = "Product three", Price = 9.3, Description = "A short description of Product three" });
            selectedProductList.Add(new Product { Name = "Product four", Price = 129, Description = "A short description of Product four" });

            for (int i = 1; i <= 10; i++)
            {
                listOfAllProducts.Add(new Product { Name = $"Number {i} product", Price = (i + 3) / 2.9, Description = $"Description of product {i}"});
            }

            productGrid.DataContext = selectedProductList;
        }

       

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quickSearchTextBox.Text == "Quick Search")
            {
                quickSearchTextBox.Text = string.Empty;
            }
        }

        private void QuickSearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quickSearchTextBox.Text == string.Empty)
            {
                quickSearchTextBox.Text = "Quick Search";
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (productGrid.SelectedItem != null)
            {
                selectedProductList.Remove((Product)productGrid.SelectedItem);
                removeButton.IsEnabled = false;
            }
        }

        private void AddItem(Product obj)
        {
            TextBlock block = new TextBlock();

            block.Text = obj.Name;

            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            block.MouseLeftButtonUp += (sender, e) =>
            {
                quickSearchTextBox.Text = block.Text;
                resultStack.Children.Clear();
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                addButton.IsEnabled = true;
                border.Visibility = Visibility.Collapsed;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            resultStack.Children.Add(block);

            

        }
        private void QuickSearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {

            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = listOfAllProducts;
            
            string query = (sender as TextBox).Text;

            if (query.Length == 0 || query == "Quick Search")
            {
                resultStack.Children.Clear();
                border.Visibility = Visibility.Collapsed;
            }
            else
            {
                border.Visibility = Visibility.Visible;
            }

            resultStack.Children.Clear();

            foreach(Product obj in data)
            {
                if (obj.Name.ToLower().Contains(query.ToLower()))
                {
                    AddItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
            }

            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(quickSearchTextBox.Text != "Quick Search" && quickSearchTextBox.Text != null && quickSearchTextBox.Text != string.Empty)
            {
                Product p = listOfAllProducts.Find(x => x.Name == quickSearchTextBox.Text);
                if (p != null)
                {
                    selectedProductList.Add(p);
                    quickSearchTextBox.Text = "Quick Search";
                    addButton.IsEnabled = false;
                }
            }
        }

        private void ProductGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!removeButton.IsMouseOver)
            {
                productGrid.SelectedItem = null;
                removeButton.IsEnabled = false;
            }
            
        }

        private void ProductGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (productGrid.SelectedCells != null)
            {
                removeButton.IsEnabled = true;
            }
        }

        private void SearchAllButton_Click(object sender, RoutedEventArgs e)
        {
            new SearchAllProductsView().ShowDialog();
        }
    }

    public class Product
    {
        public Product()
        {

        }
        public override string ToString()
        {
            return Name;
        }

        public string ToLower()
        {
            return Name.ToLower();
        }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
