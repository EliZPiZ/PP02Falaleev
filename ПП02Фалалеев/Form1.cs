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

namespace ПП02Фалалеев
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PE4P317\SQLEXPRESS2022;Initial Catalog=ПП02Фалалеев;Integrated Security=True");
            // очищаем listView1
            listView1.Items.Clear();

            // создаем список изображений для строк listView1
            ImageList imageList = new ImageList();

            // устанавливаем размер изображений
            imageList.ImageSize = new Size(100, 100);

            con.Open();
            SqlCommand cons = new SqlCommand($"SELECT [Тип агента] = Concat([Тип агента], ' #', ID), [Наименование агента], [Телефонагента], [Логотип агента] FROM агенты", con);
            SqlDataReader read = cons.ExecuteReader();
            List<string> type = new List<string>();
            List<string> name = new List<string>();
            List<string> phone = new List<string>();

            while (read.Read())
            {
                type.Add(read.GetValue(0).ToString());
                name.Add(read.GetValue(1).ToString());
                phone.Add(read.GetValue(2).ToString());

                try
                {
                    imageList.Images.Add(Image.FromFile(System.IO.Directory.GetCurrentDirectory() + read.GetString(3)));
                    imageList1.Images.Add(Image.FromFile(System.IO.Directory.GetCurrentDirectory() + read.GetString(3)));
                }
                catch
                {
                    imageList.Images.Add(Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\agents\picture.png"));
                }
            }
            con.Close();
            // создадим пустое изображение (просто белая заливка)
            Bitmap emptyImage = new Bitmap(180, 150);

            // получим объект Graphics для редактирования изображения
            using (Graphics gr = Graphics.FromImage(emptyImage))
            {
                // выполним заливку изображения emptyImage белым цветом
                gr.Clear(Color.White);
            }

            // и добавим его в список
            imageList.Images.Add(emptyImage);
            
            // устанавливаем в listView1 список изображений imageList
            listView1.SmallImageList = imageList;

            // добавляем строки в listView1
            for (int i = 0; i < type.Count; i++)
            {
                // создадим объект ListViewItem (строку) для listView1
                ListViewItem listViewItem = new ListViewItem(new string[] { type[i], name[i], phone[i] });

                // индекс изображения из imageList для данной строки listViewItem
                listViewItem.ImageIndex = i;

                // добавляем созданный элемент listViewItem (строку) в listView1
                listView1.Items.Add(listViewItem);
            }
        }

    }
}
