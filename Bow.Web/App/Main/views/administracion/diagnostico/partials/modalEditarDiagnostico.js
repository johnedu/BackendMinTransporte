(function () {
    angular.module('app').controller('modalEditarDiagnosticoController', ['$scope', '$modalInstance', 'diagnosticoEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, diagnosticoEditar, administracionService) {

            $scope.diagnostico = {
                id: '',
                nombre: '',
                observaciones: '',
                urlImagen: '',
                esRequerido: ''
            };

            administracionService.getItemByDiagnosticoVial({ id: diagnosticoEditar })
                .success(function (data) {
                    $scope.diagnostico = data;
                });

            $scope.okModal = function () {
                administracionService.updateItemDiagnosticoVial($scope.diagnostico)
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