(function () {
    angular.module('app').controller('modalEliminarDeslizadorController', ['$scope', '$modalInstance', 'deslizadorEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, deslizadorEliminar, administracionService) {

            administracionService.getDeslizador({ id: deslizadorEliminar })
                .success(function (data) {
                    $scope.deslizador = data;
                });

            $scope.okModal = function () {
                administracionService.deleteDeslizador({ id: deslizadorEliminar })
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

