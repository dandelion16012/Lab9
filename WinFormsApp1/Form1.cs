using System.Globalization;
using System.Net;
using WebApplication1.Models;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1_Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string city = comboBox1.Text;

            Task.Run(() =>
            {
                StreamReader cities = new StreamReader("C:\\Users\\Полина\\Documents\\3_сем\\ая_3сем\\lab9\\WinFormsApp1\\city.txt");
                double lat = 0; double lon = 0;
                string temp = cities.ReadToEnd();
                string[] city_strings = temp.Split('\n');
                foreach (string str in city_strings)
                {
                    string[] done_city_strings = str.Split(',');
                    if (done_city_strings[0] == city)
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        lat = Convert.ToDouble(done_city_strings[1], provider);
                        lon = Convert.ToDouble(done_city_strings[2], provider);
                        break;
                    }
                }

                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=7014cad18f46440ad10609dba5cc3cb4&units=metric";
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                    Invoke((MethodInvoker)delegate
                    {
                        textBox1.Text = (string)city;
                        textBox2.Text = (string)weatherResponse.Weather[0].Description;
                        textBox3.Text = $"{weatherResponse.Main.Temp}";
                    });
                }
            });
        }

        private void Form1_Load()
        {
            StreamReader cities = new StreamReader("C:\\Users\\Полина\\Documents\\3_сем\\ая_3сем\\lab9\\WinFormsApp1\\city.txt");
            string temp = cities.ReadToEnd();
            string[] city_strings = temp.Split('\n');
            foreach (string str in city_strings)
            {
                string[] done_city_strings = str.Split(',');
                comboBox1.Items.Add(done_city_strings[0]);
            }

        }

    }
}