namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_preguntasFrecuentes : DbMigration
    {
        public override void Up()
        {
            AddColumn("MinTransporte.pregunta_frecuente", "EsActiva", c => c.Boolean(nullable: false));
            DropColumn("MinTransporte.pregunta_frecuente", "EstadoActiva");
            DropColumn("MinTransporte.pregunta_frecuente", "FechaCreacion");
            DropColumn("MinTransporte.pregunta_frecuente", "UsuarioIdCreacion");
            DropColumn("MinTransporte.pregunta_frecuente", "FechaModificacion");
            DropColumn("MinTransporte.pregunta_frecuente", "UsuarioIdModificacion");
        }
        
        public override void Down()
        {
            AddColumn("MinTransporte.pregunta_frecuente", "UsuarioIdModificacion", c => c.Long());
            AddColumn("MinTransporte.pregunta_frecuente", "FechaModificacion", c => c.String());
            AddColumn("MinTransporte.pregunta_frecuente", "UsuarioIdCreacion", c => c.Long(nullable: false));
            AddColumn("MinTransporte.pregunta_frecuente", "FechaCreacion", c => c.String());
            AddColumn("MinTransporte.pregunta_frecuente", "EstadoActiva", c => c.Boolean(nullable: false));
            DropColumn("MinTransporte.pregunta_frecuente", "EsActiva");
        }
    }
}
