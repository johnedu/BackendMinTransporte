﻿using Bow.MappingsBase;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Mappings
{
    public class DeslizadorMap : MultiTenantMap<Deslizador>
    {
        public DeslizadorMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();           

            Property(d => d.URLImagen).HasMaxLength(512);
            Property(d => d.URLImagen).IsRequired();         

            //Llaves Foráneas

            //Tabla
            ToTable("deslizador");
        }
    }
}
