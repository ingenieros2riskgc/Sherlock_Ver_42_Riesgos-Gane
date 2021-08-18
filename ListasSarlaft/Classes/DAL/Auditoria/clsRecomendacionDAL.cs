using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes.DTO.Auditoria;

namespace ListasSarlaft.Classes.DAL.Auditoria
{
    public class clsRecomendacionDAL
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        cControl cControl = new cControl();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;

        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
            strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        #endregion Variables Globales
        public DataTable mtdGetRecomendaciones(string IdHallazgo)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = string.Format("SELECT [IdRecomendacion], CONVERT(VARCHAR(10),R.[FechaRegistro],120) AS FechaRegistro "+
                   "FROM   Listas.Usuarios AS U, [Auditoria].[Recomendacion] AS R "+
                   "LEFT JOIN Parametrizacion.[JerarquiaOrganizacional] AS J1 ON R.[IdDependenciaAuditada] = J1.IdHijo "+
                   "LEFT JOIN Parametrizacion.[DetalleJerarquiaOrg] AS DJ ON J1.idHijo = DJ.idHijo "+
                   "LEFT JOIN Parametrizacion.[JerarquiaOrganizacional] AS J2 ON R.[IdDependenciaRespuesta] = J2.IdHijo "+
                   "WHERE R.IdUsuario = U.IdUsuario AND R.Tipo = 'Dependencia' AND R.IdHallazgo = {0} "+
                   "UNION "+
                   "SELECT[IdRecomendacion], CONVERT(VARCHAR(10), R.[FechaRegistro], 120) AS FechaRegistro "+
                   "FROM[Procesos].Proceso AS P, [Listas].Usuarios AS U, [Auditoria].[Recomendacion] AS R "+
                   "LEFT JOIN Parametrizacion.[JerarquiaOrganizacional] AS J3 ON R.[IdDependenciaRespuesta] = J3.IdHijo "+
                   "WHERE R.IdUsuario = U.IdUsuario AND R.IdSubproceso = P.IdProceso AND R.Tipo = 'Procesos' AND "+
                    "R.IdHallazgo = {0}",IdHallazgo);
                strConsulta = string.Format("{0} {1} {2}", strQrySelect, strQryFrom, strQryWhere);

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInfo;
        }
        public bool mtdUpdateRecomendacionEdad(ref string strErrMsg, string IdHallazgo, int edadRecomendacion)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[Recomendacion] SET [edadRecomendacion] = {0} "+
                    "WHERE IdHallazgo = {1}", edadRecomendacion, IdHallazgo);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}