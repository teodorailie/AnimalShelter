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
    public partial class Certificate : Form
    {
        public Certificate()
        {
            InitializeComponent();
           //EmpNameLbl.Text = Login.Employees;
            GetAdopters();
            DisplayProduct();
            DisplayTransactions();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\AN3\Sem II\new\AnimalShelter\AnimalShelter\AnimalShelter.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    private void GetAdopters()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select AdoptId from AdopterTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("AdoptId", typeof(int));
            dt.Load(Rdr);
            AdoptIdCb.ValueMember = "AdoptId";
            AdoptIdCb.DataSource = dt;
            Con.Close();
        }
        private void DisplayProduct()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            APDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void DisplayTransactions()
        {
            Con.Open();
            string Query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void GetAdoptName()
        {
            Con.Open();
            string Query = "Select * from AdopterTbl where AdoptId='" + AdoptIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                AdoptNameTb.Text = dr["AdoptName"].ToString();
            }
            Con.Close();
        }
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PrQty=@PQ where PrId=@Pkey",Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);
                cmd.ExecuteNonQuery();
                //MessageBox("Product Edited");
                Con.Close();
                DisplayProduct();
                //Clear();
            }
            catch(Exception Ex)
            { MessageBox.Show(Ex.Message);
            }
        }

        int n = 0, GrdTotal = 0;
        int Key = 0, Stock = 0;
       

        private void RestBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void AdoptIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
           GetAdoptName();

        }

        private void APDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = APDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            //CatCb.Text = APDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock =Convert.ToInt32( APDGV.Rows[e.RowIndex].Cells[3].Value.ToString());
            QtyTb.Text= APDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            PrPriceTb.Text = APDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            if(PrNameTb.Text=="")
            {
                Key = 0;
            }
            else { Key = Convert.ToInt32(APDGV.Rows[e.RowIndex].Cells[0].Value.ToString()); }
        }

        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl (BDate,AdoptId,AdoptName,EmpName,Amt) values(@BD,@AI,@AN,@EN,@Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@AI", AdoptIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AN", AdoptNameTb.Text);
                cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Added");
                Con.Close();
                DisplayTransactions();
                //Clear();
            }
            catch (Exception Ex)
            { MessageBox.Show(Ex.Message); }
        }
        string prodname;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprm", 285, 600);
                if (printPreviewDialog1.ShowDialog()==DialogResult.OK)
            
                {
                printDocument1.Print();

                }
           

        }
        int prodid, prodqty, prodprice, tottal, pos = 60;

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

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void AdoptNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnimalsLbl_Click(object sender, EventArgs e)
        {
            Animals Obj = new Animals();
            Obj.Show();
            this.Hide();
        }

        private void HomeLbl_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Animal Shelter", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("Id Product Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
                foreach(DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                    e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                    pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total: Total" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("***AnimalShelter***", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
                 BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void Reset()
        {
            AnimalLbl.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            Key = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
        if(QtyTb.Text==""||Convert.ToInt32(QtyTb.Text)>Stock)
            { MessageBox.Show("No enough in house"); }
        else if(QtyTb.Text==""||Key==0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = AnimalLbl.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PrPriceTb.Text;
                newRow.Cells[4].Value = total;
                GrdTotal = GrdTotal + total;
                BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = "Total" + GrdTotal;
                UpdateStock();
                Reset();
            }
        }

       
    }
}
