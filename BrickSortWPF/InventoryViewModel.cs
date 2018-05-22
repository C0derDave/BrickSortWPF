using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickSortWPF
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PartViewModel> Parts { get; set; }

        private Inventory Inventory { get; set; }

        /// <summary>
        /// Create an InventoryViewModel with a list of PartViewModel objects.
        /// </summary>
        /// <param name="setID">The LEGO® set ID to make an inventory for.</param>
        public InventoryViewModel(string setID)
        {
            Inventory = new Inventory(setID);
            Parts = new ObservableCollection<PartViewModel>();

            InitializePartsList();
        }

        private void InitializePartsList()
        {
            Parts.Clear();
            foreach(Part part in Inventory.Parts)
            {
                Parts.Add(new PartViewModel(part.Name, part.ID, part.RequiredQuantity, part.ImageURL));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
