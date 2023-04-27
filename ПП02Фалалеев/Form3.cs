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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ПП02Фалалеев
{
    public partial class Form3 : Form
    {
        DataTable Jobs = new DataTable();
        public Form3()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-PE4P317\\SQLEXPRESS2022;Initial Catalog=ПП02Фалалеев;Integrated Security=True");
          
            con.Open();
            SqlCommand cons = new SqlCommand($"SELECT Агенты = [Наименование агента], ID, Тип = [Тип агента], Наименование = [Наименование агента], Почта = [Электронная почта агента]" +
                $", Телефон = Телефонагента, Адрес = [Юридический адрес], Директор, Фото = [Логотип агента] FROM агенты", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cons);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Агенты";
            listBox1.DataSource = dataTable;
            Jobs = dataTable;
            
            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFields((int)listBox1.SelectedValue);
        }
        private void FillFields(int id)
        {
            for(int i =0; i < Jobs.Rows.Count; i++)
            {
                if ((int)Jobs.Rows[i]["ID"] == id)
                {
                    pictureBox1.ImageLocation = System.IO.Directory.GetCurrentDirectory() + Jobs.Rows[i]["Фото"];
                    textBox1.Text = Jobs.Rows[i]["Тип"].ToString();
                    textBox2.Text = Jobs.Rows[i]["Наименование"].ToString();
                    textBox3.Text = Jobs.Rows[i]["Почта"].ToString();
                    textBox4.Text = Jobs.Rows[i]["Телефон"].ToString();
                    textBox5.Text = Jobs.Rows[i]["Адрес"].ToString();
                    textBox6.Text = Jobs.Rows[i]["Директор"].ToString();

                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02Фалалеев;Integrated Security=True");

            con.Open();
            SqlCommand cons = new SqlCommand($"DELETE FROM агенты WHERE ID = {listBox1.SelectedValue}", con);
            cons.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Удален!", "Info");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02Фалалеев;Integrated Security=True");

            con.Open();
            SqlCommand cons = new SqlCommand($"UPDATE агенты SET [Тип агента] = '{textBox1.Text}' ," +
                $" [Наименование агента] = '{textBox2.Text}' " +
                $", [Электронная почта агента] = '{textBox3.Text}' " +
                $", [Телефонагента] = '{textBox4.Text}' " +
                $", [Юридический адрес] = '{textBox5.Text}' " +
                $", [Директор] = '{textBox6.Text}' WHERE ID = {listBox1.SelectedValue}", con);
            cons.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Сохранено!", "Info");
        }
    }
}
