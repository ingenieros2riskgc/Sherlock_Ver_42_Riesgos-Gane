using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsHallazgosAuditoriaDAL
    {
        public DataTable mtdGetSecuencia(ref string strErrMsg, int IdAuditoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT COUNT(IdHallazgo) as CountH FROM [Auditoria].[Hallazgo] where IdAuditoria = {0}", IdAuditoria);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la secuencia. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdGetImagenesAuditoria(ref DataTable dtCaracOut, ref string strErrMsg, ref string variable, string IdHallazgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            //Calificacion
            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>()
               {
                   new SqlParameter() { ParameterName = "@IdAuditoria", SqlDbType = SqlDbType.VarChar, Value =  variable.Trim().ToString() },
                   new SqlParameter() { ParameterName = "@IdHallazgo", SqlDbType = SqlDbType.VarChar, Value =  IdHallazgo.Trim().ToString() },
               };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[dbo].[SP_ImagenesAuditoriav2]", parametros);

                
                dtCaracOut = dt;
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los porcentajes de la calificación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
    }
}