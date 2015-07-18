(function () {
    angular.module('app').controller('modalEliminarHistoriaVialController', ['$scope', '$modalInstance', 'historiaEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, historiaEliminar, administracionService) {

            administracionService.getHistoriaVial({ id: historiaEliminar })
                .success(function (data) {
                    $scope.historiaVial = data;
                });

            $scope.okModal = function () {
                administracionService.deleteHistoriasVial({ id: historiaEliminar })
                    .success(function () {
                        $modalInstance.close($scope.historiaVial.nombre);
                    }).error(function (error) {
                       
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();

