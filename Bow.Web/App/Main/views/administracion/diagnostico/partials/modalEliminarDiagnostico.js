(function () {
    angular.module('app').controller('modalEliminarDiagnosticoController', ['$scope', '$modalInstance', 'diagnosticoEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, diagnosticoEliminar, administracionService) {

            administracionService.getNotGetItemByDiagnosticoVialicias({ id: diagnosticoEliminar })
                .success(function (data) {
                    $scope.diagnosticoEliminar = data;
                });

            $scope.okModal = function () {
                administracionService.deleteItemDiagnosticoVial({ id: diagnosticoEliminar })
                    .success(function () {
                        $modalInstance.close($scope.diagnosticoEliminar.nombre);
                    }).error(function (error) {
                       
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();

