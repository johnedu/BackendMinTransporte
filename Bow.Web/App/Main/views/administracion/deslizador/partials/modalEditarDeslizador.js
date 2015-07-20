(function () {
    angular.module('app').controller('modalEditarDeslizadorController', ['$scope', '$modalInstance', 'deslizadorEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, deslizadorEditar, administracionService) {

            $scope.deslizador = {
                nombre: '',              
                urlImagen: ''
            };

            administracionService.getDeslizador({ id: deslizadorEditar })
                .success(function (data) {
                    $scope.deslizador = data;
                });

            $scope.okModal = function () {
                administracionService.updateDeslizador($scope.deslizador)
                    .success(function () {
                        $modalInstance.close($scope.deslizador.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();