﻿(function () {
    angular.module('app').controller('modalEliminarCalificacionConductoresController', ['$scope', '$modalInstance', 'reporteInactivar', 'abp.services.app.administracion',
        function ($scope, $modalInstance, reporteInactivar, administracionService) {

            administracionService.getPreguntaFrecuente({ id: reporteInactivar })
                .success(function (data) {
                    $scope.preguntaFrecuente = data;
                });

            $scope.okModal = function () {
                administracionService.deletePreguntaFrecuente({ id: reporteInactivar })
                    .success(function () {
                        $modalInstance.close($scope.preguntaFrecuente.pregunta);
                    }).error(function (error) {
                       
                        $scope.mensajeError = error.message;
                    });
            }


            $scope.cancelModal = function () {
                $modalInstance.dismiss('cancel');
            }
        }]);
})();

