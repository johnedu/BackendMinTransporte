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
    public class DiagnosticoVialMap : MultiTenantMap<DiagnosticoVial>
    {
        public DiagnosticoVialMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            Property(d => d.EsActivo).IsRequired();

            //Llaves Foráneas
            HasMany<ItemDiagnostico>(diagnosticoVial => diagnosticoVial.ItemsDiagnosticoVial)
              .WithRequired(itemDiagnostico => itemDiagnostico.DiagnosticoVialItem)
              .HasForeignKey(itemDiagnostico => itemDiagnostico.DiagnosticoVialId)
              .WillCascadeOnDelete(true);

            //Tabla
            ToTable("diagnostico_vial");
        }
    }
}
