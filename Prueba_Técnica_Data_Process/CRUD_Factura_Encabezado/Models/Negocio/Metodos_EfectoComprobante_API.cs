using CRUD_Factura_Encabezado.Models.Entidades;
using Newtonsoft.Json;

namespace CRUD_Factura_Encabezado.Models.Negocio
{
    public class Metodos_EfectoComprobante_API
    {
        private string _webAPIEfectoComprobante;
        public Metodos_EfectoComprobante_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _webAPIEfectoComprobante = builder.GetSection("WebAPIEfectoComprobante").Value;
        }
        public async Task<List<EfectoComprobante>> Consultar()
        {
            List<EfectoComprobante> lstEfectoComprobante = new List<EfectoComprobante>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_webAPIEfectoComprobante);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        lstEfectoComprobante = JsonConvert.DeserializeObject<List<EfectoComprobante>>(json);
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
            return lstEfectoComprobante;
        }
        public async Task<EfectoComprobante> Consultar(int idEfectoComprobante)
        {
            EfectoComprobante oEfectoComprobante = new EfectoComprobante();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync($"{_webAPIEfectoComprobante}/{idEfectoComprobante}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        oEfectoComprobante = JsonConvert.DeserializeObject<EfectoComprobante>(json);
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
            return oEfectoComprobante;
        }
    }
}
