using Bow.MappingsBase;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Mappings
{
    public class NoticiasMap : MultiTenantMap<Noticias>
    {
        public NoticiasMap()
        {
            //Atributos
            Property(d => d.Titulo).HasMaxLength(512);
            Property(d => d.Titulo).IsRequired();

            Property(d => d.Descripcion).HasMaxLength(2048);
            Property(d => d.Descripcion).IsRequired();

            Property(d => d.URLImagen).HasMaxLength(512);
            Property(d => d.URLImagen).IsRequired();

            
            Property(d => d.Fecha).IsRequired();
            
            Property(d => d.EsActiva).IsRequired();

            //Llaves Foráneas
           

            //Tabla
            ToTable("noticias");
        }
    }
}
