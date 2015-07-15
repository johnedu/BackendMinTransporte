(function () {
    angular.module('app').controller('modalEliminarCategoriaController', ['$scope', '$modalInstance', 'categoriaEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, categoriaEliminar, administracionService) {

            administracionService.getTipoReporte({ id: categoriaEliminar })
                .success(function (data) {
                    $scope.categoria = data;
                });

            $scope.okModal = function () {
                administracionService.deleteTipo({ id: categoriaEliminar })
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

