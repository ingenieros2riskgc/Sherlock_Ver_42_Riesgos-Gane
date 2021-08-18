using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPlanificacionDAL
    {
        public bool mtdReplicarPlanificacion(int IdPlan, ref string strErrMsg, int IdUsuario, int IdRegistros, int NewPlan)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                #region Replica Planeacion
                /*strConsulta = string.Format("INSERT INTO [Auditoria].[Planeacion]"+
           "([Nombre],[Estado],[IdDependencia],[Observacion],[Recursos],[Alcance],[NivelImportancia]"+
           ",[IdDetalleTipo_TipoNaturaleza],[FechaPlaneacion],[FechaCierre],[FechaRegistro],[IdUsuario],[IdEmpresa])"+
"SELECT [Nombre]+'-Copia',[Estado],[IdDependencia],[Observacion],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza] "+
      ",[FechaPlaneacion],[FechaCierre],GETDATE() as FechaRegistro,{1} as IdUsuario,[IdEmpresa] "+
        "FROM [Auditoria].[Planeacion] "+
        "where [IdPlaneacion] = {0}", IdPlan, IdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Planeacion
                
                #region Replica Auditorias
                strConsulta = string.Format("INSERT INTO [Auditoria].[RegistrosAuditoria]"+
           "([IdPlaneacion],[Tema],[FechaRegistro],[IdUsuario],[FechaInicio],[FechaCierre])"+
"SELECT {2} as IdPlaneacion,[Tema],GETDATE() as FechaRegistro,{1} as IdUsuario "+
      ",[FechaInicio],[FechaCierre] "+
        "FROM [Auditoria].[RegistrosAuditoria] "+
        "where [IdPlaneacion] = {0}", IdPlan,IdUsuario, IdRegistros);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/

                strConsulta = string.Format("INSERT INTO [Auditoria].[Auditoria]"+
           "([IdPlaneacion],[Tema],[IdEstandar],[Tipo],[IdDependencia],[IdProceso]" +
           ",[FechaRegistro],[IdUsuario],[IdEmpresa],[Estado],[Encabezado],[Metodologia]"+
           ",[Objetivo],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza]"+
           ",[FechaInicio],[FechaCierre],[Conclusion],[Observaciones],[IdTipoProceso]"+
           ",[IdMesEjecucion],[SemanaEjecucion],[TituloInforme],[ReferenciaInforme]) "+
"SELECT {2} as IdPlaneacion,AA.[Tema]+'-Copia' as Tema,[IdEstandar],[Tipo],[IdDependencia],[IdProceso]" +
      ",GETDATE() as FechaRegistro,{1} as IdUsuario,[IdEmpresa],[Estado],[Encabezado]"+
      ",[Metodologia],[Objetivo],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza]"+
      ",AA.[FechaInicio],AA.[FechaCierre],[Conclusion],[Observaciones],[IdTipoProceso] "+
      ",[IdMesEjecucion],[SemanaEjecucion],[TituloInforme],[ReferenciaInforme] "+
        "FROM [Auditoria].[Auditoria] as AA " +
"inner join[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"where ARA.IdPlaneacion = {0} and AA.IdAuditoria = {3}", IdPlan, IdUsuario, NewPlan, IdRegistros);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();

                #endregion Replica Auditorias
                #region Get Last Id
                strConsulta = string.Format("SELECT MAX(IdAuditoria) LastId FROM [Auditoria].[Auditoria]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                cDatabase.desconectar();
                foreach (DataRow dr in dt.Rows)
                {
                    LastId = Convert.ToInt32(dr["LastId"].ToString());
                }
                #endregion Get Last Id
                #region Replica Auditoria Proceso
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaProceso] ([IdProceso],[IdTipoProceso],[IdAuditoria])"+
  "SELECT [IdProceso],[IdTipoProceso],{2} as IdAuditoria "+
        "FROM [Auditoria].[Auditoria] as AA "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0} and AA.IdAuditoria = {1}", IdPlan,IdRegistros, LastId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                //cDatabase.desconectar();
                #endregion Replica Auditoria Proceso
                #region Replica Auditoria Objetivo
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaObjetivo] ([IdAuditoria],[IdObjetivo],[Alcance],[IdGrupoAuditoria]" +
                ",[FechaInicial],[FechaFinal],[FechaRegistro],[IdUsuario])" +
                "SELECT '{0}' as IdAuditoria,[AuditoriaObjetivo].[IdObjetivo], NULL as Alcance,[AuditoriaObjetivo].[IdGrupoAuditoria]," +
                "NULL as FechaInicial, NULL as FechaFinal, GETDATE() as FechaRegistro,[IdUsuario] " +
                "FROM[Auditoria].[AuditoriaObjetivo] " +
                "WHERE[IdAuditoria] = {1}", LastId, IdRegistros);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Auditoria Objetivo
                /*#region Replica Objetivo
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaObjetivo]"+
           "([IdAuditoria],[IdObjetivo],[Alcance],[IdGrupoAuditoria],[FechaInicial]"+
           ",[FechaFinal],[FechaRegistro],[IdUsuario])"+
"SELECT AAO.[IdAuditoria],[IdObjetivo],AAO.[Alcance],[IdGrupoAuditoria] "+
      ",[FechaInicial],[FechaFinal],GETDATE() as FechaRegistro,{1} as IdUsuario "+
"FROM [Auditoria].[AuditoriaObjetivo] as AAO "+
"INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAO.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Objetivo
                #region Replica Otros Factores
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaOtrosFactores]"+
           "([IdAuditoria],[IdCiclo],[IdOtrosFactores],[FechaRegistro],[IdUsuario])"+
"SELECT AAOF.[IdAuditoria],[IdCiclo],[IdOtrosFactores],GETDATE() as FechaRegistro,{1} as IdUsuario "+
"FROM [Auditoria].[AuditoriaOtrosFactores] as AAOF "+
"INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOF.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Otros Factores
                #region Replica RICC
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaRICC]"+
           "([IdAuditoria],[IdCiclo],[IdRiesgoInherente],[IdCalificacionControl]"+
           ",[FechaRegistro],[IdUsuario])"+
"SELECT AARI.[IdAuditoria],[IdCiclo],[IdRiesgoInherente],[IdCalificacionControl] "+
      ",GETDATE() as FechaRegistro,{1} as IdUsuario "+
  "FROM [Auditoria].[AuditoriaRICC] AARI "+
 "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AARI.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica RICC
                #region Replica Auditoria Seguimiento
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaSeguimiento]"+
           "([IdAuditoria],[Seguimiento],[FechaSeguimiento],[IdUsuario])"+
"SELECT AAS.[IdAuditoria],[Seguimiento],GETDATE() as FechaSeguimiento,{1} as IdUsuario "+
"FROM [Auditoria].[AuditoriaSeguimiento] as AAS "+
"INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAS.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Auditoria Seguimiento
                #region Replica ObjEnfoque
                strConsulta = string.Format("INSERT INTO [Auditoria].[AudObjEnfoque]"+
           "([IdAuditoria],[IdObjetivo],[IdEnfoque],[FechaRegistro],[IdUsuario])"+
"SELECT AAOEF.[IdAuditoria],[IdObjetivo],[IdEnfoque],GETDATE() as FechaSeguimiento,{1} as IdUsuario "+
"FROM [Auditoria].[AudObjEnfoque] as AAOEF "+
"INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOEF.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}",IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica ObjEnfoque
                #region Replica ObjRecurso
                strConsulta = string.Format("INSERT INTO [Auditoria].[AudObjRecurso]"+
           "([IdAuditoria],[IdObjetivo],[IdGrupoAuditoria],[IdHijo],[Etapa]"+
           ",[FechaInicial],[FechaFinal],[HorasPlaneadas],[FechaRegistro],[IdUsuario])"+
"SELECT AAOR.[IdAuditoria],[IdObjetivo],[IdGrupoAuditoria],[IdHijo],[Etapa],[FechaInicial],[FechaFinal] "+
      ",[HorasPlaneadas],GETDATE() as FechaSeguimiento,{1} as IdUsuario "+
  "FROM [Auditoria].[AudObjRecurso] as AAOR "+
"INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOR.IdAuditoria "+
"INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion "+
"WHERE ARA.IdPlaneacion = {0}",IdPlan, IdUsuario);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                #endregion Replica ObjRecurso*/

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al replicar la Planeación. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                //cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdReplicarPlanificacionNew(int IdPlan, ref string strErrMsg, int IdUsuario)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                #region Replica Planeacion
                strConsulta = string.Format("INSERT INTO [Auditoria].[Planeacion]" +
           "([Nombre],[Estado],[IdDependencia],[Observacion],[Recursos],[Alcance],[NivelImportancia]" +
           ",[IdDetalleTipo_TipoNaturaleza],[FechaPlaneacion],[FechaCierre],[FechaRegistro],[IdUsuario],[IdEmpresa])" +
    "SELECT [Nombre]+'-Copia',[Estado],[IdDependencia],[Observacion],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza] " +
      ",[FechaPlaneacion],[FechaCierre],GETDATE() as FechaRegistro,{1} as IdUsuario,[IdEmpresa] " +
        "FROM [Auditoria].[Planeacion] " +
        "where [IdPlaneacion] = {0}", IdPlan, IdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                #endregion Replica Planeacion
                #region Get Last IdPlaneacion
                strConsulta = string.Format("SELECT MAX(IdPlaneacion) LastId FROM [Auditoria].[Planeacion]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                cDatabase.desconectar();
                foreach (DataRow dr in dt.Rows)
                {
                    LastId = Convert.ToInt32(dr["LastId"].ToString());
                }
                #endregion Get Last Id
                #region Replica Auditorias
                strConsulta = string.Format("INSERT INTO [Auditoria].[RegistrosAuditoria]"+
            "([IdPlaneacion],[Tema],[FechaRegistro],[IdUsuario],[FechaInicio],[FechaCierre])"+
     "SELECT {2} as IdPlaneacion,[Tema],GETDATE() as FechaRegistro,{1} as IdUsuario "+
       ",[FechaInicio],[FechaCierre] "+
         "FROM [Auditoria].[RegistrosAuditoria] "+
         "where [IdPlaneacion] = {0}", IdPlan,IdUsuario, LastId);

                 cDatabase.conectar();
                 cDatabase.ejecutarQuery(strConsulta);
                 cDatabase.desconectar();

                 /*strConsulta = string.Format("INSERT INTO [Auditoria].[Auditoria]" +
            "([IdPlaneacion],[Tema],[IdEstandar],[Tipo],[IdDependencia],[IdProceso]" +
            ",[FechaRegistro],[IdUsuario],[IdEmpresa],[Estado],[Encabezado],[Metodologia]" +
            ",[Objetivo],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza]" +
            ",[FechaInicio],[FechaCierre],[Conclusion],[Observaciones],[IdTipoProceso]" +
            ",[IdMesEjecucion],[SemanaEjecucion],[TituloInforme],[ReferenciaInforme]) " +
     "SELECT {2} as IdPlaneacion,AA.[Tema]+'-Copia' as Tema,[IdEstandar],[Tipo],[IdDependencia],[IdProceso]" +
       ",GETDATE() as FechaRegistro,{1} as IdUsuario,[IdEmpresa],[Estado],[Encabezado]" +
       ",[Metodologia],[Objetivo],[Recursos],[Alcance],[NivelImportancia],[IdDetalleTipo_TipoNaturaleza]" +
       ",AA.[FechaInicio],AA.[FechaCierre],[Conclusion],[Observaciones],[IdTipoProceso] " +
       ",[IdMesEjecucion],[SemanaEjecucion],[TituloInforme],[ReferenciaInforme] " +
         "FROM [Auditoria].[Auditoria] as AA " +
     "inner join[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
     "where ARA.IdPlaneacion = {0} and AA.IdAuditoria = {3}", IdPlan, IdUsuario, LastId, IdRegistros);*/

                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/

                #endregion Replica Auditorias
                #region Get Last Id
                strConsulta = string.Format("SELECT MAX(IdAuditoria) LastId FROM [Auditoria].[Auditoria]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                cDatabase.desconectar();
                foreach (DataRow dr in dt.Rows)
                {
                    LastId = Convert.ToInt32(dr["LastId"].ToString());
                }
                #endregion Get Last Id
                #region Replica Auditoria Proceso
                /*strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaProceso] ([IdProceso],[IdTipoProceso],[IdAuditoria])" +
    "SELECT [IdProceso],[IdTipoProceso],{2} as IdAuditoria " +
        "FROM [Auditoria].[Auditoria] as AA " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0} and AA.IdAuditoria = {1}", IdPlan, IdRegistros, LastId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();
                //cDatabase.desconectar();
                #endregion Replica Auditoria Proceso
                #region Replica Auditoria Objetivo
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaObjetivo] ([IdAuditoria],[IdObjetivo],[Alcance],[IdGrupoAuditoria]" +
                ",[FechaInicial],[FechaFinal],[FechaRegistro],[IdUsuario])" +
                "SELECT '{0}' as IdAuditoria,[AuditoriaObjetivo].[IdObjetivo], NULL as Alcance,[AuditoriaObjetivo].[IdGrupoAuditoria]," +
                "NULL as FechaInicial, NULL as FechaFinal, GETDATE() as FechaRegistro,[IdUsuario] " +
                "FROM[Auditoria].[AuditoriaObjetivo] " +
                "WHERE[IdAuditoria] = {1}", LastId, IdRegistros);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica Auditoria Objetivo
                #region Replica Objetivo
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaObjetivo]" +
           "([IdAuditoria],[IdObjetivo],[Alcance],[IdGrupoAuditoria],[FechaInicial]" +
           ",[FechaFinal],[FechaRegistro],[IdUsuario])" +
    "SELECT AAO.[IdAuditoria],[IdObjetivo],AAO.[Alcance],[IdGrupoAuditoria] " +
      ",[FechaInicial],[FechaFinal],GETDATE() as FechaRegistro,{1} as IdUsuario " +
    "FROM [Auditoria].[AuditoriaObjetivo] as AAO " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAO.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica Objetivo
                #region Replica Otros Factores
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaOtrosFactores]" +
           "([IdAuditoria],[IdCiclo],[IdOtrosFactores],[FechaRegistro],[IdUsuario])" +
    "SELECT AAOF.[IdAuditoria],[IdCiclo],[IdOtrosFactores],GETDATE() as FechaRegistro,{1} as IdUsuario " +
    "FROM [Auditoria].[AuditoriaOtrosFactores] as AAOF " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOF.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica Otros Factores
                #region Replica RICC
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaRICC]" +
           "([IdAuditoria],[IdCiclo],[IdRiesgoInherente],[IdCalificacionControl]" +
           ",[FechaRegistro],[IdUsuario])" +
    "SELECT AARI.[IdAuditoria],[IdCiclo],[IdRiesgoInherente],[IdCalificacionControl] " +
      ",GETDATE() as FechaRegistro,{1} as IdUsuario " +
    "FROM [Auditoria].[AuditoriaRICC] AARI " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AARI.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica RICC
                #region Replica Auditoria Seguimiento
                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaSeguimiento]" +
           "([IdAuditoria],[Seguimiento],[FechaSeguimiento],[IdUsuario])" +
    "SELECT AAS.[IdAuditoria],[Seguimiento],GETDATE() as FechaSeguimiento,{1} as IdUsuario " +
    "FROM [Auditoria].[AuditoriaSeguimiento] as AAS " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAS.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica Auditoria Seguimiento
                #region Replica ObjEnfoque
                strConsulta = string.Format("INSERT INTO [Auditoria].[AudObjEnfoque]" +
           "([IdAuditoria],[IdObjetivo],[IdEnfoque],[FechaRegistro],[IdUsuario])" +
    "SELECT AAOEF.[IdAuditoria],[IdObjetivo],[IdEnfoque],GETDATE() as FechaSeguimiento,{1} as IdUsuario " +
    "FROM [Auditoria].[AudObjEnfoque] as AAOEF " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOEF.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica ObjEnfoque
                #region Replica ObjRecurso
                strConsulta = string.Format("INSERT INTO [Auditoria].[AudObjRecurso]" +
           "([IdAuditoria],[IdObjetivo],[IdGrupoAuditoria],[IdHijo],[Etapa]" +
           ",[FechaInicial],[FechaFinal],[HorasPlaneadas],[FechaRegistro],[IdUsuario])" +
    "SELECT AAOR.[IdAuditoria],[IdObjetivo],[IdGrupoAuditoria],[IdHijo],[Etapa],[FechaInicial],[FechaFinal] " +
      ",[HorasPlaneadas],GETDATE() as FechaSeguimiento,{1} as IdUsuario " +
    "FROM [Auditoria].[AudObjRecurso] as AAOR " +
    "INNER JOIN[Auditoria].[Auditoria] as AA on AA.IdAuditoria = AAOR.IdAuditoria " +
    "INNER JOIN[Auditoria].[RegistrosAuditoria] as ARA on ARA.IdRegistroAuditoria = AA.IdPlaneacion " +
    "WHERE ARA.IdPlaneacion = {0}", IdPlan, IdUsuario);
                /*cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                cDatabase.desconectar();*/
                #endregion Replica ObjRecurso

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al replicar la Planeación. [{0}]", ex.Message);
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