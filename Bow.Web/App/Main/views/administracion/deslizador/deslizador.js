(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.deslizador';

    /*****************************************************************
    * 
    * CONTROLADOR DE DESLIZADOR
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.deslizador = [];

           //Funcion encargada de consultar las imagenes del slider
           function cargarDeslizador() {
               administracionService.getAllDeslizador().success(function (data) {
                   vm.deslizador = data.deslizador;
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarDeslizador();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Imagen del slider
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/deslizador/partials/modalNuevaDeslizador.cshtml',
                   controller: 'modalNuevoDeslizadorController',
                   size: 'md'
               });

               modalInstance.result.then(function (deslizador) {
                   cargarDeslizador();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la imagen nueva del slider: ' + deslizador, abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la imagen nueva del slider'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Imagen del slider
           ************************************************************************/
           vm.abrirModalEditar = function (delizadorId) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/deslizador/partials/modalEditarDeslizador.cshtml',
                   controller: 'modalEditarDeslizadorController',
                   size: 'md',
                   resolve: {
                       deslizadorEditar: function () {
                           return delizadorId;
                       }
                   }
               });

               modalInstance.result.then(function (deslizador) {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la imagen nueva del slider: ' + deslizador, abp.localization.localize('', 'Bow') + 'Información');
                   cargarDeslizador();

               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la imagen nueva del slider'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar una Imagen del slider
           ************************************************************************/
           vm.abrirModalEliminar = function (delizadorId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/deslizador/partials/modalEliminarDeslizador.cshtml',
                    controller: 'modalEliminarDeslizadorController',
                    size: 'md',
                    resolve: {
                        deslizadorEliminar: function () {
                            return delizadorId;
                        }
                    }
                });

                modalInstance.result.then(function (deslizador) {
                    cargarDeslizador();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la imagen nueva del slider: ' + deslizador, abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la imagen nueva del slider'
                });
           }

           /************************************************************************
           * Llamado para modificar el estado de la Imagen del slider
           ****************************************************************
           vm.modificarEstadoDeslizador = function (deslizador) {
               if (deslizador.esActiva) {
                   deslizador.esActiva = false;
               } else {
                   deslizador.esActiva = true;
               }
               administracionService.updateDeslizador(deslizador)
                   .success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se modificó correctamente el estado de la imagen nueva del slider: ' + deslizador.nombre, abp.localization.localize('', 'Bow') + 'Información');
                       cargarHistoriasViales();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }********/
       }]);
})();