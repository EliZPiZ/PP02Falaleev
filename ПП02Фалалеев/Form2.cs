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
using System.IO;

namespace ПП02Фалалеев
{
    public partial class Form2 : Form
    {
        string FileName = "";
        string FilePath = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FileName == "")
            {
                MessageBox.Show("Загрузите фото!");
                return;
            }
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PE4P317\\SQLEXPRESS2022;Initial Catalog=ПП02Фалалеев;Integrated Security=True");

            con.Open();
            SqlCommand cons = new SqlCommand($"INSERT INTO агенты ([Тип агента], [Наименование агента], [Электронная почта агента], " +
                $"[Телефонагента], [Юридический адрес], [Директор], [Логотип агента]) VALUES('{textBox1.Text}' ," +
                $" '{textBox2.Text}' " +
                $",  '{textBox3.Text}' " +
                $",  '{textBox4.Text}' " +
                $", '{textBox5.Text}' " +
                $", '{textBox6.Text}' " +
                $", '{"\\agents\\" + FileName}' )", con);
            cons.ExecuteNonQuery();
            con.Close();

            if(!File.Exists(System.IO.Directory.GetCurrentDirectory()+ "\\agents\\" + FileName))
            {
                File.Copy(FilePath, System.IO.Directory.GetCurrentDirectory() + "\\agents\\" + FileName);
            }

            MessageBox.Show("Добавлен!", "Info");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                FileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                FilePath = openFileDialog.FileName;
            }
        }
    }
}
