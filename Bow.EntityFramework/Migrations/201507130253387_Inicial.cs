namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MinTransporte.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256),
                        MethodName = c.String(maxLength: 256),
                        Parameters = c.String(maxLength: 1024),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Exception = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MinTransporte.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("MinTransporte.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "MinTransporte.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("MinTransporte.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "MinTransporte.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        UserName = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128),
                        PasswordResetCode = c.String(maxLength: 128),
                        LastLoginTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("MinTransporte.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "MinTransporte.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "MinTransporte.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "MinTransporte.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.UserId)
                .ForeignKey("MinTransporte.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "MinTransporte.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizacionId = c.Int(),
                        TenancyName = c.String(nullable: false, maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("MinTransporte.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "MinTransporte.pregunta_frecuente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pregunta = c.String(nullable: false),
                        Respuesta = c.String(nullable: false),
                        EstadoActiva = c.Boolean(nullable: false),
                        FechaCreacion = c.String(),
                        UsuarioIdCreacion = c.Long(nullable: false),
                        FechaModificacion = c.String(),
                        UsuarioIdModificacion = c.Long(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaFrecuente_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MinTransporte.tipo_reporte",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoReporte_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MinTransporte.reporte_incidentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoReporteId = c.Int(nullable: false),
                        Direccion = c.String(maxLength: 512),
                        Latitud = c.String(nullable: false, maxLength: 100),
                        Longitud = c.String(nullable: false, maxLength: 100),
                        Observaciones = c.String(maxLength: 2048),
                        EsActivo = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReporteIncidentes_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.tipo_reporte", t => t.TipoReporteId)
                .Index(t => t.TipoReporteId);
            
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
            
            CreateTable(
                "MinTransporte.reporte_calificaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoVehiculoId = c.Int(nullable: false),
                        Placa = c.String(nullable: false, maxLength: 50),
                        Empresa = c.String(maxLength: 512),
                        Observaciones = c.String(maxLength: 2048),
                        Calificacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EsActiva = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReporteCalificaciones_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MinTransporte.tipo_vehiculo", t => t.TipoVehiculoId)
                .Index(t => t.TipoVehiculoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("MinTransporte.reporte_calificaciones", "TipoVehiculoId", "MinTransporte.tipo_vehiculo");
            DropForeignKey("MinTransporte.reporte_incidentes", "TipoReporteId", "MinTransporte.tipo_reporte");
            DropForeignKey("MinTransporte.AbpRoles", "TenantId", "MinTransporte.AbpTenants");
            DropForeignKey("MinTransporte.AbpPermissions", "RoleId", "MinTransporte.AbpRoles");
            DropForeignKey("MinTransporte.AbpRoles", "LastModifierUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpRoles", "DeleterUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpRoles", "CreatorUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUsers", "TenantId", "MinTransporte.AbpTenants");
            DropForeignKey("MinTransporte.AbpSettings", "TenantId", "MinTransporte.AbpTenants");
            DropForeignKey("MinTransporte.AbpTenants", "LastModifierUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpTenants", "DeleterUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpTenants", "CreatorUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpSettings", "UserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUserRoles", "UserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpPermissions", "UserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUserLogins", "UserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUsers", "LastModifierUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUsers", "DeleterUserId", "MinTransporte.AbpUsers");
            DropForeignKey("MinTransporte.AbpUsers", "CreatorUserId", "MinTransporte.AbpUsers");
            DropIndex("MinTransporte.reporte_calificaciones", new[] { "TipoVehiculoId" });
            DropIndex("MinTransporte.reporte_incidentes", new[] { "TipoReporteId" });
            DropIndex("MinTransporte.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("MinTransporte.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("MinTransporte.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("MinTransporte.AbpSettings", new[] { "UserId" });
            DropIndex("MinTransporte.AbpSettings", new[] { "TenantId" });
            DropIndex("MinTransporte.AbpUserRoles", new[] { "UserId" });
            DropIndex("MinTransporte.AbpUserLogins", new[] { "UserId" });
            DropIndex("MinTransporte.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("MinTransporte.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("MinTransporte.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("MinTransporte.AbpUsers", new[] { "TenantId" });
            DropIndex("MinTransporte.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("MinTransporte.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("MinTransporte.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("MinTransporte.AbpRoles", new[] { "TenantId" });
            DropIndex("MinTransporte.AbpPermissions", new[] { "UserId" });
            DropIndex("MinTransporte.AbpPermissions", new[] { "RoleId" });
            DropTable("MinTransporte.reporte_calificaciones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReporteCalificaciones_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.tipo_vehiculo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoVehiculo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.reporte_incidentes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReporteIncidentes_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.tipo_reporte",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TipoReporte_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.pregunta_frecuente",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaFrecuente_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.AbpSettings");
            DropTable("MinTransporte.AbpUserRoles");
            DropTable("MinTransporte.AbpUserLogins");
            DropTable("MinTransporte.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("MinTransporte.AbpPermissions");
            DropTable("MinTransporte.AbpAuditLogs");
        }
    }
}
