(function () {
    angular.module('app').controller('modalEditarNoticiaController', ['$scope', '$modalInstance', 'noticiaEditar', '$http', '$timeout', 'Upload', 'abp.services.app.utilidades', 'abp.services.app.administracion',
        function ($scope, $modalInstance, noticiaEditar, $http, $timeout, Upload, utilidadesService, administracionService) {

            $scope.noticia = {
                titulo: '',
                descripcion: '',
                urlImagen: '',
                url: ''
            };

            administracionService.getNoticias({ id: noticiaEditar })
                .success(function (data) {
                    $scope.noticia = data;
                });

            $scope.okModal = function () {
                administracionService.updateNoticias($scope.noticia)
                    .success(function () {
                        $modalInstance.close($scope.noticia.titulo);
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
                            // get upload percentage
                            $scope.showProgressBar = true;
                            $scope.percent = parseInt(100.0 * evt.loaded / evt.total);
                        }).success(function (data, status, headers, config) {
                            // file is uploaded successfully
                            console.log('Archivo cargado: ', data);
                            utilidadesService.readFile({ ruta: data.returnData, realFileName: data.returnDataType })
                                .success(function (data) {
                                    $scope.noticia.urlImagen = data.urlImagen;
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