(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.reporteIncidentes';

    /*****************************************************************
    * 
    * CONTROLADOR DE REPORTE DE INCIDENTES
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.listaReportesIncidente = [];

           //Funcion encargada de consultar las preguntas frecuentes en la base de datos
           function cargarReporteIncidentes() {
               administracionService.getAllReporteIncidentes().success(function (data) {
                   vm.listaReportesIncidente = bow.tablas.paginar(data.reportesIncidentes, 10);
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarReporteIncidentes();

           /************************************************************************
           * Llamado para abrir Modal para Consultar detalles del reporte de incidentes
           ************************************************************************/
           vm.abrirModalConsultar = function (reporteId) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/reporteIncidentes/partials/modalConsultarReporteIncidentes.cshtml',
                   controller: 'modalConsultarReporteIncidentesController',
                   size: 'md',
                   resolve: {
                       reporteEditar: function () {
                           return reporteId;
                       }
                   }
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar un reporte de incidente
           ************************************************************************/
           vm.abrirModalEliminar = function (reporteId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/reporteIncidentes/partials/modalEliminarReporteIncidentes.cshtml',
                    controller: 'modalEliminarReporteIncidentesController',
                    size: 'md',
                    resolve: {
                        reporteInactivar: function () {
                            return reporteId;
                        }
                    }
                });

                modalInstance.result.then(function () {
                    cargarReporteIncidentes();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente el reporte.', abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al eliminar el reporte.'
                });
           }
       }]);
})();