(function () {
    angular.module('app').controller('modalEditarHistoriaVialController', ['$scope', '$modalInstance', 'historiaEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, historiaEditar, administracionService) {

            $scope.codigoHistoria = historiaEditar;

            $scope.historiaVial = {
                id: '',
                nombre: '',
                descripcion: '',
                nombrePersona: '',
                url: '',
                categoriaId: '',
                esActiva: true
            };

            //Funcion encargada de consultar las categorias disponibles
            function cargarCategorias() {
                administracionService.getAllCategorias().success(function (data) {
                    $scope.listaCategorias = data.tiposReporte;
                }).error(function (error) {
                    console.log(error);
                });
            }
            cargarCategorias();

            administracionService.getHistoriaVial({ id: historiaEditar })
                .success(function (data) {
                    $scope.historiaVial = data;
                });

            $scope.okModal = function () {
                administracionService.updateHistoriasVial($scope.historiaVial)
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