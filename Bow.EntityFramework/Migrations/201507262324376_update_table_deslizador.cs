namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_deslizador : DbMigration
    {
        public override void Up()
        {
            AlterColumn("MinTransporte.deslizador", "Nombre", c => c.String(nullable: false, maxLength: 2048));
            AlterColumn("MinTransporte.deslizador", "UrlImagen", c => c.String(nullable: false, maxLength: 2048));
            DropTable("MinTransporte.tipo_vehiculo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoVehiculo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "MinTransporte.tipo_vehiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoVehiculo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("MinTransporte.deslizador", "UrlImagen", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("MinTransporte.deslizador", "Nombre", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
