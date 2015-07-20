(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.calificacionConductores';

    /*****************************************************************
    * 
    * CONTROLADOR DE CALIFICACION DE CONDUCTORES
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.listaReporteConductores = [];

           //Funcion encargada de consultar las preguntas frecuentes en la base de datos
           function cargarReporteConductores() {
               administracionService.getAllReporteCalificaciones().success(function (data) {
                   vm.listaReporteConductores = data.reportesCalificaciones;
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarReporteConductores();

           /************************************************************************
           * Llamado para abrir Modal para consultar detalle del reporte
           ************************************************************************/
           vm.abrirModalConsultar = function (reporteId) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/calificacionConductores/partials/modalConsultarCalificacionConductores.cshtml',
                   controller: 'modalConsultarCalificacionConductoresController',
                   size: 'md',
                   resolve: {
                       reporteEditar: function () {
                           return reporteId;
                       }
                   }
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Inactivar el reporte de calificación
           ************************************************************************/
           vm.abrirModalEliminar = function (reporteId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/calificacionConductores/partials/modalEliminarCalificacionConductores.cshtml',
                    controller: 'modalEliminarCalificacionConductoresController',
                    size: 'md',
                    resolve: {
                        reporteInactivar: function () {
                            return reporteId;
                        }
                    }
                });

                modalInstance.result.then(function (pregunta) {
                    cargarPreguntasFrecuentes();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se inactivó correctamente el reporte.', abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al modificar el reporte'
                });
           }

       }]);
})();