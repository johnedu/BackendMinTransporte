(function () {
    //Nombre del controlador   
    var controllerId = 'app.views.administracion.noticias';

    /*****************************************************************
    * 
    * CONTROLADOR DE PREGUNTAS FRECUENTES
    * 
    *****************************************************************/

    angular.module('app').controller(controllerId, ['$scope', '$modal', 'abp.services.app.administracion',
       function ($scope, $modal, administracionService) {
           var vm = this;

           //Inicializando Modelos

           vm.noticias = [];

           //Funcion encargada de consultar las noticias en la base de datos
           function cargarNoticias() {
               administracionService.getAllNoticias().success(function (data) {
                   vm.noticias = data.noticias;
               }).error(function (error) {
                   console.log(error);
               });
           }
           cargarNoticias();

           /************************************************************************
            * Llamado para abrir Modal para Nueva Noticia
            ************************************************************************/

           vm.abrirModalNueva= function () {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/noticias/partials/modalNuevoNoticias.cshtml',
                   controller: 'modalNuevoNoticiaController',
                   size: 'md'
               });

               modalInstance.result.then(function (noticia) {
                   cargarNoticias();
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se guardó correctamente la noticia: ' + noticia, abp.localization.localize('', 'Bow') + 'Información');
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al guardar la noticia'
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Editar noticia
           ************************************************************************/
           vm.abrirModalEditar = function (noticiaid) {
               var modalInstance = $modal.open({
                   templateUrl: '/App/Main/views/administracion/noticias/partials/modalEditarNoticias.cshtml',
                   controller: 'modalEditarNoticiaController',
                   size: 'md',
                   resolve: {
                       noticiaEditar: function () {
                           return noticiaid;
                       }
                   }
               });

               modalInstance.result.then(function (noticia) {
                   abp.notify.success(abp.localization.localize('', 'Bow') + 'Se actualizó correctamente la noticia: ' + noticia, abp.localization.localize('', 'Bow') + 'Información');
                   cargarNoticias();
               }, function () {
                   vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la noticia '
               });
           }

           /************************************************************************
           * Llamado para abrir Modal para Eliminar noticia
           ************************************************************************/
           vm.abrirModalEliminar = function (noticiaId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/administracion/noticias/partials/modalEliminarNoticias.cshtml',
                    controller: 'modalEliminarNoticiaController',
                    size: 'md',
                    resolve: {
                        noticiaEliminar: function () {
                            return noticiaId;
                        }
                    }
                });

                modalInstance.result.then(function (noticia) {
                    cargarNoticias();
                    abp.notify.success(abp.localization.localize('', 'Bow') + 'Se eliminó correctamente la noticia: ' + noticia, abp.localization.localize('', 'Bow') + 'Información');
                }, function () {
                    vm.resultado = abp.localization.localize('', 'Bow') + 'Ocurrió un problema al actualizar la noticia '
                });
           }

           /************************************************************************
           * Llamado para modificar el estado de la noticia
           ************************************************************************/
           vm.modificarEstadoNoticia = function (noticia) {
               if (noticia.esActiva) {
                   noticia.esActiva = false;
               } else {
                   noticia.esActiva = true;
               }
               administracionService.updateNoticias(noticia).success(function () {
                       abp.notify.success(abp.localization.localize('', 'Bow') + 'Se modificó correctamente el estado de la noticia: ' + noticia.titulo, abp.localization.localize('', 'Bow') + 'Información');
                       cargarNoticias();
                   }).error(function (error) {
                       $scope.mensajeError = error.message;
                   });
           }
       }]);
})();