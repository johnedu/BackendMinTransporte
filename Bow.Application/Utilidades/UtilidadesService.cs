using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using Bow.Administracion.Entidades;
using Bow.Administracion.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    public class UtilidadesService : IUtilidadesService
    {
        # region Repositorios

        # endregion

        public UtilidadesService()
        {
            
        }

        /***************************************************************************************************
         * Lectura de Archivos
         * ************************************************************************************************/

        //  Método para leer los archivos
        public ReadFileOutput ReadFile(ReadFileInput ruta)
        {
            string rutaCompletaArchivoTemporal = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Tmp\\FileUploads\\" + ruta.Ruta;
            string rutaCompletaNuevoArchivo = System.AppDomain.CurrentDomain.BaseDirectory + "App\\Main\\images\\uploadFiles\\" + ruta.Ruta;

            StreamReader file = new StreamReader(rutaCompletaArchivoTemporal);

            Image img = System.Drawing.Image.FromStream(file.BaseStream);

            string extension = ruta.RealFileName.Split('.').Last().ToString();

            if (extension.ToLower().Contains("png")) {
                rutaCompletaNuevoArchivo += "." + extension.ToLower();
                img.Save(rutaCompletaNuevoArchivo, ImageFormat.Png);
            }
            else if (extension.ToLower().Contains("jpg"))
            {
                rutaCompletaNuevoArchivo += "." + extension.ToLower();
                img.Save(rutaCompletaNuevoArchivo, ImageFormat.Jpeg);
            }

            file.Close();

            File.Delete(rutaCompletaArchivoTemporal); 

            return new ReadFileOutput { UrlImagen = "\\App\\Main\\images\\uploadFiles\\" + ruta.Ruta + "." + extension.ToLower() };
        }

    }
}
