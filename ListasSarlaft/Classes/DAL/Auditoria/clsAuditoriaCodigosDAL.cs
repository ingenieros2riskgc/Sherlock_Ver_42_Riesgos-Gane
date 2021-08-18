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
    public class clsAuditoriaCodigosDAL
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
        public DataTable mtdGetCodigoAuditoria(string area)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT [IdCodigoAuditoria],[sigla],[consecutivo],[area] ";
                strQryFrom = "FROM [Auditoria].[AuditoriaCodigos] ";
                strQryWhere = "where area = '" + area+"'";

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
    }
}