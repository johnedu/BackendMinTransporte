
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
        public int EdadPersona { get; set; }
        public bool EsActiva { get; set; }

        public virtual ICollection<PasoHistoriaVial> PasosHistorialVial { get; set; }

        public HistoriaVial()
        {
            PasosHistorialVial = new List<PasoHistoriaVial>();
        }
    }
}
