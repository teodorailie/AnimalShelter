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
    public partial class Adopters : Form
    {
        public Adopters()
        {
            InitializeComponent();
            DisplayAdopters();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\AN3\Sem II\new\AnimalShelter\AnimalShelter\AnimalShelter.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        private void DisplayAdopters()
        {
            Con.Open();
            string Query = "Select * from AdopterTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AdoptersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
            Key = 0;

        }
        
        int Key = 0;
       // private void AdoptersDGV_RowHeaderMouseClick(object sender, DataGridViewCellEventArgs e)
        //{
          //  Key = Convert.ToInt32(AdoptersDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            //CustNameTb.Text = AdoptersDGV.SelectedRows[0].Cells[1].Value.ToString();
            //CustAddTb.Text = AdoptersDGV.SelectedRows[0].Cells[2].Value.ToString();
            //CustPhoneTb.Text = AdoptersDGV.SelectedRows[0].Cells[3].Value.ToString();

        //}
        private void SaveBtn_Click_1(object sender, EventArgs e)
        {

            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AdopterTbl (AdoptName,AdoptAdd,AdoptPhone) values(@AN,@AA,@AP)", Con);
                    cmd.Parameters.AddWithValue("@AN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@AA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@AP", CustPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Adopter Added");
                    Con.Close();
                    DisplayAdopters();
                    Clear();
                }
                catch (Exception Ex)
                { MessageBox.Show(Ex.Message); }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AdopterTbl set AdoptName=@AN,AdoptAdd=@AA,AdoptPhone=@AP where AdoptId=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@AA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@AP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Adopter Updated");
                    Con.Close();
                    DisplayAdopters();
                    Clear();
                }
                catch (Exception Ex)
                { MessageBox.Show(Ex.Message); }
            }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Adopter");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AdopterTbl where AdoptId = @AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Adopter Deleted");
                    Con.Close();
                    DisplayAdopters();
                    Clear();
                }
                catch (Exception Ex)
                { MessageBox.Show(Ex.Message); }
            }

        }

       // private void AdoptersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
          //  CustNameTb.Text = AdoptersDGV.SelectedRows[0].Cells[1].Value.ToString();
            //CustAddTb.Text = AdoptersDGV.SelectedRows[0].Cells[2].Value.ToString();
            //CustPhoneTb.Text = AdoptersDGV.SelectedRows[0].Cells[3].Value.ToString();

//            if (CustNameTb.Text == "")
  //          { Key = 0; }
    //        else
      //      {
        //        Key = Convert.ToInt32(AdoptersDGV.SelectedRows[0].Cells[0].Value.ToString());
          //  }
       // }

        private void HomeLbl_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
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

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void AdoptersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            CustNameTb.Text = AdoptersDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            CustAddTb.Text = AdoptersDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            CustPhoneTb.Text = AdoptersDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else { Key = Convert.ToInt32(AdoptersDGV.Rows[e.RowIndex].Cells[0].Value.ToString()); }
        }
    }
}
