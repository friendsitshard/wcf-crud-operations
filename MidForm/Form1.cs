using MidForm.MyServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace MidForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int selectedId = 0;
        private int currentOrderId = 0;
        MyServiceReference.MidServiceClient midServiceClient = new MyServiceReference.MidServiceClient();

        private void Form1_Load(object sender, EventArgs e)
        {
            TableComboBox.Text = "Users";

            var data = midServiceClient.showUsers();
            dataGridView1.DataSource = data;
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                midServiceClient.insertUser(EmailTextBox.Text, PasswordTextBox.Text);
                var data = midServiceClient.showUsers();
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("Empty email or password", "Error");
            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (selectedId != 0)
            {
                
                if (!string.IsNullOrEmpty(EmailTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    midServiceClient.updateUser(selectedId, EmailTextBox.Text, PasswordTextBox.Text);
                    var data = midServiceClient.showUsers();
                    dataGridView1.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Empty email or password ", "Error");
                }
            }
            else
            {
                MessageBox.Show("Choose ID by clicking on cell", "Error");
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedId != 0)
            {
                midServiceClient.deleteUser(selectedId);
                var data = midServiceClient.showUsers();
                dataGridView1.DataSource = data;
                selectedId = 0;
            }
            else
            {
                MessageBox.Show("Choose ID by clicking on cell", "Error");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TableComboBox.Text == "Users")
            {
                selectedId = (int)dataGridView1.Rows[e.RowIndex].Cells["user_id"].Value;
                var user = midServiceClient.getUser(selectedId);
                EmailTextBox.Text = user.email.ToString();
                PasswordTextBox.Text = user.password.ToString();
            }
            else
            {
                currentOrderId = (int)dataGridView1.Rows[e.RowIndex].Cells["order_id"].Value;
                var order = midServiceClient.getOrder(currentOrderId);
                NameTextBox.Text = order.name.ToString();
                PriceTextBox.Text = order.price.ToString();
                UserIdTextBox.Text = order.user_id.ToString();
            }
        }

        private void TableComboBox_TextChanged(object sender, EventArgs e)
        {
            selectedId = 0;
            currentOrderId = 0;
            if (TableComboBox.Text == "Users")
            {
                var data = midServiceClient.showUsers();
                dataGridView1.DataSource = data;
            }
            else if(TableComboBox.Text == "Orders")
            {
                var data = midServiceClient.showOrders();
                dataGridView1.DataSource = data;
            }
        }

        private void InsertOrderButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTextBox.Text) && !string.IsNullOrEmpty(PriceTextBox.Text) && !string.IsNullOrEmpty(UserIdTextBox.Text))
            {
                midServiceClient.insertOrder(NameTextBox.Text, PriceTextBox.Text, Convert.ToInt32(UserIdTextBox.Text));
                var data = midServiceClient.showOrders();
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("Empty name, price or user ID", "Error");
            }
        }

        private void UpdateOrderButton_Click(object sender, EventArgs e)
        {
            if(currentOrderId != 0)
            {
                if (!string.IsNullOrEmpty(NameTextBox.Text) && !string.IsNullOrEmpty(PriceTextBox.Text) && !string.IsNullOrEmpty(UserIdTextBox.Text))
                {
                    midServiceClient.updateOrder(currentOrderId, NameTextBox.Text, PriceTextBox.Text, Convert.ToInt32(UserIdTextBox.Text));
                    var data = midServiceClient.showOrders();
                    dataGridView1.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Empty name, price or user ID", "Error");
                }
            }
            else
            {
                MessageBox.Show("Choose ID by clicking on cell", "Error");
            }
        }

        private void DeleteOrderButton_Click(object sender, EventArgs e)
        {
            if (currentOrderId != 0)
            {
                midServiceClient.deleteOrder(currentOrderId);
                var data = midServiceClient.showOrders();
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("Choose ID by clicking on cell", "Error");
            }
        }
    }
}
