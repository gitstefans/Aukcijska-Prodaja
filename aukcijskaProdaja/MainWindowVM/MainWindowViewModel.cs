using ModelClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace MainWindowVM
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        private Product currentProduct;
        //private List<Product> productList;
        private ProductCollection productList;
        private ListCollectionView productListView;
        private string filteringText;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                if (currentProduct == value)
                {
                    return;
                }
                currentProduct = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentProduct"));
            }
        }
        public ProductCollection ProductList
        {
            get { return productList; }
            set
            {
                if(productList == value)
                { return; }
                productList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductList"));
            }
        }
        public ListCollectionView ProductListView
        {
            get { return productListView; }
            set
            {
                if (productListView == value)
                {
                    return;
                }
                productListView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductListView"));
            }
        }

        public string FilteringText
        {
            get { return filteringText; }
            set
            {
                if (filteringText == value)
                {
                    return;
                }
                filteringText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilteringText"));
            }
        }

        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                if (deleteCommand == value) { return; }

                deleteCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteCommand"));
            }
        }
        void DeleteExecute(object obj)
        {
            
            CurrentProduct.DeleteProduct();
            ProductList.Remove(CurrentProduct);
            
        }
        bool CanDelete(object obj)
        {
            if (CurrentProduct == null) return false;

            return true;
        }

        public MainWindowViewModel()
        {
            DeleteCommand = new RelayCommand(DeleteExecute, CanDelete);
            this.PropertyChanged += MainWindowViewModel_PropertyChanged;

            ProductList = ProductCollection.GetAllProducts();

            ProductListView = new ListCollectionView(ProductList);
            productListView.Filter = ProductFilter;

            CurrentProduct = new Product();
            
            
        }
        private bool ProductFilter(object obj)
        {
            if (filteringText == null) return true;
            if (filteringText.Equals("")) return true;

            Product product = obj as Product;
            return (product.ProductName.ToLower().StartsWith(FilteringText.ToLower()));
        }
        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FilteringText"))
            {
                ProductListView.Refresh();
            }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        public void bidButtonMethod()
        {
            CurrentProduct.ProductPrice = CurrentProduct.ProductPrice + 1;
        }
    }
}
