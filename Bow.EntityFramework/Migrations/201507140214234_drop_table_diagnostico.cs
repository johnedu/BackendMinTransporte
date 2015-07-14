namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class drop_table_diagnostico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("MinTransporte.item_diagnostico", "DiagnosticoVialId", "MinTransporte.diagnostico_vial");
            DropIndex("MinTransporte.item_diagnostico", new[] { "DiagnosticoVialId" });
            DropColumn("MinTransporte.item_diagnostico", "DiagnosticoVialId");
            DropTable("MinTransporte.diagnostico_vial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DiagnosticoVial_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
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
            
            AddColumn("MinTransporte.item_diagnostico", "DiagnosticoVialId", c => c.Int(nullable: false));
            CreateIndex("MinTransporte.item_diagnostico", "DiagnosticoVialId");
            AddForeignKey("MinTransporte.item_diagnostico", "DiagnosticoVialId", "MinTransporte.diagnostico_vial", "Id", cascadeDelete: true);
        }
    }
}
