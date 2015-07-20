(function () {
    angular.module('app').controller('modalConsultarReporteIncidentesController', ['$scope', '$modalInstance', 'reporteEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, reporteEditar, administracionService) {

            $scope.reporteIncidente = '';

            administracionService.getReporteIncidentes({ id: reporteEditar })
                .success(function (data) {
                    $scope.reporteIncidente = data;
                });

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();