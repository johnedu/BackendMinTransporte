namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class add_tables_diagnostico_historiaVial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "MinTransporte.TipoVehiculoes", newName: "tipo_vehiculo");
            DropForeignKey("MinTransporte.reporte_incidentes", "TipoReporteId", "MinTransporte.tipo_reporte");
            CreateTable(
                "MinTransporte.paso_historia_vial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HistoriaVialId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        Descripcion = c.String(nullable: false, maxLength: 2058),
                        URLImagen = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PasoHistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.historia_vial", t => t.HistoriaVialId, cascadeDelete: true)
                .Index(t => t.HistoriaVialId);
            
            CreateTable(
                "MinTransporte.historia_vial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        Descripcion = c.String(nullable: false, maxLength: 2048),
                        NombrePersona = c.String(nullable: false, maxLength: 512),
                        EdadPersona = c.Int(nullable: false),
                        EsActiva = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MinTransporte.item_diagnostico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        Observaciones = c.String(nullable: false, maxLength: 2058),
                        EsRequerido = c.Boolean(nullable: false),
                        EsActivo = c.Boolean(nullable: false),
                        DiagnosticoVialId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ItemDiagnostico_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.diagnostico_vial", t => t.DiagnosticoVialId, cascadeDelete: true)
                .Index(t => t.DiagnosticoVialId);
            
            CreateTable(
                "MinTransporte.diagnostico_vial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        EsActivo = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DiagnosticoVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("MinTransporte.tipo_vehiculo", "Nombre", c => c.String(nullable: false, maxLength: 512));
            AddForeignKey("MinTransporte.reporte_incidentes", "TipoReporteId", "MinTransporte.tipo_reporte", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("MinTransporte.reporte_incidentes", "TipoReporteId", "MinTransporte.tipo_reporte");
            DropForeignKey("MinTransporte.item_diagnostico", "DiagnosticoVialId", "MinTransporte.diagnostico_vial");
            DropForeignKey("MinTransporte.paso_historia_vial", "HistoriaVialId", "MinTransporte.historia_vial");
            DropIndex("MinTransporte.item_diagnostico", new[] { "DiagnosticoVialId" });
            DropIndex("MinTransporte.paso_historia_vial", new[] { "HistoriaVialId" });
            AlterColumn("MinTransporte.tipo_vehiculo", "Nombre", c => c.String());
            DropTable("MinTransporte.diagnostico_vial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DiagnosticoVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.item_diagnostico",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ItemDiagnostico_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.historia_vial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.paso_historia_vial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PasoHistoriaVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            AddForeignKey("MinTransporte.reporte_incidentes", "TipoReporteId", "MinTransporte.tipo_reporte", "Id");
            RenameTable(name: "MinTransporte.tipo_vehiculo", newName: "TipoVehiculoes");
        }
    }
}
