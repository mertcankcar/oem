using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KurWıdget
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
        Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10,
        Screen.PrimaryScreen.WorkingArea.Height - this.Height - 10);

            label1.Text = "Altın :" ; 
            label2.Text = "Dolar :" ;
            label3.Text = "Euro  :" ;
            timer1.Start();
            


        }

        private async void  timer1_TickAsync(object sender, EventArgs e)
        {
            await DovizGuncelle();

        }

       
            

            private double sonDolar = 0;
        private double sonEuro = 0;

        private async Task DovizGuncelle()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://api.exchangerate.host/latest?base=TRY&symbols=USD,EUR";
                    string response = await client.GetStringAsync(url);

                    JObject data = JObject.Parse(response);
                    double dolar = (double)data["rates"]["USD"];
                    double euro = (double)data["rates"]["EUR"];

                    // Artış / azalış rengi
                    label2.ForeColor = dolar >= sonDolar ? Color.Green : Color.Red;
                    label3.ForeColor = euro >= sonEuro ? Color.Green : Color.Red;

                    // Label güncelle
                    label2.Text = "Dolar : " + dolar.ToString("0.##");
                    label3.Text = "Euro : " + euro.ToString("0.##");

                    // Son değerleri kaydet
                    sonDolar = dolar;
                    sonEuro = euro;
                }
            }
            catch
            {
                label2.Text = "Dolar : Bağlantı yok";
                label3.Text = "Euro : Bağlantı yok";
                label2.ForeColor = label3.ForeColor = Color.White;
            }
        }

    }



}


