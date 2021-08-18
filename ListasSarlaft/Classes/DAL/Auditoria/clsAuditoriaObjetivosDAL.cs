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
    public class clsAuditoriaObjetivosDAL
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        #endregion Variables Globales
        public void mtdInsertAuditoriaObjetivos(clsAuditoriaObjetivosDTO objAudObj)
        {
            try
            {
                string strQuery = string.Format("INSERT INTO [Auditoria].[Objetivo] ([IdEstandar],[Numero],[Nombre],[Descripcion],[FechaRegistro],[IdUsuario])"
                    + " VALUES({0},{1},'{2}','{3}',GETDATE(),{4})", objAudObj.intIdEstandar,objAudObj.intNumero,objAudObj.strNombre,objAudObj.strDescripcion,objAudObj.intIdUsuario);

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
        public void mtdInsertAuditoriaEnfoque(clsAuditoriaEnfoqueDTO objAudEnf)
        {
            try
            {
                string strQuery = string.Format("INSERT INTO [Auditoria].[Enfoque] ([IdObjetivo],[Numero],[Descripcion],[FechaRegistro],[IdUsuario])"
                    + " VALUES({0},{1},'{2}',GETDATE(),{3})", objAudEnf.intIdObjetivo, objAudEnf.intNumero, objAudEnf.strDescripcion, objAudEnf.intIdUsuario);

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
        public DataTable mtdGetObjetivoNombre(string nombre)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT [IdObjetivo] ";
                strQryFrom = "FROM [Auditoria].[Objetivo] ";
                strQryWhere = "where Nombre = '" + nombre + "'";

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
        public DataTable mtdGetEnfoqueNombre(string nombre)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT [IdEnfoque] ";
                strQryFrom = "FROM [Auditoria].[Enfoque] ";
                strQryWhere = "where Descripcion LIKE '%" + nombre + "%'";

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
        public DataTable mtdGetLiteralDescripcion(string descripcion, string IdEnfoqueUlt)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT [IdDetalleEnfoque] ";
                strQryFrom = "FROM [Auditoria].[DetalleEnfoque] ";
                strQryWhere = "where Descripcion like '%" + descripcion + "%' and IdEnfoque = "+IdEnfoqueUlt;

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
        public bool mtdDeleteObjetivoEnfoque(int IdAuditoria, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Auditoria].[AudObjEnfoque] "
                       + " WHERE IdAuditoria = {0}", IdAuditoria);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                strConsulta = string.Format("DELETE FROM [Auditoria].[AuditoriaObjetivo] "
                    + " WHERE IdAuditoria = {0}", IdAuditoria);

                
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
        public bool mtdUpdateAudObjRecurso(int IdAuditoria, int IdObjetivo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[AudObjRecurso] "
                    + "SET [IdObjetivo] = {1}"
                    + " WHERE IdAuditoria = {0}", IdAuditoria, IdObjetivo);

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