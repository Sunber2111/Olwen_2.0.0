
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Olwen_2._0._0.ViewModel
{
    
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<ProductViewModel>();

            SimpleIoc.Default.Register<CustomerViewModel>();

            SimpleIoc.Default.Register<EmployeeViewModel>();

            SimpleIoc.Default.Register<SupplierViewModel>();

            SimpleIoc.Default.Register<StoreViewModel>();

            SimpleIoc.Default.Register<SalesViewModel>();

            SimpleIoc.Default.Register<CreateOrderViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ProductViewModel Product
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductViewModel>();
            }
        }

        public CustomerViewModel Customer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CustomerViewModel>();
            }
        }

        public EmployeeViewModel Employee
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EmployeeViewModel>();
            }
        }
        
        public SupplierViewModel Supplier
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SupplierViewModel>();
            }
        }

        public StoreViewModel Store
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StoreViewModel>();
            }
        }

        public SalesViewModel Sales
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SalesViewModel>();
            }
        }

        public CreateOrderViewModel CreateOrder
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateOrderViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}