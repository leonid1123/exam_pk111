using MySqlConnector;
using System;
using System.Windows.Forms;

namespace exam_pk111
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection("Server=localhost;User ID=root;Password=root;Database=exam_pk111");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string motherboard = mb.Text.Trim();
            string processor = proc.Text.Trim();
            string videocard = vid.Text.Trim();
            string power = bp.Text.Trim();
            string sqlIns = "INSERT INTO computers (motherboard,processor,videocard,power) VALUES (@mb,@pr,@vi,@po)";
            MySqlCommand ins = new MySqlCommand(sqlIns, conn);
            ins.Parameters.AddWithValue("@mb", motherboard);
            ins.Parameters.AddWithValue("@pr", processor);
            ins.Parameters.AddWithValue("@vi", videocard);
            ins.Parameters.AddWithValue("@po", power);
            ins.ExecuteNonQuery();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            listBox1.Items.Clear();
            string sqlSel = "SELECT motherboard,processor,videocard,power FROM computers";
            MySqlCommand sel = new MySqlCommand(sqlSel, conn);
            MySqlDataReader selRead = sel.ExecuteReader();
            while (selRead.Read())
            {
                listBox1.Items.Add(selRead.GetString(0) + " " + selRead.GetString(1) + " " + selRead.GetString(2) + " " + selRead.GetString(3));
            }
            conn.Close();
        }
    }
}