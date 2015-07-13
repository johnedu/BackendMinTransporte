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
    public class ItemDiagnosticoMap : MultiTenantMap<ItemDiagnostico>
    {
        public ItemDiagnosticoMap()
        {
            //Atributos

            Property(d => d.DiagnosticoVialId).IsRequired();

            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            Property(d => d.Observaciones).HasMaxLength(2048);
            Property(d => d.Observaciones).IsRequired();

            //Tabla
            ToTable("item_diagnostico");
        }
    }
}
