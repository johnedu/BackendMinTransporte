(function () {
    angular.module('app').controller('modalEliminarNoticiaController', ['$scope', '$modalInstance', 'preguntaEliminar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, noticiaEliminar, administracionService) {

            administracionService.getNoticias({ id: noticiaEliminar })
                .success(function (data) {
                    $scope.noticiaEliminar = data;
                });

            $scope.okModal = function () {
                administracionService.deleteNoticias({ id: noticiaEliminar })
                    .success(function () {
                        $modalInstance.close($scope.noticiaEliminar.titulo);
                    }).error(function (error) {
                       
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();

