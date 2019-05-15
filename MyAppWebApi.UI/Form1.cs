using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAppWebApi.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", result.CreateAuthorizationHeader());

            //HttpResponseMessage r = await client.GetAsync(resourceUrl);

         // HttpClient c =  GetClient("test", "123");


        }

        public static HttpClient GetClient(string username, string password)
        {
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            var client = new HttpClient()
            {
                BaseAddress=new Uri("http://localhost:56126/token"),
                DefaultRequestHeaders = { Authorization = authValue }
                //Set some other client defaults like timeout / BaseAddress
                
            };
           
            return client;
        }

        // Auth with bearer token
        public static HttpClient GetClient(string token)
        {
            var authValue = new AuthenticationHeaderValue("Bearer", token);

            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
                //Set some other client defaults like timeout / BaseAddress
            };
            return client;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                Dictionary<string, string> tokenDetails = null;
                var messageDetails = new Message {};
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:56126/");
                var login = new Dictionary<string, string>
       {
           {"grant_type", "password"},
           {"username", "test"},
           {"password", "123"},
       };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    if (tokenDetails != null && tokenDetails.Any())
                    {
                        var tokenNo = tokenDetails.FirstOrDefault().Value;
                        Console.Write(tokenNo);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenNo);
                    var donendeger =    client.PostAsJsonAsync("api/Test", messageDetails)
                            .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                        var deger= await donendeger.Result.Content.ReadAsAsync<MyData[]>();
                        listBox1.DataSource = deger;
                    }
                }
            }
        }
    }

}

