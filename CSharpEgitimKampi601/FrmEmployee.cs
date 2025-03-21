using CSharpEgitimKampi601.Services;
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
    public partial class FrmEmployee: Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1";
        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query= "select * from employees order by employeeid";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        void DepartmentList()
        {
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query= "select * from departments order by departmentid";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "departmentname";
            cmbEmployeeDepartment.ValueMember = "departmentid";
            cmbEmployeeDepartment.DataSource = dataTable;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            DepartmentList();
            EmployeeList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into employees (employeename, employeesurname, employeesalary, departmentid) values (@p1, @p2, @p3, @p4)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", employeeName);
            command.Parameters.AddWithValue("@p2", employeeSurname);
            command.Parameters.AddWithValue("@p3", employeeSalary);
            command.Parameters.AddWithValue("@p4", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Employee added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            EmployeeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "delete from employees where employeeid=@p1";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Silme işlemi başarıyla gerçekleşti.");
            connection.Close();
            EmployeeList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "update employees set employeeName=@p1,employeeSurname=@p2,employeeSalary=@p3,departmentId=@p4 where employeeId=@p5";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", employeeName);
            command.Parameters.AddWithValue("@p2", employeeSurname);
            command.Parameters.AddWithValue("@p3", employeeSalary);
            command.Parameters.AddWithValue("@p4", departmentId);
            command.Parameters.AddWithValue("@p5", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işlemi başarıyla gerçekleşti.");
            connection.Close();
            EmployeeList();
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {

        }
    }
}
