(function () {
    angular.module('app').controller('modalEditarCategoriaController', ['$scope', '$modalInstance', 'categoriaEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, categoriaEditar, administracionService) {

            $scope.categoria = {
                id: '',
                nombre: '',
                tipoCategoria: '',
                urlImagen: '',
                esActiva: true
            };

            administracionService.getTipoReporte({ id: categoriaEditar })
                .success(function (data) {
                    $scope.categoria = data;
                });

            $scope.okModal = function () {
                administracionService.updateTipo($scope.categoria)
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