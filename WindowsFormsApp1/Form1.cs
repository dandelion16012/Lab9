using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using WindowsFormsApp1.model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1_Load();
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

        private async void button1_Click(object sender, EventArgs e)
        {
            string city = comboBox1.Text;

            await Task.Run(async () =>
            {
                await Task.Delay(1000);
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
                   /* Invoke((MethodInvoker)delegate
                    {*/
                         textBox1.Text = (string)city;
                        textBox2.Text = (string)weatherResponse.Weather[0].Description;
                        textBox3.Text = $"{weatherResponse.Main.Temp}";
                    /*});*/
                }
            });
        }

        
    }
    
}
