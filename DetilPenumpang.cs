using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerbangSaja
{
    public partial class DetilPenumpang : Form
    {
        public DetilPenumpang()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RIO-LAPTOP\Documents\TerbangSajaDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from Penumpang";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);   
            PenumpangDetil.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void DetilPenumpang_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Penumpang addpas = new Penumpang();
            addpas.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(PidTb.Text == "")
            {
                MessageBox.Show("Masukkan penumpang yang akan dihapus");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from Penumpang where PassId=" + PidTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penumpang Telah dihapus");
                    Con.Close();
                    populate();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void PenumpangDetil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PidTb.Text = PenumpangDetil.SelectedRows[0].Cells[0].Value.ToString();
            PnameTb.Text = PenumpangDetil.SelectedRows[0].Cells[1].Value.ToString();
            PpassTb.Text = PenumpangDetil.SelectedRows[0].Cells[2].Value.ToString();
            PaddTb.Text = PenumpangDetil.SelectedRows[0].Cells[3].Value.ToString();
            natcb.SelectedItem = PenumpangDetil.SelectedRows[0].Cells[4].Value.ToString();
            GendCb.SelectedItem = PenumpangDetil.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PidTb.Text = "";
            PnameTb.Text = "";
            PpassTb.Text = "";
            PaddTb.Text = "";
            natcb.SelectedItem = "";
            GendCb.SelectedItem = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(PidTb.Text == "" || PnameTb.Text == "" || PpassTb.Text == "" || PaddTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update Penumpang set PassName='" + PnameTb.Text + "',Passport='" + PpassTb.Text + "',PassAd='" + PaddTb.Text + "',Kenegaraan='" + natcb.SelectedItem.ToString() + "',Kelamin='" + GendCb.SelectedItem.ToString() + "',Telepon='" + PphoneTb.Text + "' where PassId=" + PidTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Penumpang sukses diperbarui");
                    Con.Close();
                    populate();
                }catch(Exception Ex)
                {
                    MessageBox.Show("Missing Information");
                }
            }
        }
    }
}
