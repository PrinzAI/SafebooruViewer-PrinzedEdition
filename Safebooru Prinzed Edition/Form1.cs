using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace Safebooru_Prinzed_Edition
{
    public partial class Form1 : Form
    {
        public static string idl;
        public static string limit;
        public static string publicFile;
        public static string dir;
        public Form1()
        {
            InitializeComponent();
        }

        public void random()
        {
            Random random = new Random();
            string sURL = $"https://safebooru.org/index.php?page=dapi&s=post&q=index&tags={ textBox1.Text }";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            var responseXml = new XmlDocument();
            responseXml.Load(objStream);
            limit = responseXml.DocumentElement.Attributes["count"].Value;
            int num = random.Next(Convert.ToInt32(limit));
            idl = Convert.ToString(num);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            random();
            string sURL = $"https://safebooru.org/index.php?page=dapi&s=post&q=index&limit=1&pid={idl}&tags={textBox1.Text}";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            var responseXml = new XmlDocument();
            responseXml.Load(objStream);
            if (Convert.ToInt32(limit) != 0)
            {
                string File_URL = responseXml.DocumentElement.FirstChild.Attributes["file_url"].Value;
                publicFile = File_URL;
                pictureBox2.Load(File_URL);
            }
            else
            {
                MessageBox.Show("Invalid Tag, please introduce a valid one");
                textBox1.Text = "";
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sURL = $"https://safebooru.org/index.php?page=dapi&s=post&q=index&tags={ textBox1.Text }";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            var responseXml = new XmlDocument();
            responseXml.Load(objStream);
            limit = responseXml.DocumentElement.Attributes["count"].Value;
            if (Convert.ToInt32(limit) != 0)
            {
                int num = 0;
                num = num + 1;
                if (textBox1.Text == "")
                {
                    
                    var path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                    pictureBox2.Image.Save($@"{path}\\Images\\Random{num} {idl}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    var path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                    pictureBox2.Image.Save($@"{path}\\Images\\{textBox1.Text} {idl}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            else
            {
                MessageBox.Show("Invalid Tag, please introduce a valid one");
                textBox1.Text = "";
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("For the moment, there are no Frequently asked questions");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application uses the Safebooru API, therefore, you need to introduze a valid tag on the text box below the 'Search' Button, after you click search with a valid tag, it will search the website for a random image of the specific tag you introduced, Have fun :D\n\n(Btw, no tags in the text box = It'll search the whole website for a random imagine altogether)\n\nDiscord Tag: Prinz#5972");
        }
    }
}
