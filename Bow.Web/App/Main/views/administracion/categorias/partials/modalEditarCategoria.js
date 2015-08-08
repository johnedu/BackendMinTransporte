(function () {
    angular.module('app').controller('modalEditarCategoriaController', ['$scope', '$modalInstance', 'categoriaEditar', '$http', '$timeout', 'Upload', 'abp.services.app.utilidades', 'abp.services.app.administracion',
        function ($scope, $modalInstance, categoriaEditar, $http, $timeout, Upload, utilidadesService, administracionService) {

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

            $scope.upload = [];
            $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };
            $scope.percent = 0;
            $scope.showProgressBar = false;

            $scope.onFileSelect = function ($files) {
                $scope.percent = 0;
                $scope.showProgressBar = false;
                //$files: an array of files selected, each file has name, size, and type.
                for (var i = 0; i < $files.length; i++) {
                    var $file = $files[i];
                    (function (index) {
                        $scope.upload[index] = Upload.upload({
                            url: "./api/file/upload", // webapi url
                            method: "POST",
                            data: { fileUploadObj: $scope.fileUploadObj },
                            file: $file
                        }).progress(function (evt) {
                            $scope.showProgressBar = true;
                            // get upload percentage
                            $scope.percent = parseInt(100.0 * evt.loaded / evt.total);
                        }).success(function (data, status, headers, config) {
                            // file is uploaded successfully
                            console.log('Archivo cargado: ', data);
                            utilidadesService.readFile({ ruta: data.returnData, realFileName: data.returnDataType })
                                .success(function (data) {
                                    $scope.categoria.urlImagen = data.urlImagen;
                                    console.log("Archivo leído correctamente.");
                                });
                        }).error(function (data, status, headers, config) {
                            // file failed to upload
                            console.log('Error al cargar el archivo: ', data);
                        });
                    })(i);
                }
            }

            $scope.abortUpload = function (index) {
                $scope.upload[index].abort();
            }

        }]);
})();