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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CountDogs();
            CountBirds();
            CountCats();
            Finance();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\AN3\Sem II\new\AnimalShelter\AnimalShelter\AnimalShelter.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        private void CountDogs()
        {
            string Cat = "Dog";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat='" + Cat + "'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Cat = "Birds";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCats()
        {
            string Cat = "Cats";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Finance()
        {
           
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Amt) from BillTbl",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FinanceLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void AnimalsLbl_Click(object sender, EventArgs e)
        {
            Animals Obj = new Animals();
            Obj.Show();
            this.Hide();

        }

        private void EmployeesLbl_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void AdoptersLbl_Click(object sender, EventArgs e)
        {
            Adopters Obj = new Adopters();
            Obj.Show();
            this.Hide();
        }

        private void CertificateLbl_Click(object sender, EventArgs e)
        {
            Certificate Obj = new Certificate();
            Obj.Show();
            this.Hide();
        }
    }

}
