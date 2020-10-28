using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ttttt
{
	//test	
	class Program
    {
        static void Main(string[] args)
        {
            read_bytefile();
            // Get_column_name();

        }

        //读取二进制字节流数据文件
        public static void read_bytefile()
        {
            string fileName = @"C:\Users\Administrator\Desktop\012_2.5_182.$69";
            string write_flie = @"C:\Users\Administrator\Desktop\binary_69.txt";
            //写入
            StreamWriter sw = new StreamWriter(write_flie);
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open),encoding: Encoding.GetEncoding(28591)))
            {
                // Encoding.GetEncoding(28591) = ISO-8859-1
                // ReadSingle()	 从当前流中读取 4 字节浮点值，并使流的当前位置提升 4 个字节。
                try
                {
                    float tenmi;
                    while (true)
                    {

                        
                        //Console.WriteLine(tenmi);
                        // Console.ReadKey();
                        // i为列数目
                        for (int i = 1; i<6; i++)
                        {
                            tenmi = reader.ReadSingle();
                            sw.Write(tenmi + "\t");
                        }
                        tenmi = reader.ReadSingle();
                        sw.Write(tenmi+"\r\n");

                    }
                }
                
                catch(EndOfStreamException e)
                {
                    Console.Write("读取完毕");
                    Console.ReadKey();
                }
                sw.Close();
                
            }


        }

        //读取文本文件，找寻特定字符串，以方便定位
        /*
        public static void Read_find()
        {
            string text = "fs";
            string filePath = @"C:\Users\Administrator\Desktop\fs\1.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("没找到" + filePath);
            }

            StreamReader sr = new StreamReader(filePath, Encoding.Default);
            string line;
            int total = 0;
            while ((line = sr.ReadLine()) != null)
            {
                Regex regex = new Regex(text);
                var matches = regex.Matches(line);
                total += matches.Count;
            }
            string fileName = Path.GetFileName(filePath);
            sr.Close();
            string reslut = string.Format("在文件:{0} -----找到 {1} 个:\"{2}\"", fileName, total, text);
            Console.WriteLine(reslut);
            Console.ReadKey();
        }
        */

        
        //读取%文件，提取二进制数据的列名称
        public static void Get_column_name()
        {
            string table_name = "GENLAB";
            string[] column_name;
            string end_token = "VARUNIT";
            Regex regex = new Regex(table_name);
            Regex regex_1 = new Regex(end_token);

            string filePath_fron = @"C: \Users\Administrator\Desktop\012_2.5_182\012_2.5_182.%";
            string filePath;
            StreamWriter sw = new StreamWriter(@"C:\Users\Administrator\Desktop\column_name.txt", true);
            for (int i = 4; i < 70; i++)
            {


                string file_end_number = i.ToString("00");
                // Console.WriteLine(file_end_number);
                // Console.Read();

                filePath = filePath_fron + file_end_number;

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("没有.%" + file_end_number + "文件");
                    // Console.ReadKey();
                    continue;
                }
                StreamReader sr = new StreamReader(filePath, Encoding.Default);
                

                string line;
                sw.Write(".%"+file_end_number+"文件\t");
                while ((line = sr.ReadLine()) != null)
                {

                    if (regex.IsMatch(line))
                    {

                        // 字符串分割
                        string[] words = line.Split('\t');
                        //写入新文件
                        string table_1 = words[1].Replace("'", "");
                        sw.Write(table_1 + "\t");

                        //读取新一行
                        line = sr.ReadLine();

                        string[] cut_line = line.Split('\t');
                        //Regex c_cut_line = new Regex(cut_line[1]);
                        column_name = Regex.Split(cut_line[1], @"' '");

                        foreach (string x in column_name)
                        {
                            string single = x.Replace("'", "");
                            sw.Write(single + "\t");
                        }
                        sw.Write("\r\n\r\n");
                        break;

                    }
                }                             
                sr.Close();
                
                Console.WriteLine("读取.%" + file_end_number + "完毕");
                
            }

            //读取.%101文件、296、297文件，101文件特殊，不以“ 空格”分隔，所以要特殊处理
            filePath = filePath_fron + "101";
            sw.Write(".%101文件\t");
            using (StreamReader sr_1 = new StreamReader(filePath, Encoding.Default))
            {
                string line_1;
                while ((line_1 = sr_1.ReadLine()) != null)
                {

                    if (regex.IsMatch(line_1))
                    {

                        // 字符串分割

                        string[] words = Regex.Split(line_1, @"B  ");
                        //写入新文件
                        string table_1 = words[1].Replace("'", "");
                        sw.Write(table_1 + "\t");

                        //读取新一行
                        line_1 = sr_1.ReadLine();

                        // string[] cut_line = line_1.Split(' ');
                        //Regex c_cut_line = new Regex(cut_line[1]);
                        column_name = Regex.Split(line_1, @"' '");

                        foreach (string x in column_name)
                        {
                            string single = x.Replace("'", "");
                            sw.Write(single + "\t");
                        }
                        sw.Write("\r\n");
                        break;

                    }
                }
            }
            //296文件
            filePath = filePath_fron + "296";
            sw.Write(".%296文件\t");
            using (StreamReader sr_1 = new StreamReader(filePath, Encoding.Default))
            {
                string line_1;
                while ((line_1 = sr_1.ReadLine()) != null)
                {

                    if (regex.IsMatch(line_1))
                    {

                        // 字符串分割

                        string[] words = Regex.Split(line_1, "\t");
                        //写入新文件
                        string table_1 = words[1].Replace("'", "");
                        sw.Write(table_1 + "\t");

                        //读取新一行
                        line_1 = sr_1.ReadLine();

                        string[] cut_line = line_1.Split('\t');
                        //Regex c_cut_line = new Regex(cut_line[1]);
                        column_name = Regex.Split(cut_line[1], @"' '");

                        foreach (string x in column_name)
                        {
                            string single = x.Replace("'", "");
                            sw.Write(single + "\t");
                        }
                        sw.Write("\r\n");
                        break;

                    }
                }
            }
            //297文件
            filePath = filePath_fron + "297";
            sw.Write(".%297文件\t");
            using (StreamReader sr_1 = new StreamReader(filePath, Encoding.Default))
            {
                string line_1;
                while ((line_1 = sr_1.ReadLine()) != null)
                {

                    if (regex.IsMatch(line_1))
                    {

                        // 字符串分割

                        string[] words = Regex.Split(line_1, "\t");
                        //写入新文件
                        string table_1 = words[1].Replace("'", "");
                        sw.Write(table_1 + "\t");

                        //读取新一行
                        line_1 = sr_1.ReadLine();

                        string[] cut_line = line_1.Split('\t');
                        //Regex c_cut_line = new Regex(cut_line[1]);
                        column_name = Regex.Split(cut_line[1], @"' '");

                        foreach (string x in column_name)
                        {
                            string single = x.Replace("'", "");
                            sw.Write(single + "\t");
                        }
                        sw.Write("\r\n");
                        break;

                    }
                }
            }
            sw.Close();
            Console.ReadKey();
            
        }

        /*
        private static void ConnectToData(string connectionString)
        {
            //Create a SqlConnection to the Northwind database.

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                //Create a SqlDataAdapter for the Suppliers table.
                SqlDataAdapter adapter = new SqlDataAdapter();

                // A table mapping names the DataTable.
                adapter.TableMappings.Add("Table", "Suppliers");

                // Open the connection.
                connection.Open();
                Console.WriteLine("The SqlConnection is open.");

                // Create a SqlCommand to retrieve Suppliers data.
                SqlCommand command = new SqlCommand(
                    "SELECT SupplierID, CompanyName FROM dbo.Suppliers;",
                    connection);
                command.CommandType = CommandType.Text;

                // Set the SqlDataAdapter's SelectCommand .
                adapter.SelectCommand = command;

                // Fill the DataSet.
                DataSet dataSet = new DataSet("Suppliers");
                adapter.Fill(dataSet);

                // Create a second Adapter and Command to get
                // the Products table, a child table of Suppliers.
                SqlDataAdapter productsAdapter = new SqlDataAdapter();
                productsAdapter.TableMappings.Add("Table", "Products");

                SqlCommand productsCommand = new SqlCommand(
                    "SELECT ProductID, SupplierID FROM dbo.Products;",
                    connection);
                productsAdapter.SelectCommand = productsCommand;

                // Fill the DataSet.
                productsAdapter.Fill(dataSet);

                // Close the connection.
                connection.Close();
                Console.WriteLine("The SqlConnection is closed.");

                // Create a DataRelation to link the two tables
                // based on the SupplierID.
                DataColumn parentColumn =
                    dataSet.Tables["Suppliers"].Columns["SupplierID"];
                DataColumn childColumn =
                    dataSet.Tables["Products"].Columns["SupplierID"];
                DataRelation relation =
                    new System.Data.DataRelation("SuppliersProducts",
                    parentColumn, childColumn);
                dataSet.Relations.Add(relation);
                Console.WriteLine(
                    "The {0} DataRelation has been created.",
                    relation.RelationName);
            }
        }
        */
        /*
        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=(local);Initial Catalog=Northwind;"
                + "Integrated Security=SSPI";
        }
        */
    }
}

