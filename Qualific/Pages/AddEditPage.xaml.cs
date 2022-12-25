using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Qualific.Components;

namespace Qualific.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Components.Product product;
        
        public AddEditPage(Components.Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;
            UnitCb.ItemsSource = DBConnect.db.Unit.ToList();
            UnitCb.DisplayMemberPath = "Name";
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                product.MainImage = File.ReadAllBytes(openFileDialog.FileName);
                ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void SaveCb_Click(object sender, RoutedEventArgs e)
        {
            if (product.Id == 0)
            {
                DBConnect.db.Product.Add(product);
            }
            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено");
        }
    }
}
