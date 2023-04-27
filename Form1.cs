using MySqlConnector;
using System;
using System.Windows.Forms;

namespace exam_pk111
{
    public partial class Form1 : Form
    {
        //private MySqlConnection conn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //conn = new MySqlConnection("Server=localhost;User ID=root;Password=root;Database=exam_pk111");
            if (DBC.GetConn() == null)
            {
                label5.Text = "Нет подключения к БД";
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //conn.Open();
            string motherboard = mb.Text.Trim();
            string processor = proc.Text.Trim();
            string videocard = vid.Text.Trim();
            string power = bp.Text.Trim();
            string sqlIns = "INSERT INTO computers (motherboard,processor,videocard,power) VALUES (@mb,@pr,@vi,@po)";
            MySqlCommand ins = new MySqlCommand(sqlIns, DBC.GetConn());
            ins.Parameters.AddWithValue("@mb", motherboard);
            ins.Parameters.AddWithValue("@pr", processor);
            ins.Parameters.AddWithValue("@vi", videocard);
            ins.Parameters.AddWithValue("@po", power);
            ins.ExecuteNonQuery();
            //conn.Close();
            DBC.CloseConn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //conn.Open();
            listBox1.Items.Clear();
            string sqlSel = "SELECT motherboard,processor,videocard,power FROM computers";
            MySqlCommand sel = new MySqlCommand(sqlSel, DBC.GetConn());
            MySqlDataReader selRead = sel.ExecuteReader();
            while (selRead.Read())
            {
                listBox1.Items.Add(selRead.GetString(0) + " " + selRead.GetString(1) + " " + selRead.GetString(2) + " " + selRead.GetString(3));
            }
            //conn.Close();
            DBC.CloseConn();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                button3.Enabled = true;
            } else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled=false;
            checkBox1.Checked=false;
        }
    }
}