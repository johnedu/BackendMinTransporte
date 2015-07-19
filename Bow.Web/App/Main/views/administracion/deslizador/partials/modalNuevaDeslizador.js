(function () {
    angular.module('app').controller('modalNuevoDeslizadorController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.deslizador = {
                nombre: '',
                urlImagen: ''
                
            };

            $scope.okModal = function () {
                administracionService.saveDeslizador($scope.deslizador)
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