using MySqlConnector;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace exam_pk111
{
    public partial class Form1 : Form
    {
        //private MySqlConnection conn = null;
        List<Comp> mylist = new List<Comp>();
        bool windowOpen = false;
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
            if (motherboard.Length > 0 && processor.Length > 0 && videocard.Length > 0 && power.Length > 0)
            {
                string sqlIns = "INSERT INTO computers (motherboard,processor,videocard,power) VALUES (@mb,@pr,@vi,@po)";
                MySqlCommand ins = new MySqlCommand(sqlIns, DBC.GetConn());
                ins.Parameters.AddWithValue("@mb", motherboard);
                ins.Parameters.AddWithValue("@pr", processor);
                ins.Parameters.AddWithValue("@vi", videocard);
                ins.Parameters.AddWithValue("@po", power);
                ins.ExecuteNonQuery();
                //conn.Close();
                DBC.CloseConn();
                GetDB();
            }
            else
            {
                MessageBox.Show("пустые поля нельзя!!!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetDB();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            checkBox1.Checked = false;
            int selectedID = mylist[listBox1.SelectedIndex].getId();
            String upd = "UPDATE computers SET motherboard=@mb,processor=@pr,videocard=@vc,power=@pw WHERE id=@id";
            MySqlCommand updCmd = new MySqlCommand(upd, DBC.GetConn());
            updCmd.Parameters.AddWithValue("@mb", mb.Text.Trim());
            updCmd.Parameters.AddWithValue("@pr", proc.Text.Trim());
            updCmd.Parameters.AddWithValue("@vc", vid.Text.Trim());
            updCmd.Parameters.AddWithValue("@pw", bp.Text.Trim());
            updCmd.Parameters.AddWithValue("@id", selectedID);
            updCmd.ExecuteNonQuery();
            GetDB();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(listBox1.SelectedIndex);
            //Console.WriteLine(mylist[listBox1.SelectedIndex].PrintInfo());
            String[] allInfo = mylist[listBox1.SelectedIndex].PrintInfo().Split('|');
            mb.Text = allInfo[0];
            proc.Text = allInfo[1];
            vid.Text = allInfo[2];
            bp.Text = allInfo[3];
        }
        void GetDB()
        {
            mylist.Clear();
            listBox1.Items.Clear();
            string sqlSel = "SELECT motherboard,processor,videocard,power,id FROM computers ORDER BY id ASC";
            MySqlCommand sel = new MySqlCommand(sqlSel, DBC.GetConn());
            MySqlDataReader selRead = sel.ExecuteReader();
            while (selRead.Read())
            {
                Comp newComp = new Comp(selRead.GetInt32(4), selRead.GetString(0), selRead.GetString(1), selRead.GetString(2), selRead.GetString(3));
                mylist.Add(newComp);
            }
            DBC.CloseConn();
            foreach (var item in mylist)
            {
                listBox1.Items.Add(item.PrintInfo());
            }
        }

        private void удалениеЗаписейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.Visible = !button4.Visible;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {

                int selectedID = mylist[listBox1.SelectedIndex].getId();
                String del = "DELETE FROM computers WHERE id=@id";
                MySqlCommand delComm = new MySqlCommand(del, DBC.GetConn());
                delComm.Parameters.AddWithValue("@id", selectedID);
                delComm.ExecuteNonQuery();
                GetDB();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //450-660
            Form1 form = this;
            windowOpen = !windowOpen;
            if (windowOpen)
            {
                form.Height = 660;
            } else
            {
                form.Height = 400;
            }
        }
    }
}