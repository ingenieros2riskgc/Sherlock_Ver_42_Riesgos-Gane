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
    public class clsAuditoriaIntroduccionDAL
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        cControl cControl = new cControl();
        //private OleDbParameter[] parameters;
        //private OleDbParameter parameter;
        #endregion Variables Globales
        public void mtdInsertAuditoriaIntroduccion(clsAuditoriaIntroduccionDTO objAudInd)
        {
            try
            {
                string strQuery = string.Format("INSERT INTO [Auditoria].[AuditoriaIntroduccion] ([IdAuditoria],[introduccion],[idUsuario],[fechaRegistro])"
                    + " VALUES({0},'{1}',{2},GETDATE())", objAudInd.intIdAuditoria,objAudInd.strIntroduccion,objAudInd.intIdUsuario);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQuery);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
    }
}