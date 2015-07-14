using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EnglishDictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_db_conn = new SQLiteConnection(@"Data Source=english.db");
            sqlite_db_conn.Open();

            String sql_str = "select * from dictionary";
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql_str, sqlite_db_conn);
            SQLiteDataReader sqlite_read = sqlite_cmd.ExecuteReader();

            List<String> word_list = new List<String>();
            while (sqlite_read.Read())
            {
                String search_word = sqlite_read["word"] + "";
                //Console.WriteLine(search_word + " summon " + GetLSD(search_word, "summon"));

                if (Utils.GetLSD(search_word,input_word.Text.Trim()) <= 2)
                {
                    String explanation = sqlite_read["explanation"] + "";
                    word_list.Add(search_word + "  " + explanation);
                }
            }

            //Console.WriteLine();
            if (word_list.Count == 0)
            {
                WordList.Items.Clear();
                exist.Text = "不存在";

            }
            else
            {
                exist.Text = "存在";

                WordList.Items.Clear();
                WordList.Font =new Font(this.Font.FontFamily,16);
                foreach (String str in word_list)
                    WordList.Items.Add(str);
            }


            sqlite_db_conn.Close();
         


        }

        private void button2_Click(object sender, EventArgs e)
        {
            insert_word.ReadOnly = false;
            insert_explanation.ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            insert_word.ReadOnly = true;
            insert_explanation.ReadOnly = true;

            String word = insert_word.Text.Trim();
            String explanation = insert_explanation.Text.Trim();

            SQLiteConnection sqlite_db_conn = new SQLiteConnection(@"Data Source=english.db");
            sqlite_db_conn.Open();

            SQLiteCommand sqlite_cmd = new SQLiteCommand("select * from dictionary where word='"+word+"'", sqlite_db_conn);
            SQLiteDataReader sqlite_read = sqlite_cmd.ExecuteReader();

            if (sqlite_read.Read())
            {
                MessageBox.Show("单词已经存在");
            }
            else
            {
                SQLiteCommand insertdata = new SQLiteCommand("insert into dictionary values('"+word+"','"+explanation+"')", sqlite_db_conn);
               if(insertdata.ExecuteNonQuery()>0)
                    MessageBox.Show("添加成功!");
               else
                   MessageBox.Show("添加失败!");
            }
            sqlite_db_conn.Close();
            insert_word.Text = "";
            insert_explanation.Text = "";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar==13) //"Ctrl + S"促发
            {
                SQLiteConnection sqlite_db_conn = new SQLiteConnection(@"Data Source=english.db");
                sqlite_db_conn.Open();

                String sql_str = "select * from dictionary";
                SQLiteCommand sqlite_cmd = new SQLiteCommand(sql_str, sqlite_db_conn);
                SQLiteDataReader sqlite_read = sqlite_cmd.ExecuteReader();

                List<String> word_list = new List<String>();
                while (sqlite_read.Read())
                {
                    String search_word = sqlite_read["word"] + "";
                    //Console.WriteLine(search_word + " summon " + GetLSD(search_word, "summon"));

                    if (Utils.GetLSD(search_word, input_word.Text.Trim()) <= 2)
                    {
                        String explanation = sqlite_read["explanation"] + "";
                        word_list.Add(search_word + "  " + explanation);
                    }
                }

                //Console.WriteLine();
                if (word_list.Count == 0)
                {
                    WordList.Items.Clear();
                    exist.Text = "不存在";

                }
                else
                {
                    exist.Text = "存在";
                    WordList.Items.Clear();
                    WordList.Font = new Font(this.Font.FontFamily, 16);
                    foreach (String str in word_list)
                        WordList.Items.Add(str);
                }


                sqlite_db_conn.Close();




            }
        }

        
    }
}
