namespace Bow.Migrations {
    using Bow.Administracion.Entidades;
    using Bow.Migrations.Data;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bow.EntityFramework.BowDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MinTransporte";
        }

        protected override void Seed(Bow.EntityFramework.BowDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
