(function () {
    angular.module('app').controller('modalNuevoDiagnosticoController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.diagnostico = {
                nombre: '',
                observaciones: '',
                urlImagen: '',
                esRequerido: false
            };

            $scope.okModal = function () {
                console.log($scope.diagnostico);
                administracionService.saveItemDiagnosticoVial($scope.diagnostico)
                    .success(function () {
                        $modalInstance.close($scope.diagnostico.nombre);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();