(function () {
    angular.module('app').controller('modalNuevoNoticiaController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.noticia = {
                titulo: '',
                descripcion: '',
                urlImagen: ''
               
            };

            $scope.okModal = function () {

                administracionService.saveNoticia($scope.noticia)
                    .success(function () {
                        $modalInstance.close($scope.noticia.titulo);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();