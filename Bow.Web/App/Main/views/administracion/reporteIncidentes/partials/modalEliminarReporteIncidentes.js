(function () {
    angular.module('app').controller('modalEliminarReporteIncidentesController', ['$scope', '$modalInstance', 'reporteInactivar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, reporteInactivar, administracionService) {

            administracionService.getPreguntaFrecuente({ id: reporteInactivar })
                .success(function (data) {
                    $scope.reporteIncidente = data;
                });

            $scope.okModal = function () {
                administracionService.updateStateReporteIncidentes({ id: reporteInactivar })
                    .success(function () {
                        $modalInstance.close($scope.reporteIncidente.tipoReporteIncidente);
                    }).error(function (error) {
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();

