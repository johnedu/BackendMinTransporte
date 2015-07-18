(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        'naut',
        'abp',
        'ngFileUpload'
    ]);

    //Configuración de parámetros de paginación por defecto
    app.run(function (paginationConfig) {
        paginationConfig.maxSize = 5;
        paginationConfig.rotate = false;
        paginationConfig.boundaryLinks = true;
        paginationConfig.firstText = "<<";
        paginationConfig.previousText = "<";
        paginationConfig.nextText = ">";
        paginationConfig.lastText = ">>";
    });

    //Configuración de parámetros de datepicker por defecto
    app.run(function (datepickerConfig) {
        datepickerConfig.startingDay = 1;
        datepickerConfig.showWeeks = false;
    });
    app.run(function (datepickerPopupConfig) {
        datepickerPopupConfig.showButtonBar = false;
    });

    //Configuration for Angular UI routing.
    app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

        //ruta por defecto
            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in BowNavigationProvider
                })
                .state('index', {
                    url: '/index',
                    templateUrl: '/App/Main/views/administracion/index.cshtml',
                    menu: 'index'
                })
                .state('preguntasFrecuentes', {
                    url: '/administracion/preguntasFrecuentes',
                    templateUrl: '/App/Main/views/administracion/preguntasFrecuentes/preguntasFrecuentes.cshtml',
                    menu: 'menu_administracion_preguntasFrecuentes'
                })
                .state('historiasViales', {
                    url: '/administracion/historiasViales',
                    templateUrl: '/App/Main/views/administracion/historiasViales/historiasViales.cshtml',
                    menu: 'menu_administracion_historiasViales'
                })
                .state('categorias', {
                    url: '/administracion/categorias',
                    templateUrl: '/App/Main/views/administracion/categorias/categorias.cshtml',
                    menu: 'menu_administracion_categorias'
                })
                .state('noticias', {
                    url: '/administracion/noticias',
                    templateUrl: '/App/Main/views/administracion/noticias/noticias.cshtml',
                    menu: 'menu_administracion_noticias'
                })
                 .state('diagnostico', {
                     url: '/administracion/diagnostico',
                     templateUrl: '/App/Main/views/administracion/diagnostico/diagnostico.cshtml',
                     menu: 'menu_administracion_diagnostico'
                 })
                .state('deslizador', {
                    url: '/administracion/deslizador',
                    templateUrl: '/App/Main/views/administracion/deslizador/deslizador.cshtml',
                    menu: 'menu_administracion_deslizador'
                })
                .state('reporteIncidentes', {
                    url: '/administracion/reporteIncidentes',
                    templateUrl: '/App/Main/views/administracion/reporteIncidentes/reporteIncidentes.cshtml',
                    menu: 'menu_administracion_reporteIncidentes' 
                })
                .state('calificacionConductores', {
                    url: '/administracion/calificacionConductores',
                    templateUrl: '/App/Main/views/administracion/calificacionConductores/calificacionConductores.cshtml',
                    menu: 'menu_administracion_calificacionConductores'
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in BowNavigationProvider
                })
               

            /*$stateProvider.when('departamentos', {
                url: '/zonificacion/pais/:paisId'
            });*/


        }
    ]);
})();

