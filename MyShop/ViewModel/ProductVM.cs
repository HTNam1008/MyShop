using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.DAO;

namespace MyShop.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        private DataTable _mobileData;
        private DataTable _filteredData;
        private int _itemsPerPage = 10;
        private DataView _filteredDataView;
        private int _currentPage = 1;

        public string[] NameOS { get; set; }
        public DataTable FilteredData => _filteredData;

        public ProductVM()
        {
            // Initialize commands and properties
            // Initialize other necessary components
        }

        public async Task LoadDataAsync()
        {
            await Database.Instance.ImportDataToSQLAsync();
            LoadDataIntoGrid();
        }

        private void LoadDataIntoGrid()
        {
            var sql = $"SELECT ID, Name, OS, Price, PriceOriginal, Quantity, Manufacturer, MemoryStorage, Details, Image FROM {Database.Instance.tablePhone}";
            var command = new SqlCommand(sql, Database.Instance.Connection);
            if (Database.Instance.Connection != null) { Database.Instance.Connection.Close(); }

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(_mobileData);
            SetPageComboBox();
            SetPriceRangeComboBox();
        }

        private void SetPageComboBox()
        {
            // SetPageComboBox logic
        }

        private void SetPriceRangeComboBox()
        {
            // SetPriceRangeComboBox logic
        }

        public void ApplyFilters()
        {
            // ApplyFilters logic
        }

        // Other methods and properties...
    }
}
