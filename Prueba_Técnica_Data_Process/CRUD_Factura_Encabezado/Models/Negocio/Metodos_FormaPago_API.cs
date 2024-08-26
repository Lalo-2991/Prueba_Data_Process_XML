using CRUD_Factura_Encabezado.Models.Entidades;
using Newtonsoft.Json;

namespace CRUD_Factura_Encabezado.Models.Negocio
{
    public class Metodos_FormaPago_API
    {
        private string _webAPIFormaPago;
        public Metodos_FormaPago_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _webAPIFormaPago = builder.GetSection("WebAPIFormaPago").Value;
        }

        public async Task<List<FormaPago>> Consultar()
        {
            List<FormaPago> lstFormaPago = new List<FormaPago>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_webAPIFormaPago);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        lstFormaPago = JsonConvert.DeserializeObject<List<FormaPago>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web API. Error´. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstFormaPago;
        }

        public async Task<FormaPago> Consultar(int idFormaPago)
        {
            FormaPago oFormaPago = new FormaPago();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync($"{_webAPIFormaPago}/{idFormaPago}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        oFormaPago = JsonConvert.DeserializeObject<FormaPago>(json);
                    }
                    else
                    {
                        throw new Exception($"Web API. Error´. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return oFormaPago;
        }
    }
}
