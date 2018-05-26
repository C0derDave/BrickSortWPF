using BrickSortWPF.ViewModel;
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

namespace BrickSortWPF
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenSetDialog dialog = new OpenSetDialog();
            if (dialog.ShowDialog() == true)
            {
                string setID = dialog.ResponseText;
                ((InventoryViewModel)DataContext).LoadSet(setID);
                Action action = (() =>
                {
                    ((InventoryViewModel)DataContext).LoadSet(setID);
                });
                this.Dispatcher.BeginInvoke(action);
            }
        }
    }
}
