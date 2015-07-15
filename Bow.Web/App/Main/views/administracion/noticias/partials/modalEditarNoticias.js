(function () {
    angular.module('app').controller('modalEditarNoticiaController', ['$scope', '$modalInstance', 'preguntaEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, noticiaEditar, administracionService) {

            $scope.noticia = noticiaEditar;

            $scope.noticia = {
                titulo: '',
                descripcion: '',
                urlImagen: ''
            };

            administracionService.getNoticias({ id: noticiaEditar })
                .success(function (data) {
                    $scope.noticia = data;
                });

            $scope.okModal = function () {
                administracionService.updateNoticias($scope.noticia)
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