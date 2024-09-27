using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;

namespace BuscaCepApp
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string cep = mskCEP.Text;
            if (string.IsNullOrEmpty(cep))
            {
                MessageBox.Show("Por favor, insira um CEP válido.");
                return;
            }

            string url = $"https://viacep.com.br/ws/{cep}/json/";
            try
            {
                var response = await client.GetStringAsync(url);
                var endereco = JsonConvert.DeserializeObject<Endereco>(response);

                lblLogradouro.Text = $"Logradouro: {endereco.Logradouro}";
                lblBairro.Text = $"Bairro: {endereco.Bairro}";
                lblLocalidade.Text = $"Localidade: {endereco.Localidade}";
                lblUf.Text = $"UF: {endereco.Uf}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o CEP: {ex.Message}");
            }
        }
    }

    public class Endereco
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }
}
