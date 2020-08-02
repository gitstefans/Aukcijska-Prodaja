using ModelClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainWindowVM
{
    public class NewEditWindowVM:INotifyPropertyChanged
    {
        private Product currentProduct;
        private string windowTitle;
        
        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                if(currentProduct == value)
                {
                    return;
                }
                currentProduct = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentProduct"));
            }
        }
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                if (windowTitle == value)
                {
                    return;
                }
                windowTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WindowTitle"));
            }
        }
        public NewEditWindowVM(Product product)
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);

            CurrentProduct = product;
            WindowTitle = "Edit";
        }
        public NewEditWindowVM()
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);

            CurrentProduct = new Product();
            WindowTitle = "New Product";
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                if (saveCommand == value)
                {
                    return;

                }
                saveCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SaveCommand"));
            }
        }
        void SaveExecute(object obj)
        {
            if (CurrentProduct != null)
            {
                CurrentProduct.SaveProduct();
            }
        }
        bool CanSave(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
