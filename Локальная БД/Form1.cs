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

namespace Локальная_БД
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataSet bd,bd1,bd2,bd3,bd4,bd5;
        DataSet vi,vi1,vi2;
        SqlDataAdapter sql;
        string podkServer = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elami\source\repos\Локальная БД\Локальная БД\Database1.mdf;Integrated Security=True";
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Dolznost". При необходимости она может быть перемещена или удалена.
            this.dolznostTableAdapter.Fill(this.database1DataSet.Dolznost);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Sotrud". При необходимости она может быть перемещена или удалена.
            this.sotrudTableAdapter.Fill(this.database1DataSet.Sotrud);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dol = "select id_dol from Dolznost where name='" + comboBox4.Text + "'";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                bd5 = new DataSet();
                sql = new SqlDataAdapter(dol, podkl);
                sql.Fill(bd5);
            }

            string updete = "UPDATE Sotrud SET id_dol="+ bd5.Tables[0].Rows[0][0].ToString() + " where name='" + comboBox3.Text + "'";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                bd4 = new DataSet();
                sql = new SqlDataAdapter(updete, podkl);
                sql.Fill(bd4);
            }
            string v = "select Sotrud.name,Sotrud.age, Dolznost.name from Sotrud inner join Dolznost on Sotrud.id_dol = Dolznost.Id_dol";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                vi2 = new DataSet();
                sql = new SqlDataAdapter(v, podkl);
                sql.Fill(vi2);

                dataGridView3.DataSource = vi2.Tables[0];
                dataGridView3.Columns[0].HeaderCell.Value = "Имя";
            }
        }

        string name;int age;

        private void button2_Click(object sender, EventArgs e)
        {
            string sot = "delete Sotrud where name='" + comboBox1.Text + "'";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                bd3 = new DataSet();
                sql = new SqlDataAdapter(sot, podkl);
                sql.Fill(bd3);
                MessageBox.Show("Сотрудник удален", "", MessageBoxButtons.OK);
            }
            string v2 = "select Sotrud.name,Sotrud.age, Dolznost.name from Sotrud inner join Dolznost on Sotrud.id_dol = Dolznost.Id_dol";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                vi1 = new DataSet();
                sql = new SqlDataAdapter(v2, podkl);
                sql.Fill(vi1);

                dataGridView2.DataSource = vi1.Tables[0];
                dataGridView2.Columns[0].HeaderCell.Value = "Имя";
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            age =Convert.ToInt32(textBox2.Text);          
            string dol = "select id_dol from Dolznost where name='" + comboBox2.Text +"'";

            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                bd = new DataSet();
                sql = new SqlDataAdapter(dol, podkl);
                sql.Fill(bd);
            }

            string id_sot = "select MAX(id) from Sotrud";
            using (SqlConnection podkl = new SqlConnection(podkServer))
            {
                podkl.Open();
                bd2 = new DataSet();
                sql = new SqlDataAdapter(id_sot, podkl);
                sql.Fill(bd2);
            }
            int id_s = Convert.ToInt32(bd2.Tables[0].Rows[0][0]) + 1;

            if (age < 18)
            {
                MessageBox.Show("Сотрудни должен достигнуть 18-летия", "", MessageBoxButtons.OK);
            }
            else
            {
                string in_sot1 = "insert into Sotrud(id, name,age, id_dol) values(" + id_s.ToString() + ", '" + name + "' , " + age.ToString() + ", " + bd.Tables[0].Rows[0]["id_dol"].ToString() + ")";
                using (SqlConnection podkl = new SqlConnection(podkServer))
                {
                    podkl.Open();
                    bd1 = new DataSet();
                    sql = new SqlDataAdapter(in_sot1, podkl);
                    sql.Fill(bd1);
                    MessageBox.Show("Сотрудник добавлен", "", MessageBoxButtons.OK);
                }
                string v = "select Sotrud.name,Sotrud.age, Dolznost.name from Sotrud inner join Dolznost on Sotrud.id_dol = Dolznost.Id_dol";
                using (SqlConnection podkl = new SqlConnection(podkServer))
                {
                    podkl.Open();
                    vi = new DataSet();
                    sql = new SqlDataAdapter(v, podkl);
                    sql.Fill(vi);

                    dataGridView1.DataSource = vi.Tables[0];
                    dataGridView1.Columns[0].HeaderCell.Value = "Имя";
                }
            }
            

        }
    }
}
