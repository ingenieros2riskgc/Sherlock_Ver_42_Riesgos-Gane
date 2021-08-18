using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsArchivosAuditoriaBLL
    {
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarArchivo(string nombrearchivo, int length, byte[] archivo, string IdRegistro, ref string strErrMsg, string extension, int TipoArchivo, string path, int idHallazgo)
        {
            bool booResult = false;
            clsArchivosAuditoriaDAL cDtCrlVersion = new clsArchivosAuditoriaDAL();
            try
            {
                booResult = cDtCrlVersion.Guardar(nombrearchivo, length, archivo, IdRegistro, ref strErrMsg, extension, TipoArchivo, path, idHallazgo);
            }
            catch (Exception ex)
            {
                strErrMsg = "nombrearchivo: " + nombrearchivo + " length: " + length + " IdRegistro:" + " extension:" + extension;
            }
            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar el archivo adjunto
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public byte[] mtdGetPathFile(ref string strErrMsg, int IdAuditoria)
        {
            byte[] file = null;
            clsArchivosAuditoriaDAL cDtAuditoria = new clsArchivosAuditoriaDAL();
            DataTable dt = cDtAuditoria.mtdGetPathFile(ref strErrMsg, IdAuditoria);
            foreach (DataRow dr in dt.Rows)
            {
                file = (byte[])dr["archivo"];
            }
            return file;
        }
    }
}