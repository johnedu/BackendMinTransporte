
using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class HistoriaVial : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombrePersona { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Url { get; set; }
        public bool EsActiva { get; set; }
        public int CategoriaId { get; set; }
        public TipoReporte CategoriaHistoria { get; set; }
    }
}
