(function () {
    angular.module('app').controller('modalNuevoDiagnosticoController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.diagnostico = {
                nombre: '',
                observaciones: ''               
            };

            $scope.okModal = function () {
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