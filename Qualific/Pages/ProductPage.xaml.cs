using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using Qualific.Components;


namespace Qualific.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int actualPage = 0;
        public ProductPage()
        {
            InitializeComponent();
            DBConnect.db.Product.Load();
            Products = DBConnect.db.Product.Local;
            GeneralCount.Text = DBConnect.db.Product.Where(x => x.IsActive != false).Count().ToString();
        }
        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(ProductPage));
        private void Refresh()
        {
            IEnumerable<Product> prodL = DBConnect.db.Product;
            ObservableCollection<Product> products = Products;
            {
                if (CbSort == null)
                    return;
                if(CbSort.SelectedItem != null)
                {
                    switch((CbSort.SelectedItem as ComboBoxItem).Tag)
                    {
                        case "1":
                            prodL = DBConnect.db.Product;
                            break;
                        case "2":
                            prodL = prodL.OrderBy(x => x.Name);
                            break;
                        case "3":
                            prodL = prodL.OrderByDescending(x => x.Name);
                            break;
                        case "4":
                            prodL = prodL.OrderBy(x => x.DateIfAddition);
                            break;
                        default:
                            prodL = prodL.OrderByDescending(x => x.DateIfAddition);
                            break;
                             
                    }
                }
                if (TxtSearch == null)
                    return;
                if(TxtSearch.Text.Length > 0)
                {
                    prodL = prodL.Where(x => x.Name.StartsWith(TxtSearch.Text) || x.Description.StartsWith(TxtSearch.Text));
                }
                if (CbFilter == null)
                    return;
                if(CbFilter.SelectedItem != null)
                {
                    switch ((CbFilter.SelectedItem as ComboBoxItem).Tag)
                    {
                        case "1":
                            prodL = DBConnect.db.Product;
                            break;
                        case "2":
                            prodL = prodL.Where(x => x.UnitId == 1);
                            break;
                        default:
                            prodL = prodL.Where(x => x.UnitId == 2);
                            break;
                    }
                }
                if(CbCount.SelectedIndex > 0 && prodL.Count() > 0)
                {
                    int selCount = Convert.ToInt32((CbCount.SelectedItem as ComboBoxItem).Content);
                    prodL = new ObservableCollection<Product>(Products.Skip(selCount * actualPage).Take(selCount));
                    if(products.Count() == 0)
                    {
                        actualPage--;
                    }

                }
                FoundCount.Text = prodL.Count().ToString() + "из";
            }
            ProductList.ItemsSource = prodL.ToList();
        }
       

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            if(MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                DBConnect.db.Product.Remove(selProduct);
                DBConnect.db.SaveChanges();
                MessageBox.Show("Данные удалены");
                ProductList.ItemsSource = DBConnect.db.Product.ToList();
            }
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if (actualPage < 0)
                actualPage = 0;
            Refresh();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            Navigation.NexPage(new Nav("Редактирование продукта", new AddEditPage(selProduct)));
        }

        private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NexPage(new Nav("Добавление нового продукта", new AddEditPage(new Product())));
        }

        private void TxtSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            actualPage = 0;
            Refresh();

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();

        }

        
       
       

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DBConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ProductList.ItemsSource = DBConnect.db.Product.ToList();
            }
        }

        private void CbFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            actualPage = 0;
            Refresh();
        }

        private void CbCount_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
