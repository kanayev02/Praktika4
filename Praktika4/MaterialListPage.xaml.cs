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

namespace Praktika4
{
    /// <summary>
    /// Логика взаимодействия для MaterialListPage.xaml
    /// </summary>
    public partial class MaterialListPage : Page
    {
        List<Material> MatStart = DataBaseClass.DB.Material.ToList();
        List<MaterialSupplier> MatSupplier = DataBaseClass.DB.MaterialSupplier.ToList();
        public MaterialListPage()
        {
            InitializeComponent();
            LVMaterial.ItemsSource = MatStart;
        }
        private void TbSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<MaterialSupplier> mtList = DataBaseClass.DB.MaterialSupplier.Where(x => x.MaterialID == index).ToList();
            string str = "";
            foreach (MaterialSupplier item in mtList)
            {
                str += item.Supplier.Title + ", ";
            }
            if (mtList.Count == 0)
            {
                tb.Text = "Поставщики: отсутствуют";
            }
            else
            {
                tb.Text = "Поставщики: " + str.Substring(0, str.Length - 2);
            }
        }
        List<Material> MatFilter = new List<Material>();

        List<Material> MatSearch = new List<Material>();

        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBPoisk.Text != String.Empty)
            {
                MatSearch = MatStart.Where(x => x.Title.Contains(TBPoisk.Text)).ToList();
                FliterSort();
            }
            else
            {
                FliterSort();
            }
        }
        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FliterSort();
        }
        private void CbSortir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FliterSort();
        }
        private void FliterSort()
        {
            int filterIndex = CBFilter.SelectedIndex;

            if (TBPoisk.Text != String.Empty)
            {
                if (filterIndex != 0)
                {
                    MatFilter = MatSearch.Where(x => x.MaterialTypeID == filterIndex).ToList();
                }
                else
                {
                    MatFilter = MatSearch;
                }
            }
            else
            {
                if (filterIndex != 0)
                {
                    MatFilter = MatStart.Where(x => x.MaterialTypeID == filterIndex).ToList();
                }
                else
                {
                    MatFilter = MatStart;
                }
            }

        }


    }
}