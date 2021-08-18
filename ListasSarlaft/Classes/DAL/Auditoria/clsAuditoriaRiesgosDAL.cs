using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes.DTO.Auditoria;

namespace ListasSarlaft.Classes.DAL
{
    public class clsAuditoriaRiesgosDAL
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
        public void mtdInsertAuditoriaRiesgos(clsAuditoriaRiesgosDTO objAudRie)
        {
            try
            {
                string strQuery = string.Format("INSERT INTO [Auditoria].[AuditoriaRiesgo] ([IdAuditoria],[IdRiesgo],[IdUsuario],[FechaRegistro])"
                    + " VALUES({0},{1},{2},GETDATE())", objAudRie.intIdAuditoria, objAudRie.intIdRiesgo, objAudRie.intIdUsuario);
                
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
        public DataTable mtdLoadAuditoriaRiesgos(int IdAuditoria)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT audrie.[IdAuditoriaRiesgo] ,audrie.[IdAuditoria],audrie.[IdRiesgo],audrie.[IdUsuario]"+
                                ",audrie.[FechaRegistro],usua.Usuario, rie.Codigo, rie.Nombre ";
                strQryFrom = "FROM [Auditoria].[AuditoriaRiesgo] as audrie INNER JOIN Listas.Usuarios as usua "+
                             "on usua.IdUsuario = audrie.IdUsuario "+
                             "INNER JOIN Riesgos.Riesgo as rie on rie.IdRiesgo = audrie.IdRiesgo";
                strQryWhere = "where audrie.IdAuditoria = " + IdAuditoria;
                strConsulta = string.Format("{0} {1} {2}", strQrySelect, strQryFrom,strQryWhere);
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
        public void mtdInsertAuditoriaRiesgosControles(clsAuditoriaRiesgosControlesDTO objAudRieCon)
        {
            try
            {
                string strQuery = string.Format("INSERT INTO [Auditoria].[AuditoriaRiesgosControles]([IdAuditoria],[IdRiesgo],[IdControl],[IdUsuario],[FechaRegistro])"
                    + " VALUES({0},{1},{2},{3},GETDATE())", objAudRieCon.intIdAuditoria, objAudRieCon.intIdRiesgo, objAudRieCon.intIdControl, objAudRieCon.intIdUsuario);

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
        public DataTable mtdLoadAuditoriaRiesgosControles(int IdAuditoria)
        {
            DataTable dtInfo = new DataTable();
            string strConsulta = string.Empty, strQrySelect = string.Empty, strQryFrom = string.Empty, strQryWhere = string.Empty;

            try
            {
                strQrySelect = "SELECT audriecon.[IdAuditoriaRiesgoControl],audriecon.[IdAuditoria],audriecon.[IdRiesgo],audriecon.[IdControl]" +
                                ",audriecon.[IdUsuario],audriecon.[FechaRegistro], usua.Usuario, rie.Codigo, con.CodigoControl, con.NombreControl ";
                strQryFrom = "FROM [Auditoria].[AuditoriaRiesgosControles] as audriecon " +
                             "INNER JOIN Listas.Usuarios as usua on usua.IdUsuario = audriecon.IdUsuario " +
                             "INNER JOIN Riesgos.Riesgo as rie on rie.IdRiesgo = audriecon.IdRiesgo "
                             + "INNER JOIN Riesgos.Control as con on con.IdControl = audriecon.IdControl ";
                strQryWhere = "where audriecon.IdAuditoria = "+IdAuditoria;
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
        public void mtdUpdateAuditoriaRiesgosControles(clsAuditoriaRiesgosControlesDTO objAudRieCon)
        {
            try
            {
                string strQuery = string.Format("UPDATE [Auditoria].[AuditoriaRiesgosControles] SET [IdControl] = {0}"+
                    " WHERE IdAuditoriaRiesgoControl = {1}", objAudRieCon.intIdControl, objAudRieCon.intIdAuditoriaRiesgoControl);

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
        public void mtdDeleteAuditoriaRiesgosControles(clsAuditoriaRiesgosControlesDTO objAudRieCon)
        {
            try
            {
                string strQuery = string.Format("DELETE FROM [Auditoria].[AuditoriaRiesgosControles]" +
                    " WHERE IdAuditoriaRiesgoControl = {0}", objAudRieCon.intIdAuditoriaRiesgoControl);

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
        public void mtdUpdateAuditoriaRiesgos(clsAuditoriaRiesgosDTO objAudRie)
        {
            try
            {
                string strQuery = string.Format("UPDATE [Auditoria].[AuditoriaRiesgo] SET [IdRiesgo] = {0}" +
                    " WHERE IdAuditoriaRiesgo = {1}", objAudRie.intIdRiesgo, objAudRie.intIdAuditoriaRiesgo);

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
        public void mtdDeleteAuditoriaRiesgos(clsAuditoriaRiesgosDTO objAudRie)
        {
            try
            {
                string strQuery = string.Format("DELETE FROM [Auditoria].[AuditoriaRiesgo]" +
                    " WHERE IdAuditoriaRiesgo = {0}", objAudRie.intIdAuditoriaRiesgo);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strQuery);
                cDataBase.desconectar();

                strQuery = string.Format("DELETE FROM [Auditoria].[AuditoriaRiesgosControles]"+
                    " WHERE IdAuditoria = {0} and IdRiesgo = {1}", objAudRie.intIdAuditoria, objAudRie.intIdRiesgo);
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