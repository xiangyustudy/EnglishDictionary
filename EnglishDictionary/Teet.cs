using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;


namespace EnglishDictionary
{
    public class Teet
    {
        static void Main()
        {
        
            SQLiteConnection sqlite_db_conn = new SQLiteConnection(@"Data Source=english.db");
            sqlite_db_conn.Open();

            String sql_str = "select * from dictionary";
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql_str, sqlite_db_conn);
            SQLiteDataReader sqlite_read = sqlite_cmd.ExecuteReader();

            List<String> word_list=new List<String>();
            while(sqlite_read.Read())
            {
                String search_word=sqlite_read["word"]+"";
                Console.WriteLine(search_word + " summon " + GetLSD(search_word, "summon"));

                if (Utils.GetLSD(search_word,"summon") <= 2)
                {
                    String explanation=sqlite_read["explanation"]+"";
                    word_list.Add(search_word+"  "+explanation);
                }           
            }

            Console.WriteLine();
            if(word_list.Count==0)
            {
                //exist.Text = "不存在";
                Console.WriteLine("不存在");

            }
            else
            {
                 //exist.Text = "存在";
                Console.WriteLine("存在");
                foreach (String str in word_list)
                    Console.WriteLine(str);
            }


         

            sqlite_db_conn.Close();


            Console.ReadKey();
        }

        static int GetLSD(String str1, String str2)
        {
            int column = str1.Length;
            int row = str2.Length;

            int[,] matrix = new int[row + 1, column + 1];
            matrix[0,0]=0;
      
           for (int i = 1; i < column + 1; i++)
               matrix[0, i] = i;
            
           for (int j = 1; j < row + 1; j++)
               matrix[j, 0] = j;

            for (int i = 1; i <  row + 1; i++)
            {
                for (int j = 1; j < column + 1; j++)
                {
                    int cost=str1[j-1] == str2[i-1]?0:1;

                    int delection=matrix[i- 1, j]+1;
                    int insertion=matrix[i,j-1]+1;
                    int susbstitution=matrix[i-1,j-1]+cost;
                   
                    matrix[i,j] = Math.Min(Math.Min(delection,insertion),susbstitution);
                            
                }             
            }

            //for (int i = 0; i < row + 1; i++)
            //{
            //    for (int j = 0; j < column + 1; j++)
            //    {
            //        Console.Write(matrix[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}


            return matrix[row,column];
        }



    }
}
