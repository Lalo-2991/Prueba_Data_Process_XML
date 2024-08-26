using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using NuGet.Configuration;
using CRUD_Factura_Encabezado.Models.Entidades;
using System.Xml.Serialization;
using System.Xml;

namespace CRUD_Factura_Encabezado.Models.Negocio
{
    public class Metodos_Facturas_API
    {
        private string _webAPIFacturas;
        public Metodos_Facturas_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _webAPIFacturas = builder.GetSection("WebAPIFacturas").Value;
        }
        public async Task<List<Encabezado>> Consultar()
        {
            List<Encabezado> lstEncabezados = new List<Encabezado>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_webAPIFacturas);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        lstEncabezados = JsonConvert.DeserializeObject<List<Encabezado>>(json);
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
            return lstEncabezados;
        }
        public async Task<Encabezado> Consultar(int id)
        {
            Encabezado oEncabezado = new Encabezado();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync($"{_webAPIFacturas}/{id}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        oEncabezado = JsonConvert.DeserializeObject<Encabezado>(json);
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
            return oEncabezado;
        }
        public async Task Eliminar(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.DeleteAsync($"{_webAPIFacturas}/{id}");
                    if (!responseTask.IsSuccessStatusCode)
                    {
                        throw new Exception($"Web API. Error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Agregar(Encabezado oEncabezado)
        {
            Encabezado respuesta = new Encabezado();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent EncabezadoString = new StringContent(JsonConvert.SerializeObject(oEncabezado), Encoding.UTF8);
                    EncabezadoString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseTask = await client.PostAsync(_webAPIFacturas, EncabezadoString);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string json = await responseTask.Content.ReadAsStringAsync();
                        respuesta = JsonConvert.DeserializeObject<Encabezado>(json);
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
        }
        public async Task Actualizar(int id, Encabezado oEncabezado)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent EncabezadoString = new StringContent(JsonConvert.SerializeObject(oEncabezado), Encoding.UTF8);
                    EncabezadoString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseTask = await client.PutAsync($"{_webAPIFacturas}/{id}", EncabezadoString);
                    if (!responseTask.IsSuccessStatusCode)
                    {
                        throw new Exception($"Web API. Error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void ExportarAXML(Encabezado oEncabezado)
        {
            XmlDocument docXML = new XmlDocument();
            XmlNode rootNode = docXML.CreateElement("cfdi:Comprobante");
            docXML.AppendChild(rootNode);


            XmlAttribute factura = docXML.CreateAttribute("Factura");
            factura.Value = oEncabezado.Factura;
            rootNode.Attributes.Append(factura);

            XmlAttribute emisor = docXML.CreateAttribute("Emisor");
            emisor.Value = oEncabezado.Emisor;
            rootNode.Attributes.Append(emisor);

            XmlAttribute folioFiscal = docXML.CreateAttribute("FolioFiscal");
            folioFiscal.Value = oEncabezado.FolioFiscal;
            rootNode.Attributes.Append(folioFiscal);

            XmlAttribute fechaEmision = docXML.CreateAttribute("FechaEmision");
            fechaEmision.Value = oEncabezado.FechaEmision.ToString();
            rootNode.Attributes.Append(fechaEmision);

            XmlAttribute fechaCertificacion = docXML.CreateAttribute("FechaCertificacion");
            fechaCertificacion.Value = oEncabezado.FechaCertificacion.ToString();
            rootNode.Attributes.Append(fechaCertificacion);

            XmlAttribute lugarExpedicion = docXML.CreateAttribute("LugarExpedicion");
            lugarExpedicion.Value = oEncabezado.LugarExpedicion.ToString();
            rootNode.Attributes.Append(lugarExpedicion);

            XmlAttribute metodoPago = docXML.CreateAttribute("MetodoPago");
            metodoPago.Value = oEncabezado.IdMetodoPagoNavigation.NombreMetodoPago;
            rootNode.Attributes.Append(metodoPago);

            XmlAttribute formaPago = docXML.CreateAttribute("FormaPago");
            formaPago.Value = oEncabezado.IdFormaPagoNavigation.NombreFormaPago;
            rootNode.Attributes.Append(formaPago);

            XmlAttribute moneda = docXML.CreateAttribute("Moneda");
            moneda.Value = oEncabezado.IdMonedaNavigation.NombreMoneda;
            rootNode.Attributes.Append(moneda);

            XmlAttribute efectoComprobante = docXML.CreateAttribute("EfectoComprobante");
            efectoComprobante.Value = oEncabezado.IdEfectoComprobanteNavigation.NombreEfectoComprobante;
            rootNode.Attributes.Append(efectoComprobante);

            docXML.Save($"{oEncabezado.Emisor}.xml");

            ////Crear el Serealizador
            //XmlSerializer serializer = new XmlSerializer(typeof(Encabezado), new XmlRootAttribute("Factura"));
            ////Crear Objeto a Serealizar

            ///* Crear un StreamWriter para escribir*/
            //StreamWriter archivo = new StreamWriter(emisor, false, Encoding.UTF8);
            //// Serializar usando el StreamWriter.
            //serializer.Serialize(archivo, oEncabezado);
            //archivo.Close();
        }
    }
}
