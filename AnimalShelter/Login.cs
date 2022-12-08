using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AnimalShelter
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\AN3\Sem II\new\AnimalShelter\AnimalShelter\AnimalShelter.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        private void ResetLbl_Click(object sender, EventArgs e)
        {
            UsernameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UsernameTb.Text==""|| PasswordTb.Text=="")
            {
                MessageBox.Show("Enter both Username and Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl where EmpName='"+UsernameTb.Text+"' and EmpPass='"+PasswordTb.Text+"'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString()=="1")
                {
                    Home obj = new Home();
                    obj.Show();
                    this.Hide();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("Wrong UserName or Password");
                }
                Con.Close();
            }
        }
    }
}
