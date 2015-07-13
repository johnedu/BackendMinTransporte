namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_noticias : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "MinTransporte.tipo_vehiculo", newName: "TipoVehiculoes");
            DropForeignKey("MinTransporte.reporte_calificaciones", "TipoVehiculoId", "MinTransporte.tipo_vehiculo");
            CreateTable(
                "MinTransporte.noticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 512),
                        Descripcion = c.String(nullable: false, maxLength: 2048),
                        URLImagen = c.String(nullable: false, maxLength: 512),
                        Fecha = c.DateTime(nullable: false),
                        EsActiva = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Noticias_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("MinTransporte.TipoVehiculoes", "Nombre", c => c.String());
            AddForeignKey("MinTransporte.reporte_calificaciones", "TipoVehiculoId", "MinTransporte.TipoVehiculoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("MinTransporte.reporte_calificaciones", "TipoVehiculoId", "MinTransporte.TipoVehiculoes");
            AlterColumn("MinTransporte.TipoVehiculoes", "Nombre", c => c.String(nullable: false, maxLength: 512));
            DropTable("MinTransporte.noticias",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Noticias_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            AddForeignKey("MinTransporte.reporte_calificaciones", "TipoVehiculoId", "MinTransporte.tipo_vehiculo", "Id");
            RenameTable(name: "MinTransporte.TipoVehiculoes", newName: "tipo_vehiculo");
        }
    }
}
