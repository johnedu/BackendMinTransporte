(function () {
    angular.module('app').controller('modalNuevaHistoriaVialController', ['$scope', '$modalInstance', 'abp.services.app.administracion',
        function ($scope, $modalInstance, administracionService) {

            $scope.historiaVial = {
                nombre: '',
                descripcion: '',
                nombrePersona: '',
                url: '',
                categoriaId: ''
            };

            $scope.listaCategorias = '';

            //Funcion encargada de consultar las categorias disponibles
            function cargarCategorias() {
                administracionService.getAllCategorias().success(function (data) {
                    $scope.listaCategorias = data.tiposReporte;
                }).error(function (error) {
                    console.log(error);
                });
            }
            cargarCategorias();

            $scope.okModal = function () {
                administracionService.saveHistoriasVial($scope.historiaVial)
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