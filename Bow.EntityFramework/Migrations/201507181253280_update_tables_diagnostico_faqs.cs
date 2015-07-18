namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tables_diagnostico_faqs : DbMigration
    {
        public override void Up()
        {
            AddColumn("MinTransporte.item_diagnostico", "UrlImagen", c => c.String());
            DropColumn("MinTransporte.pregunta_frecuente", "UrlImagen");
        }
        
        public override void Down()
        {
            AddColumn("MinTransporte.pregunta_frecuente", "UrlImagen", c => c.String(maxLength: 1024));
            DropColumn("MinTransporte.item_diagnostico", "UrlImagen");
        }
    }
}
