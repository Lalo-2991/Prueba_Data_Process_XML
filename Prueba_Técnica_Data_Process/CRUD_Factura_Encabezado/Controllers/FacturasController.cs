using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Factura_Encabezado.Models.Negocio;
using System.Runtime.CompilerServices;
using CRUD_Factura_Encabezado.Models.Entidades;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace CRUD_Factura_Encabezado.Controllers
{
    public class FacturasController : Controller
    {
        private readonly Metodos_EfectoComprobante_API _metodosEfectoComprobante_API;
        private readonly Metodos_Facturas_API _metodosFacturas_API;
        private readonly Metodos_FormaPago_API _metodosFormaPago_API;
        private readonly Metodos_MetodoPago_API _metodosMetodoPago_API;
        private readonly Metodos_Moneda_API _metodosMoneda_API;

        public FacturasController()
        {
            _metodosEfectoComprobante_API = new Metodos_EfectoComprobante_API();
            _metodosFacturas_API = new Metodos_Facturas_API();
            _metodosFormaPago_API = new Metodos_FormaPago_API();
            _metodosMetodoPago_API = new Metodos_MetodoPago_API();
            _metodosMoneda_API = new Metodos_Moneda_API();
        }

        // GET: Facturas
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            List<Encabezado> lstEncabezados = await _metodosFacturas_API.Consultar();
            return View(lstEncabezados);
        }

        // GET: Facturas/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            Encabezado oEncabezado = await _metodosFacturas_API.Consultar(id);
            oEncabezado.IdEfectoComprobanteNavigation = await _metodosEfectoComprobante_API.Consultar(oEncabezado.IdEfectoComprobante);
            oEncabezado.IdFormaPagoNavigation = await _metodosFormaPago_API.Consultar(oEncabezado.IdFormaPago);
            oEncabezado.IdMetodoPagoNavigation = await _metodosMetodoPago_API.Consultar(oEncabezado.IdMetodoPago);
            oEncabezado.IdMonedaNavigation = await _metodosMoneda_API.Consultar(oEncabezado.IdMoneda);
            if (oEncabezado == null)
            {
                return NotFound();
            }

            return View(oEncabezado);
        }

        // GET: Facturas/Agregar
        public async Task<IActionResult> Agregar()
        {
            Encabezado oEncabezado = new Encabezado();
            //ViewBag.lstEfectoComprobante = await _metodosEfectoComprobante_API.Consultar();
            //ViewBag.lstFormaPago = await _metodosFormaPago_API.Consultar();
            //ViewBag.lstMetodoPago = await _metodosMetodoPago_API.Consultar();
            //ViewBag.lstMoneda = await _metodosMoneda_API.Consultar();
            ViewBag.lstEfectoComprobante = (await _metodosEfectoComprobante_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdEfectoComprobante.ToString(),
                Text = a.NombreEfectoComprobante
            });

            ViewBag.lstFormaPago = (await _metodosFormaPago_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdFormaPago.ToString(),
                Text = a.NombreFormaPago
            }).ToList();

            ViewBag.lstMetodoPago = (await _metodosMetodoPago_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdMetodoPago.ToString(),
                Text = a.NombreMetodoPago
            }).ToList();

            ViewBag.lstMoneda = (await _metodosMoneda_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdMoneda.ToString(),
                Text = a.NombreMoneda
            }).ToList();

            return View(oEncabezado);
        }

        // POST: Facturas/Agregar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar([Bind("Id,Factura,Emisor,FolioFiscal,FechaEmision,FechaCertificacion,LugarExpedicion,IdMetodoPago,IdFormaPago,IdMoneda,IdEfectoComprobante")] Encabezado oEncabezado)
        {
            if (ModelState.IsValid)
            {
                await _metodosFacturas_API.Agregar(oEncabezado);
                return RedirectToAction(nameof(Index));
            }


            //ViewBag.lstEfectoComprobante = await _metodosEfectoComprobante_API.Consultar();
            //ViewBag.lstFormaPago = await _metodosFormaPago_API.Consultar();
            //ViewBag.lstMetodoPago = await _metodosMetodoPago_API.Consultar();
            //ViewBag.lstMoneda = await _metodosMoneda_API.Consultar();

            ViewBag.lstEfectoComprobante = (await _metodosEfectoComprobante_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdEfectoComprobante.ToString(),
                Text = a.NombreEfectoComprobante
            });

            ViewBag.lstFormaPago = (await _metodosFormaPago_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdFormaPago.ToString(),
                Text = a.NombreFormaPago
            }).ToList();

            ViewBag.lstMetodoPago = (await _metodosMetodoPago_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdMetodoPago.ToString(),
                Text = a.NombreMetodoPago
            }).ToList();

            ViewBag.lstMoneda = (await _metodosMoneda_API.Consultar()).Select(a => new SelectListItem()
            {
                Value = a.IdMoneda.ToString(),
                Text = a.NombreMoneda
            }).ToList();

            return View(oEncabezado);
        }

        // GET: Facturas/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            Encabezado oEncabezado = await _metodosFacturas_API.Consultar(id);
            ViewBag.lstEfectoComprobante = await _metodosEfectoComprobante_API.Consultar();
            ViewBag.lstFormaPago = await _metodosFormaPago_API.Consultar();
            ViewBag.lstMetodoPago = await _metodosMetodoPago_API.Consultar();
            ViewBag.lstMoneda = await _metodosMoneda_API.Consultar();
            if (oEncabezado == null)
            {
                return NotFound();
            }
            return View(oEncabezado);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Factura,Emisor,FolioFiscal,FechaEmision,FechaCertificacion,LugarExpedicion,IdMetodoPago,IdFormaPago,IdMoneda,IdEfectoComprobante")] Encabezado oEncabezado)
        {
            if (id != oEncabezado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _metodosFacturas_API.Actualizar(id, oEncabezado);
                }
                catch
                {
                    //throw new Exception("Error al Agregar Factura");
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.lstEfectoComprobante = await _metodosEfectoComprobante_API.Consultar();
            ViewBag.lstFormaPago = await _metodosFormaPago_API.Consultar();
            ViewBag.lstMetodoPago = await _metodosMetodoPago_API.Consultar();
            ViewBag.lstMoneda = await _metodosMoneda_API.Consultar();
            return View(oEncabezado);
        }

        // GET: Facturas/Eliminar/5
        public async Task<IActionResult> Eliminar(int id)
        {
            Encabezado oEncabezado = await _metodosFacturas_API.Consultar(id);
            oEncabezado.IdEfectoComprobanteNavigation = await _metodosEfectoComprobante_API.Consultar(oEncabezado.IdEfectoComprobante);
            oEncabezado.IdFormaPagoNavigation = await _metodosFormaPago_API.Consultar(oEncabezado.IdFormaPago);
            oEncabezado.IdMetodoPagoNavigation = await _metodosMetodoPago_API.Consultar(oEncabezado.IdMetodoPago);
            oEncabezado.IdMonedaNavigation = await _metodosMoneda_API.Consultar(oEncabezado.IdMoneda);
            if (oEncabezado == null)
            {
                return NotFound();
            }
            return View(oEncabezado);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("ConfirmacionEliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int id)
        {
            try
            {
                await _metodosFacturas_API.Eliminar(id);
            }
            catch 
            {
                throw new Exception("Error en eliminación");
            }
            return RedirectToAction(nameof(Index));
        }


        [ActionName("Exportar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exportar(int id)
        {
            try
            {
                Encabezado oFactura = await _metodosFacturas_API.Consultar(id);
                oFactura.IdEfectoComprobanteNavigation = await _metodosEfectoComprobante_API.Consultar(oFactura.IdEfectoComprobante);
                oFactura.IdFormaPagoNavigation = await _metodosFormaPago_API.Consultar(oFactura.IdFormaPago);
                oFactura.IdMetodoPagoNavigation = await _metodosMetodoPago_API.Consultar(oFactura.IdMetodoPago);
                oFactura.IdMonedaNavigation = await _metodosMoneda_API.Consultar(oFactura.IdMoneda);
                _metodosFacturas_API.ExportarAXML(oFactura);
            }
            catch
            {
                throw new Exception("Error en Serialización");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
