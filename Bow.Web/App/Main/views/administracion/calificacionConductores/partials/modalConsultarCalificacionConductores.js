(function () {
    angular.module('app').controller('modalConsultarCalificacionConductoresController', ['$scope', '$modalInstance', 'reporteEditar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, reporteEditar, administracionService) {

            $scope.reporteCalificacion = '';

            administracionService.getReporteCalificaciones({ id: reporteEditar })
                .success(function (data) {
                    $scope.reporteCalificacion = data;
                });

            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();