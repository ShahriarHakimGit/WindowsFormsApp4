using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        //global variables
        private int posn = 0;
        SqlConnection cn;
        DataSet ds;
        SqlDataAdapter da;
        SqlCommandBuilder cb;
        DataTable custTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void form1_load(object sender, EventArgs e)
        {
            MakeConnection();
            GenerateData();

            if (ds.Tables.Contains("CustomersCopy"))
            {
                custTable = ds.Tables["CustomersCopy"];
                if (custTable.Rows.Count > 0)
                {
                    PopulateFields();
                }
                else
                {
                    MessageBox.Show("No customers found in the table.");
                }
            }
            else
            {
                MessageBox.Show("Table 'CustomersCopy' not found in DataSet.");
            }
        }


        private void MakeConnection()
        {
            string connectionString = "Server=172.20.10.3,1433;Database=Dafesty;User Id=sa;Password=reallyStrongPwd123;";
            cn = new SqlConnection(connectionString);
            try
            {
                cn.Open();
                MessageBox.Show("Connection Opened Successfully!");
                // Connection stays open here
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                cn.Close(); // Close only on error
            }
            // You can now use `cn` outside this block if needed

        }
        private void GenerateData() 
        {
            string sSQL = "SELECT CustomerID, CustomerName, PhoneNumber, EmailAddress from Customers";
            da = new SqlDataAdapter(sSQL, cn);
            cb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "CustomersCopy");
        }

        private void PopulateFields() 
        {
            txtCustomerID.Text = custTable.Rows[posn]["CustomerID"].ToString();
            txtCustomerName.Text = custTable.Rows[posn]["CustomerName"].ToString();
            txtPhoneNumber.Text = custTable.Rows[posn]["PhoneNumber"].ToString();
            txtEmailAddress.Text = custTable.Rows[posn]["EmailAddress"].ToString();

        }

        


        private void previousButton_Click(object sender, EventArgs e)
        {
            posn--;
            if(posn< 0)
            {
                posn = 0;
            }
            PopulateFields();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            //MakeConnection();
            posn++;
            //if posn is greater than row count
            int numberofRows = custTable.Rows.Count;
            int rowIndex = numberofRows - 1;
            if(posn >= rowIndex) 
            {
                posn = rowIndex;
            }
            PopulateFields();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Console.Write(txtCustomerID.Text);
        }

        private void firstButton_Click(object sender, EventArgs e)
        {
            posn = 0;
            PopulateFields();
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            posn = custTable.Rows.Count - 1;
            PopulateFields();
        }
    }
}
