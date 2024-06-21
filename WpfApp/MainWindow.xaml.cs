using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects_.Models;
using Services;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly IProductService iProductService;
        private readonly ICategoryService iCategoryService;

        public MainWindow()
        {
            InitializeComponent();
            iProductService = new ProductService();
            iCategoryService = new CategoryService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void LoadCategoryList()
        {
            try
            {
                var categories = iCategoryService.GetCategories();
                cboCategory.ItemsSource = categories;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading categories");
            }
        }

        private void LoadProductList()
        {
            try
            {
                var products = iProductService.GetProducts();
                
                dgData.ItemsSource = products;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading products");
            }
            finally
            {
                ResetInput();
            }
        }

        private void ResetInput()
        {
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtUnitsInStock.Text = string.Empty;
            cboCategory.SelectedIndex = -1;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Product
                {
                    ProductName = txtProductName.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitsInStock = short.Parse(txtUnitsInStock.Text),
                    CategoryId = (int)cboCategory.SelectedValue
                };
                iProductService.SaveProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product product)
            {
                txtProductID.Text = product.ProductId.ToString();
                txtProductName.Text = product.ProductName;
                txtPrice.Text = product.UnitPrice.ToString();
                txtUnitsInStock.Text = product.UnitsInStock.ToString();
                cboCategory.SelectedValue = product.CategoryId;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProductID.Text, out int productId))
                {
                    var product = new Product
                    {
                        ProductId = productId,
                        ProductName = txtProductName.Text,
                        UnitPrice = decimal.Parse(txtPrice.Text),
                        UnitsInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = (int)cboCategory.SelectedValue
                    };
                    iProductService.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("Please select a product to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProductID.Text, out int productId))
                {
                    var product = new Product { ProductId = productId };
                    iProductService.DeleteProduct(product);
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
