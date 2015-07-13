namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_deslizador : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MinTransporte.deslizador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        URLImagen = c.String(nullable: false, maxLength: 512),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Deslizador_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("MinTransporte.paso_historia_vial", "Descripcion", c => c.String(nullable: false, maxLength: 2048));
            AlterColumn("MinTransporte.paso_historia_vial", "URLImagen", c => c.String(maxLength: 512));
            AlterColumn("MinTransporte.item_diagnostico", "Observaciones", c => c.String(nullable: false, maxLength: 2048));
        }
        
        public override void Down()
        {
            AlterColumn("MinTransporte.item_diagnostico", "Observaciones", c => c.String(nullable: false, maxLength: 2058));
            AlterColumn("MinTransporte.paso_historia_vial", "URLImagen", c => c.String());
            AlterColumn("MinTransporte.paso_historia_vial", "Descripcion", c => c.String(nullable: false, maxLength: 2058));
            DropTable("MinTransporte.deslizador",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Deslizador_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
