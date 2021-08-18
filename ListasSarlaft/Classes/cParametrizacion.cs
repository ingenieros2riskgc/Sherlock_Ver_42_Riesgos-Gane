using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cParametrizacion : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        public DataTable cargarRegiones()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Parametrizacion.Regiones.IdRegion, LTRIM(RTRIM(Parametrizacion.Regiones.NombreRegion)) AS NombreRegion, LTRIM(RTRIM(Listas.Usuarios.Usuario)) AS Usuario, CONVERT(DATETIME,Parametrizacion.Regiones.FechaRegistro, 107) AS FechaRegistro FROM Parametrizacion.Regiones INNER JOIN Listas.Usuarios ON Parametrizacion.Regiones.IdUsuario = Listas.Usuarios.IdUsuario");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
    }
}