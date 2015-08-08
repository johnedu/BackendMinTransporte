(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.historiasViales';

    /*****************************************************************
    * 
    * CONTROLADOR DE HISTORIAS EN LA VÍA
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.historiasViales = [];

           //Funcion encargada de consultar las historias en la base de datos
           function cargarHistoriasViales() {
               administracionService.getAllHistoriasViales().success(function (data) {
                   vm.historiasViales = bow.tablas.paginar(data.historiasViales, 10);
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarHistoriasViales();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Historia en la Vía
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/historiasViales/partials/modalNuevaHistoriaVial.cshtml',
                   controller: 'modalNuevaHistoriaVialController',
                   size: 'md'
               });

               modalInstance.result.then(function (historia) {
                   cargarHistoriasViales();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la historia: ' + historia, abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la hisotoria'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar una Historia
           ************************************************************************/
           vm.abrirModalEditar = function (historiaId) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/historiasViales/partials/modalEditarHistoriaVial.cshtml',
                   controller: 'modalEditarHistoriaVialController',
                   size: 'md',
                   resolve: {
                       historiaEditar: function () {
                           return historiaId;
                       }
                   }
               });

               modalInstance.result.then(function (historia) {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la historia: ' + historia, abp.localization.localize('', 'Bow') + 'Información');
                   cargarHistoriasViales();

               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la historia vial'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar una Historia
           ************************************************************************/
           vm.abrirModalEliminar = function (historiaId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/historiasViales/partials/modalEliminarHistoriaVial.cshtml',
                    controller: 'modalEliminarHistoriaVialController',
                    size: 'md',
                    resolve: {
                        historiaEliminar: function () {
                            return historiaId;
                        }
                    }
                });

                modalInstance.result.then(function (historia) {
                    cargarHistoriasViales();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la historia: ' + historia, abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la historia vial'
                });
           }

           /************************************************************************
           * Llamado para modificar el estado de la historia vial
           ************************************************************************/
           vm.modificarEstadoHistoria = function (historia) {
               if (historia.esActiva) {
                   historia.esActiva = false;
               } else {
                   historia.esActiva = true;
               }
               administracionService.updateHistoriasVial(historia)
                   .success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se modificó correctamente el estado de la historia: ' + historia.nombre, abp.localization.localize('', 'Bow') + 'Información');
                       cargarHistoriasViales();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }
       }]);
})();