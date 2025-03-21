using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer: Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;User Id=postgres;Password=1;";
       
        void GetAllCustomers()
        {
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * from Customers order by CustomerId";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query= "insert into Customers (CustomerName,CustomerSurname,CustomerCity) values (@p1,@p2,@p3)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", customerName);
            command.Parameters.AddWithValue("@p2", customerSurname);
            command.Parameters.AddWithValue("@p3", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarıyla gerçekleşti.");
            connection.Close();
            GetAllCustomers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "delete from Customers where CustomerId=@customerId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Silme işlemi başarıyla gerçekleşti.");
            connection.Close();
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            int id= int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "update Customers set CustomerName=@p1,CustomerSurname=@p2,CustomerCity=@p3 where CustomerId=@p4";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", customerName);
            command.Parameters.AddWithValue("@p2", customerSurname);
            command.Parameters.AddWithValue("@p3", customerCity);
            command.Parameters.AddWithValue("@p4", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işlemi başarıyla gerçekleşti.");
            connection.Close();
            GetAllCustomers();

        }

        private void btnGetById_Click(object sender, EventArgs e)
        {

        }
    }
}
