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
using System.Windows.Shapes;

namespace BrickSortWPF
{
    /// <summary>
    /// Interaction logic for OpenSetWindow.xaml
    /// </summary>
    public partial class OpenSetDialog : Window
    {
        public OpenSetDialog()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return setID.Text; }
            set { setID.Text = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
