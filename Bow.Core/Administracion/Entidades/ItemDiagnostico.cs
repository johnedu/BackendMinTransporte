using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class ItemDiagnostico : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public bool EsRequerido { get; set; }
        public bool EsActivo { get; set; }
        public int DiagnosticoVialId { get; set; }
        public DiagnosticoVial DiagnosticoVialItem { get; set; }
    }
}
