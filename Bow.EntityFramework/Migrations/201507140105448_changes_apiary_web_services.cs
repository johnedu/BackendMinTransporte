namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class changes_apiary_web_services : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("MinTransporte.paso_historia_vial", "HistoriaVialId", "MinTransporte.historia_vial");
            DropIndex("MinTransporte.paso_historia_vial", new[] { "HistoriaVialId" });
            AddColumn("MinTransporte.pregunta_frecuente", "FechaPublicacion", c => c.DateTime(nullable: false));
            AddColumn("MinTransporte.pregunta_frecuente", "UrlImagen", c => c.String(maxLength: 1024));
            AddColumn("MinTransporte.tipo_reporte", "TipoCategoria", c => c.String(nullable: false, maxLength: 512));
            AddColumn("MinTransporte.tipo_reporte", "UrlImagen", c => c.String(maxLength: 2048));
            AddColumn("MinTransporte.reporte_incidentes", "Distancia", c => c.String(maxLength: 100));
            AddColumn("MinTransporte.noticias", "Url", c => c.String(nullable: false, maxLength: 512));
            AddColumn("MinTransporte.historia_vial", "FechaPublicacion", c => c.DateTime(nullable: false));
            AddColumn("MinTransporte.historia_vial", "Url", c => c.String(maxLength: 2048));
            AddColumn("MinTransporte.historia_vial", "CategoriaId", c => c.Int(nullable: false));
            AlterColumn("MinTransporte.historia_vial", "NombrePersona", c => c.String(maxLength: 512));
            CreateIndex("MinTransporte.historia_vial", "CategoriaId");
            AddForeignKey("MinTransporte.historia_vial", "CategoriaId", "MinTransporte.tipo_reporte", "Id", cascadeDelete: true);
            DropColumn("MinTransporte.historia_vial", "EdadPersona");
            DropTable("MinTransporte.paso_historia_vial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PasoHistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "MinTransporte.paso_historia_vial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HistoriaVialId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        Descripcion = c.String(nullable: false, maxLength: 2048),
                        URLImagen = c.String(maxLength: 512),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PasoHistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("MinTransporte.historia_vial", "EdadPersona", c => c.Int(nullable: false));
            DropForeignKey("MinTransporte.historia_vial", "CategoriaId", "MinTransporte.tipo_reporte");
            DropIndex("MinTransporte.historia_vial", new[] { "CategoriaId" });
            AlterColumn("MinTransporte.historia_vial", "NombrePersona", c => c.String(nullable: false, maxLength: 512));
            DropColumn("MinTransporte.historia_vial", "CategoriaId");
            DropColumn("MinTransporte.historia_vial", "Url");
            DropColumn("MinTransporte.historia_vial", "FechaPublicacion");
            DropColumn("MinTransporte.noticias", "Url");
            DropColumn("MinTransporte.reporte_incidentes", "Distancia");
            DropColumn("MinTransporte.tipo_reporte", "UrlImagen");
            DropColumn("MinTransporte.tipo_reporte", "TipoCategoria");
            DropColumn("MinTransporte.pregunta_frecuente", "UrlImagen");
            DropColumn("MinTransporte.pregunta_frecuente", "FechaPublicacion");
            CreateIndex("MinTransporte.paso_historia_vial", "HistoriaVialId");
            AddForeignKey("MinTransporte.paso_historia_vial", "HistoriaVialId", "MinTransporte.historia_vial", "Id", cascadeDelete: true);
        }
    }
}
