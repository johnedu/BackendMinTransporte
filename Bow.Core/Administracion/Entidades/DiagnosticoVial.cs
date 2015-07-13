using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class DiagnosticoVial : EntidadMultiTenant
    {

        public string Nombre { get; set; }
        public bool EsActivo { get; set; }

        public virtual ICollection<ItemDiagnostico> ItemsDiagnosticoVial { get; set; }

        public DiagnosticoVial()
        {
            ItemsDiagnosticoVial = new List<ItemDiagnostico>();
        }
    }
}
