(function () {
    angular.module('app').controller('modalNuevaCategoriaController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.categoria = {
                nombre: '',
                tipoCategoria: '',
                urlImagen: ''
            };

            $scope.okModal = function () {

                administracionService.saveTipo($scope.categoria)
                    .success(function () {
                        $modalInstance.close($scope.categoria.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();