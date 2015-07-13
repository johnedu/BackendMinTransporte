using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Noticias : EntidadMultiTenant
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string URLImagen { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsActiva { get; set; }

    }
}
