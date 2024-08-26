using CRUD_Factura_Encabezado.Models.Entidades;
using Newtonsoft.Json;

namespace CRUD_Factura_Encabezado.Models.Negocio
{
    public class Metodos_Moneda_API
    {
        private string _webAPIMoneda;
        public Metodos_Moneda_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _webAPIMoneda = builder.GetSection("WebAPIMoneda").Value;
        }
        public async Task<List<Moneda>> Consultar()
        {
            List<Moneda> lstMoneda = new List<Moneda>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_webAPIMoneda);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        lstMoneda = JsonConvert.DeserializeObject<List<Moneda>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web API. Error, al API responde. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstMoneda;
        }
        public async Task<Moneda> Consultar(int idMoneda)
        {
            Moneda oMoneda = new Moneda();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync($"{_webAPIMoneda}/{idMoneda}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        oMoneda = JsonConvert.DeserializeObject<Moneda>(json);
                    }
                    else
                    {
                        throw new Exception($"Web API. Error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return oMoneda;
        }
    }
}
