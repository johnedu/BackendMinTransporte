using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class ReporteCalificaciones : EntidadMultiTenant
    {
        public int TipoVehiculoId { get; set; }
        public TipoVehiculo TipoVehiculoReporte { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string Observaciones { get; set; }
        public decimal Calificacion { get; set; }
        public bool EsActiva { get; set; }
    }
}
