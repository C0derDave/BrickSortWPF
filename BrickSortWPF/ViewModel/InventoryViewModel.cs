using BrickSortWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BrickSortWPF.ViewModel
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PartViewModel> Parts { get; set; }

        private Inventory Inventory { get; set; }


    
        /// <summary>
        /// Create an InventoryViewModel with a list of PartViewModel objects.
        /// </summary>
        public InventoryViewModel()
        {
            Parts = new ObservableCollection<PartViewModel>();
            Inventory = new Inventory();
        }

        public async Task LoadSet(string setID)
        {
            bool b = await Task.Run(() => Inventory.LoadSet(setID));
            Thread.Sleep(0);

            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Parts.Clear();
                foreach(Part part in Inventory.Parts)
                {
                    Parts.Add(new PartViewModel(part.Name, part.ID, part.RequiredQuantity, part.ImageURL));
                }
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
