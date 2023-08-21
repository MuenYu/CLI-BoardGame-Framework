namespace Game
{
    class IOUtil
    {
        public static void Write2File(string path,string content)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            fs.Close();
        }

        public static string ReadFromFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return content;
        }
    }
}
