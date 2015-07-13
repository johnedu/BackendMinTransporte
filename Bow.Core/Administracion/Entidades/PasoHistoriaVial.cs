using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class PasoHistoriaVial : EntidadMultiTenant
    {
        public int HistoriaVialId { get; set; }
        public HistoriaVial HistoriaVialPaso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string URLImagen { get; set; }
    }
}
