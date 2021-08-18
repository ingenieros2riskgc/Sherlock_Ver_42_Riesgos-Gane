using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsRegistroAuditoriaDAL
    {
        public bool mtdInsertarRegistroAuditoria(clsRegistroAuditoriaDTO objEnc, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                /*strConsulta = string.Format("INSERT INTO [Auditoria].[RegistrosAuditoria] ([IdAuditoria],[Tema], [IdEstandar], [Tipo], [FechaRegistro], [IdPlaneacion], [IdDependencia], " +
                    "[IdProceso], [IdUsuario], [IdEmpresa], [Estado], [Recursos], [Objetivo], [Alcance], [NivelImportancia], [IdDetalleTipo_TipoNaturaleza], " +
                    "[FechaInicio], [FechaCierre]) " +
                    "VALUES ({17},'{0}',{1},'{2}',(CONVERT(SMALLDATETIME,'{3}',101)),{4},{5},{6},{7},{8},'{9}','{10}','{11}','{12}','{13}',{14},(CONVERT(SMALLDATETIME,'{15}',101)),(CONVERT(SMALLDATETIME,'{16}',101)))",
                    objEnc.strTema, objEnc.intIdEstandar, objEnc.strTipo, objEnc.strFechaRegistro, objEnc.intIdPlaneacion, objEnc.intIdDependencia, objEnc.intIdProceso, objEnc.intIdUsuario,
                    objEnc.intIdEmpresa, objEnc.strEstado, objEnc.strRecursos, objEnc.strObjetivo, objEnc.strAlcance, objEnc.strNivelImportancia, objEnc.intIdDetalle_TipoNaturaleza,
                    objEnc.strFechaInicio, objEnc.strFechaCierre, objEnc.intIdAuditoria);*/
                strConsulta = string.Format("INSERT INTO [Auditoria].[RegistrosAuditoria] ([Tema],[FechaRegistro],[IdUsuario],[FechaInicio],[FechaCierre],[IdPlaneacion],[idArea]) VALUES(" +
                    "'{0}',(CONVERT(SMALLDATETIME,'{1}',101)),{2},(CONVERT(SMALLDATETIME,'{3}',101)),(CONVERT(SMALLDATETIME,'{4}',101)),{5},{6})", objEnc.strTema, objEnc.strFechaRegistro, objEnc.intIdUsuario, 
                    objEnc.strFechaInicio, objEnc.strFechaCierre, objEnc.intIdPlaneacion, objEnc.intIdArea);


                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el registro de Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRegistrosAuditoria(ref DataTable dtCaracOut, ref string strErrMsg, int IdAuditoria, int CodAuditoria, string NombreAud, int IdEstandar, int IdArea)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                if(CodAuditoria != 0)
                {
                    strCondicion = string.Format(" and Auditoria.IdAuditoria = {0}", CodAuditoria);
                }
                if(NombreAud != string.Empty)
                {
                    if(strCondicion != string.Empty)
                        strCondicion = strCondicion + string.Format(" and Auditoria.Tema LIKE '%{0}%'", NombreAud);
                    else
                        strCondicion = string.Format(" and Auditoria.Tema LIKE '%{0}%'", NombreAud);
                }
                if(IdEstandar != 0)
                {
                    if (strCondicion != string.Empty)
                        strCondicion = strCondicion + string.Format(" and Auditoria.IdEstandar = {0}", IdEstandar);
                    else
                        strCondicion = string.Format(" and Auditoria.IdEstandar = {0}", IdEstandar);
                }
                strConsulta = string.Format("SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo],ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([IdProceso], 0) as IdProceso, [NombreHijo] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa],  CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10),[FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion],[Especial],[periodicidad],[codigoAuditoria] " +
         "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Parametrizacion].[JerarquiaOrganizacional] " +
         "WHERE[Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
                "[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
                "[Auditoria].IdDependencia = [JerarquiaOrganizacional].IdHijo AND " +
                "[Auditoria].IdPlaneacion = {0} and IdDependencia <> 0 and IdProceso = 0 and [Auditoria].idArea ={2} {1}" +
         " UNION " +
         "SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [Proceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro], 120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion],[Especial],[periodicidad],[codigoAuditoria] " +
         "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[Proceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [Proceso].IdProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and " +
"[Auditoria].IdPlaneacion = {0} " +
"and AuditoriaProceso.IdTipoProceso = 2 and [Auditoria].idArea ={2} {1}" +
         " UNION " +
         "SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [MacroProceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion],[Especial],[periodicidad],[codigoAuditoria] " +
         "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[MacroProceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND" +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [MacroProceso].IdMacroProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and" +
"[Auditoria].IdPlaneacion = {0}" +
" and AuditoriaProceso.IdTipoProceso = 1 and [Auditoria].idArea ={2} {1}", IdAuditoria, strCondicion, IdArea);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los registros de la auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdActualizarRegistroAuditoria(clsRegistroAuditoriaDTO objEnc, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                //IdAuditoria, IdProceso, IdTipoProceso
                if(objEnc.strTipo=="Procesos")
                    mtdActualizarAuditoriaProceso(objEnc.intIdAuditoria, objEnc.intIdProceso, 2, ref strErrMsg);
                strConsulta = string.Format("UPDATE [Auditoria].[Auditoria] " +
                    "SET [Tema] = '{0}',[IdEstandar] = {1},[Tipo] = '{2}',[FechaRegistro] = (CONVERT(SMALLDATETIME,'{3}',101)),[IdPlaneacion] = {4},[IdDependencia] = {5}"+
                    ",[IdProceso] = {6},[IdUsuario] = {7},[IdEmpresa] = {8},[Estado] = '{9}',[Recursos] = '{10}',[Objetivo] = '{11}',[Alcance] = '{12}',[NivelImportancia] = '{13}'"+
	                ",[IdDetalleTipo_TipoNaturaleza] = {14},[FechaInicio] = (CONVERT(SMALLDATETIME,'{15}',101)),[FechaCierre] = (CONVERT(SMALLDATETIME,'{16}',101))"+
                    ",[IdMesEjecucion] = {19},[SemanaEjecucion] = '{20}', [Especial] = '{21}', [periodicidad] = {22}" +
                    " WHERE [IdAuditoria] = {17} and [IdPlaneacion] = {18}",
                    objEnc.strTema, objEnc.intIdEstandar, objEnc.strTipo, objEnc.strFechaRegistro, objEnc.intIdPlaneacion, objEnc.intIdDependencia, objEnc.intIdProceso, objEnc.intIdUsuario,
                    objEnc.intIdEmpresa, objEnc.strEstado, objEnc.strRecursos, objEnc.strObjetivo, objEnc.strAlcance, objEnc.strNivelImportancia, objEnc.intIdDetalle_TipoNaturaleza,
                    objEnc.strFechaInicio, objEnc.strFechaCierre, objEnc.intIdAuditoria, objEnc.intIdPlaneacion, objEnc.intIdMesExe, objEnc.strSemanaExe, objEnc.strEspecial, objEnc.intPeriodicidad);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el registro de Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[AuditoriaProceso] SET [IdProceso] = {0},[IdTipoProceso] = {1} "
                    + " WHERE IdAuditoria = {2}", IdProceso, IdTipoProceso, IdAuditoria);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdAuditoria(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(IdRegistroAuditoria) LastId FROM [Auditoria].[RegistrosAuditoria]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la Auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaProceso] ([IdProceso],[IdTipoProceso],[IdRegistroAuditoria])"
                    + "VALUES({1},{2},{0})", IdAuditoria, IdProceso, IdTipoProceso);

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
        public bool mtdActualizarTemaAuditoria(clsRegistroAuditoriaDTO objEnc, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[RegistrosAuditoria] " +
                    "SET [Tema] = '{0}',[FechaInicio] = '{1}',[FechaCierre] = '{2}'" +
                    " WHERE [IdRegistroAuditoria] = {3} and [IdPlaneacion] = {4}",
                    objEnc.strTema,  objEnc.strFechaInicio, objEnc.strFechaCierre, objEnc.intIdRegistroAuditoria, objEnc.intIdPlaneacion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el registro de Auditoria. [{0}]", ex.Message);
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