using BrickSortWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BrickSortWPF.ViewModel
{
    public class PartViewModel : INotifyPropertyChanged
    {
        private Part _part;

        public string Name { get; set; }
        
        public string UniqueID { get; set; }

        public int RequiredQuantity { get; set; }

        public string Color { get; set; }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string ImageURL { get; set; }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        p => true,
                        p => Add());
                }
                return _addCommand;
            }
        }

        private ICommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(
                        p => true,
                        p => Remove());
                }
                return _removeCommand;
            }
        }

        public PartViewModel(string name, string id, string color, int requiredQuantity, string imageURL)
        {
            //_part = part;
            Name = name;
            UniqueID = id;
            Color = color;
            Quantity = 0;
            RequiredQuantity = requiredQuantity;

            ImageURL = imageURL;
            Uri uri = new Uri(imageURL, UriKind.Absolute);
            Image = new BitmapImage(uri);
            OnPropertyChanged(nameof(Image));
        }

        public void Add()
        {
            Quantity++;
            //_part.Quantity++;
        }

        public void Remove()
        {
            if (Quantity > 0)
            {
                Quantity--;
                //_part.Quantity--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
