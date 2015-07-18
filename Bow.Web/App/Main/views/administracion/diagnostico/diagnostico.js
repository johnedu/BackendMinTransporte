(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.diagnostico';

    /*****************************************************************
    * 
    * CONTROLADOR DE PREGUNTAS DIAGNOSTICO
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.diagnostico = [];

           //Funcion encargada de consultar los diagnostico en la base de datos
       
           function cargarDiagnostico() {
               administracionService.getAllItemsDiagnosticoVial().success(function (data) {
                   vm.diagnostico = data.itemsDiagnosticoVial;
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarDiagnostico();

           /************************************************************************
            * Llamado para abrir Modal para Nuevo Diagnostico
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/diagnostico/partials/modalNuevoDiagnostico.cshtml',
                   controller: 'modalNuevoDiagnosticoController',
                   size: 'md'
               });

               modalInstance.result.then(function (diagnostico) {
                   cargarDiagnostico();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente el diagnostico: ' + diagnostico, abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar el diagnostico'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar Diagnostico
           ************************************************************************/
           vm.abrirModalEditar = function (diagnostico) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/diagnostico/partials/modalEditarDiagnostico.cshtml',
                   controller: 'modalEditarDiagnosticoController',
                   size: 'md',
                   resolve: {
                       diagnosticoEditar: function () {
                           return diagnostico;
                       }
                   }
               });

               modalInstance.result.then(function (diagnostico) {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente el diagnostico: ' + diagnostico, abp.localization.localize('', 'Bow') + 'Información');
                   cargarDiagnostico();
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar el diagnostico '
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar diagnostico
           ************************************************************************/
           vm.abrirModalEliminar = function (diagnosticoId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/diagnostico/partials/modalEliminarDiagnostico.cshtml',
                    controller: 'modalEliminarDiagnosticoController',
                    size: 'md',
                    resolve: {
                        diagnosticoEliminar: function () {
                            return diagnosticoId;
                        }
                    }
                });

                modalInstance.result.then(function (diagnostico) {
                    cargarDiagnostico();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente el diagnostico: ' + diagnostico, abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar el diagnostico '
                });
           }

           /************************************************************************
           * Llamado para modificar el estado de la diagnostico
           ************************************************************************/
           vm.modificarEstadoDiagnostico = function (diagnostico) {
               alert();
               if (diagnostico.esActivo) {
                   diagnostico.esActivo = false;
               } else {
                   diagnostico.esActivo = true;
               }
               administracionService.updateItemDiagnosticoVial(diagnostico).success(function () {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se modificó correctamente el estado del diagnostico: ' + diagnostico.Nombre, abp.localization.localize('', 'Bow') + 'Información');
                   cargarDiagnostico();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }
       }]);
})();