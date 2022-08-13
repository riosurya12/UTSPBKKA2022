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

namespace TerbangSaja
{
    public partial class DetilTerbang : Form
    {
        public DetilTerbang()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RIO-LAPTOP\Documents\TerbangSajaDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from FlightTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PenerbanganDetil.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DetilTerbang_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Flight Addfl = new Flight();
            Addfl.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Seatnum.Text = "";

        }

        private void PenerbanganDetil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FcodeTb.Text = PenerbanganDetil.SelectedRows[0].Cells[0].Value.ToString();
            SrcCb.SelectedItem = PenerbanganDetil.SelectedRows[0].Cells[1].Value.ToString();
            DstCb.SelectedItem = PenerbanganDetil.SelectedRows[0].Cells[2].Value.ToString();
            Seatnum.Text = PenerbanganDetil.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Masukkan penerbangan yang akan dihapus");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penerbangan Telah dihapus");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
