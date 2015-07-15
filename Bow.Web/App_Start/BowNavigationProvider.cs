﻿using Abp.Application.Navigation;
using Abp.Localization;

namespace Bow.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class BowNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "menu_administracion",
                        new LocalizableString("menu_administracion", BowConsts.LocalizationSourceName),
                        icon: "fa fa-cogs"
                        ).AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_preguntasFrecuentes",
                                new LocalizableString("menu_administracion_preguntasFrecuentes", BowConsts.LocalizationSourceName),
                                url: "#/administracion/preguntasFrecuentes"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                "menu_administracion_historiasViales",
                                new LocalizableString("menu_administracion_historiasViales", BowConsts.LocalizationSourceName),
                                url: "#/administracion/historiasViales"
                            )
                        )
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        "menu_administracion_noticias",
                        //        new LocalizableString("menu_administracion_noticias", BowConsts.LocalizationSourceName),
                        //        url: "#/administracion/noticias"
                        //    )
                        //)
                );
        }
    }
}
