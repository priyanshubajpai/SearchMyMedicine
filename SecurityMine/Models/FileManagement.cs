using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SecurityMine.Models
{
    public class FileManagement
    {
        public void WriteDeletedUser(AppUser user)
        {
            FileStream fs = new FileStream(@"C:\Users\Hp\Desktop\BookMyMedicine\DeletedUsers.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
            string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
            writer.Write(time + "  ");
            writer.Write(date + "  ");
            writer.Write("UserName: "+user.UserName+"  ");
            writer.Write("Email: "+user.Email+"  ");
            writer.Write("PhoneNumber: "+user.PhoneNumber);
            writer.WriteLine();

            writer.Close();
            fs.Close();
        }

        public List<string> ReadDeletedUsers()
        {
            List<string> list = new List<string>();
            FileStream fs = new FileStream(@"C:\Users\Hp\Desktop\BookMyMedicine\DeletedUsers.txt", FileMode.Open, FileAccess.Read);

            StreamReader reader = new StreamReader(fs);

            /* text = reader.ReadToEnd();*/  // read entire file in one go

            fs.Seek(0, SeekOrigin.Begin);

            reader.DiscardBufferedData();

            while (reader.Peek() != -1)
            {
                list.Add(reader.ReadLine());

            }

            reader.Close();
            fs.Close();

            return list;
        }

        public void WriteMessages(SendMessageValidation obj)
        {
            FileStream fs = new FileStream($"C:\\Users\\Hp\\Desktop\\BookMyMedicine\\MessageExchange\\{obj.UserName}.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
            string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
            writer.WriteLine(time + " " + " " + date +" Admin: " + obj.Message);

            writer.Close();
            fs.Close();
        }

        public void WriteMessagesAsUser(SendMessageValidation obj)
        {
            FileStream fs = new FileStream($"C:\\Users\\Hp\\Desktop\\BookMyMedicine\\MessageExchange\\{obj.UserName}.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
            string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
            writer.WriteLine(time+" "+" "+date+" "+obj.UserName+": "+obj.Message);

            writer.Close();
            fs.Close();
        }

        public void WriteMessagesAsUserToMasterAdminFile(SendMessageValidation obj)
        {
            FileStream fs = new FileStream("C:\\Users\\Hp\\Desktop\\BookMyMedicine\\MessageExchange\\Admin.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
            string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
            writer.WriteLine(time + " " + " " + date + " " + obj.UserName + ": " + obj.Message);

            writer.Close();
            fs.Close();
        }

        public List<string> ReadMyMessages(ReadMessagesValidation obj)
        {
            List<string> list = new List<string>();
            string path = $"C:\\Users\\Hp\\Desktop\\BookMyMedicine\\MessageExchange\\{obj.UserName}.txt";
            if (File.Exists(path) == false)
            {
                list = null;
                return list;
            }
            else
            {
                FileStream fs = new FileStream($"C:\\Users\\Hp\\Desktop\\BookMyMedicine\\MessageExchange\\{obj.UserName}.txt", FileMode.Open, FileAccess.Read);

                StreamReader reader = new StreamReader(fs);

                /* text = reader.ReadToEnd();*/  // read entire file in one go

                fs.Seek(0, SeekOrigin.Begin);

                reader.DiscardBufferedData();

                while (reader.Peek() != -1)
                {
                    list.Add(reader.ReadLine());

                }

                reader.Close();
                fs.Close();

                return list;
            }
        }

        public void WriteFileSize(long length)
        {
            //FileStream fs = new FileStream("C:\\Users\\Hp\\Desktop\\SecurityMine\\AdminMessageFileSize.txt", FileMode.Append, FileAccess.Write);
            //StreamWriter writer = new StreamWriter(fs);

            //writer.WriteLine(length);

            //writer.Close();
            //fs.Close();

            System.IO.File.WriteAllText(@"C:\Users\Hp\Desktop\BookMyMedicine\AdminMessageFileSize.txt", length.ToString());
        }

        public string ReadLastLineInAdminMessageSizeFile()
        {
            //FileStream fs = new FileStream("C:\\Users\\Hp\\Desktop\\SecurityMine\\AdminMessageFileSize.txt", FileMode.Open, FileAccess.Read);
            //StreamReader reader = new StreamReader(fs);

            string last_line= File.ReadAllLines(@"C:\Users\Hp\Desktop\BookMyMedicine\AdminMessageFileSize.txt").Last();

            return last_line;
        }

        public void WriteFeedBackToFile(FeedBackValidation obj)
        {
            FileStream fs = new FileStream(@"C:\Users\Hp\Desktop\BookMyMedicine\FeedBack.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
            string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
            writer.WriteLine(time + " " + " " + date + " " + obj.Name + " " + obj.Email +" " + obj.FeedBack);

            writer.Close();
            fs.Close();
        }

        public List<string> ReadFeedBackFromFile()
        {
            List<string> list = new List<string>();
            FileStream fs = new FileStream(@"C:\Users\Hp\Desktop\BookMyMedicine\FeedBack.txt", FileMode.Open, FileAccess.Read);

            StreamReader reader = new StreamReader(fs);

            /* text = reader.ReadToEnd();*/  // read entire file in one go

            fs.Seek(0, SeekOrigin.Begin);

            reader.DiscardBufferedData();

            while (reader.Peek() != -1)
            {
                list.Add(reader.ReadLine());

            }

            reader.Close();
            fs.Close();

            return list;
        }
    }

   
}