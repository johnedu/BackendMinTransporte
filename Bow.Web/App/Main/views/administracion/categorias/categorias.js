(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.categorias';

    /*****************************************************************
    * 
    * CONTROLADOR DE CATEGORIAS
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.categorias = [];

           //Funcion encargada de consultar las categorias en la base de datos
           function cargarCategorias() {
               administracionService.getAllTipos().success(function (data) {
                   vm.categorias = data.tiposReporte;
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarCategorias();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Categoria
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/categorias/partials/modalNuevaCategoria.cshtml',
                   controller: 'modalNuevaCategoriaController',
                   size: 'md'
               });

               modalInstance.result.then(function (categoria) {
                   cargarCategorias();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la categoría: ' + categoria, abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la categoría'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar Categoría
           ************************************************************************/
           vm.abrirModalEditar = function (categoriaId) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/categorias/partials/modalEditarCategoria.cshtml',
                   controller: 'modalEditarCategoriaController',
                   size: 'md',
                   resolve: {
                       categoriaEditar: function () {
                           return categoriaId;
                       }
                   }
               });

               modalInstance.result.then(function (categoria) {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la categoría: ' + categoria, abp.localization.localize('', 'Bow') + 'Información');
                   cargarCategorias();
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la categoría'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar la categoría
           ************************************************************************/
           vm.abrirModalEliminar = function (categoriaId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/categorias/partials/modalEliminarCategoria.cshtml',
                    controller: 'modalEliminarCategoriaController',
                    size: 'md',
                    resolve: {
                        categoriaEliminar: function () {
                            return categoriaId;
                        }
                    }
                });

                modalInstance.result.then(function (categoria) {
                    cargarCategorias();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la categoría: ' + categoria, abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la categoría'
                });
           }

           /************************************************************************
           * Llamado para modificar el estado de la categoría
           ************************************************************************/
           vm.modificarEstadoCategoria = function (categoria) {
               if (categoria.esActiva) {
                   categoria.esActiva = false;
               } else {
                   categoria.esActiva = true;
               }
               administracionService.updateTipo(categoria)
                   .success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se modificó correctamente el estado de la categoría: ' + categoria.nombre, abp.localization.localize('', 'Bow') + 'Información');
                       cargarCategorias();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }
       }]);
})();