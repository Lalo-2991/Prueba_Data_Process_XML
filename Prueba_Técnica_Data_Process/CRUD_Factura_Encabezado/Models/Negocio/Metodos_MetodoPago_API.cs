using CRUD_Factura_Encabezado.Models.Entidades;
using Newtonsoft.Json;

namespace CRUD_Factura_Encabezado.Models.Negocio
{
    public class Metodos_MetodoPago_API
    {
        private string _webAPIMetodoPago;
        public Metodos_MetodoPago_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _webAPIMetodoPago = builder.GetSection("WebAPIMetodoPago").Value;
        }
        public async Task<List<MetodoPago>> Consultar()
        {
            List<MetodoPago> lstMetodoPago = new List<MetodoPago>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_webAPIMetodoPago);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        lstMetodoPago = JsonConvert.DeserializeObject<List<MetodoPago>>(json);
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
            return lstMetodoPago;
        }

        public async Task<MetodoPago> Consultar(int idMetodoPago)
        {
            MetodoPago oMetodoPago = new MetodoPago();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync($"{_webAPIMetodoPago}/{idMetodoPago}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        oMetodoPago = JsonConvert.DeserializeObject<MetodoPago>(json);
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
            return oMetodoPago;
        }
    }
}
