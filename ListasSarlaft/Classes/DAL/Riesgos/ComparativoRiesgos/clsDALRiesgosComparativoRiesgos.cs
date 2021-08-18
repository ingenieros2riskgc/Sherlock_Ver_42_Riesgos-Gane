using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALRiesgoComparativoRiesgos
    {
        public bool mtdConsultarPefilesRiesgos(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [NumeroRegistros],[SumatoriaProbabilidad],[SumatoriaImpacto]"
                    + " FROM [Riesgos].[vwRiesgosComparativoRiesgos]"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCuadroComperativo(ref DataTable dtCaracOut, ref string strErrMsg, int opc)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT LTRIM(RTRIM(ISNULL(PCR.NombreClasificacionRiesgo, ''))) ClasificacionRiesgo,"+
"LTRIM(RTRIM(ISNULL(PRI.NombreRiesgoInherente, ''))) RiesgoInherente"+
//, RR.IdRiesgo, RR.CodRiesgo
/*, LTRIM(RTRIM(ISNULL(PRI.ValorRiesgoInherente, ''))) CodigoRiesgoInherente
, LTRIM(RTRIM(ISNULL(RiesgoResidual.ValorRiesgoInherente, ''))) CodigoRiesgoResidual
", LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual"+
" FROM  Parametrizacion.ClasificacionRiesgo AS PCR"+
" LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo LEFT JOIN Parametrizacion.ClasificacionGeneralRiesgo AS PCGR ON RR.IdClasificacionGeneralRiesgo = PCGR.IdClasificacionGeneralRiesgo LEFT JOIN Parametrizacion.ClasificacionParticularRiesgo AS PCPR ON RR.IdClasificacionParticularRiesgo = PCPR.IdClasificacionParticularRiesgo LEFT JOIN Parametrizacion.TipoRiesgoOperativo AS PTRO ON RR.IdTipoRiesgoOperativo = PTRO.IdTipoRiesgoOperativo LEFT JOIN Parametrizacion.TipoEventoOperativo AS PTEO ON RR.IdTipoEventoOperativo = PTEO.IdTipoEventoOperativo LEFT JOIN Procesos.CadenaValor AS PCV ON PCV.IdCadenaValor = RR.IdCadenaValor LEFT JOIN Procesos.Macroproceso AS PM ON RR.IdMacroproceso = PM.IdMacroProceso LEFT JOIN Procesos.Proceso AS PP ON RR.IdProceso = PP.IdProceso LEFT JOIN Procesos.Subproceso AS PS ON PS.IdSubProceso = RR.IdSubProceso LEFT JOIN Procesos.Actividad AS PA ON RR.IdActividad = PA.IdActividad LEFT JOIN Parametrizacion.Probabilidad AS PPr ON PPr.IdProbabilidad = RR.IdProbabilidad LEFT JOIN Parametrizacion.Probabilidad AS pr ON pr.IdProbabilidad = RR.IdProbabilidadResidual LEFT JOIN Parametrizacion.Impacto AS PIm ON PIm.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.Impacto AS im ON im.IdImpacto = RR.IdImpactoResidual LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto LEFT JOIN Parametrizacion.JerarquiaOrganizacional AS PJO ON PJO.idHijo = RR.IdResponsableRiesgo LEFT JOIN Parametrizacion.FactorRiesgoOperativo AS PFRO ON RR.IdFactorRiesgoOperativo = PFRO.IdFactorRiesgoOperativo LEFT JOIN Parametrizacion.RiesgoAsociadoOperativo AS PRAO ON RR.IdRiesgoAsociadoOperativo = PRAO.IdRiesgoAsociadoOperativo LEFT JOIN Parametrizacion.Regiones AS PReg  ON RR.IdRegion = PReg.IdRegion LEFT JOIN Parametrizacion.Paises AS PPai ON RR.IdPais = PPai.IdPais AND PReg.IdRegion = PPai.IdRegion LEFT JOIN Parametrizacion.Departamentos AS PDep ON RR.IdDepartamento = PDep.IdDepartamento AND PPai.IdPais = PDep.IdPais LEFT JOIN Parametrizacion.Ciudades AS PCiu ON RR.IdCiudad = PCiu.IdCiudad  AND PDep.IdDepartamento = PCiu.IdDepartamento LEFT JOIN Parametrizacion.OficinaSucursal AS POSuc ON RR.IdOficinaSucursal = POSuc.IdOficinaSucursal AND PCiu.IdCiudad = POSuc.IdCiudad INNER JOIN Listas.Usuarios LU ON RR.IdUsuario = LU.IdUsuario"+
                         " left JOIN[Parametrizacion].[DetalleJerarquiaOrg] AS PDJ ON PDJ.idHijo = PJO.idHijo"+
                         " left JOIN Parametrizacion.Area as Parea on Parea.IdArea = PDJ.IdArea"+
                         " GROUP BY PCR.NombreClasificacionRiesgo, PRI.NombreRiesgoInherente,RiesgoResidual.NombreRiesgoInherente" +
                         " ORDER BY PCR.NombreClasificacionRiesgo"///*,RR.IdRiesgo", RR.IdRiesgo, RR.CodRiesgo"+ ", PRI.ValorRiesgoInherente, RiesgoResidual.ValorRiesgoInherente,
                    );*/
                /*strConsulta = string.Format("SELECT PCR.NombreClasificacionRiesgo as ClasificacionRiesgo,count(RR.IdRiesgo) as cantRiesgo, PRI.NombreRiesgoInherente as RiesgoInherente " +
"FROM  Parametrizacion.ClasificacionRiesgo AS PCR "+
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo "+
"LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto "+
"GROUP BY PCR.NombreClasificacionRiesgo, PRI.NombreRiesgoInherente ORDER BY PCR.NombreClasificacionRiesgo");*/
                /*strConsulta = string.Format("SELECT PCR.NombreClasificacionRiesgo, SUM( isnull(PRI.IdProbabilidad,0)) as IdProbabilidad,SUM(isnull(PRI.IdImpacto,0)) as IdImpacto " +
"FROM  Parametrizacion.ClasificacionRiesgo AS PCR "+
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo "+
"LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto "+
"GROUP BY PCR.NombreClasificacionRiesgo ORDER BY PCR.NombreClasificacionRiesgo");*/
                if (opc == 1)
                {
                    strConsulta = string.Format("SELECT PCR.NombreClasificacionRiesgo,COUNT(RR.IdRiesgo) as cant, isnull(PRI.IdProbabilidad,0) as IdProbabilidad,isnull(PRI.IdImpacto,0) as IdImpacto " +
                    "FROM  Parametrizacion.ClasificacionRiesgo AS PCR " +
                    "LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto " +
                    "GROUP BY PCR.NombreClasificacionRiesgo,PRI.IdProbabilidad,PRI.IdImpacto ORDER BY PCR.NombreClasificacionRiesgo");
                }
                if(opc == 2)
                {
                    strConsulta = string.Format("SELECT RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo,COUNT(RR.IdRiesgo) as cant,RR.CodRiesgo,RCtrl.IdControl,PRI.IdProbabilidad,PRI.IdImpacto,LTRIM(RTRIM(ISNULL(RCtrl.efectividad, ''))) efectividad,LTRIM(RTRIM(ISNULL(RCtrl.operatividad, ''))) operatividad , (SELECT count(1)" +
                        "FROM[Riesgos].[ControlesRiesgo] where IdRiesgo = RR.IdRiesgo) as cantRiesgo " +
                    "FROM Riesgos.Riesgo RR "+
                    "LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo "+
                    "LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto "+
                    "LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo "+
                    "LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl "+
                    "LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad "+
                    "GROUP BY RR.IdClasificacionRiesgo, RCtrl.IdControl, PRI.IdProbabilidad, PRI.IdImpacto, RCtrl.efectividad, RCtrl.operatividad, PCR.NombreClasificacionRiesgo,RR.IdRiesgo,RR.CodRiesgo " +
                    ",PRI.ValorRiesgoInherente " +
                    "ORDER BY RR.IdClasificacionRiesgo,PRI.ValorRiesgoInherente");/**PRI.IdProbabilidad,PRI.IdImpacto,**/
                }
                if(opc == 3)
                {
                    strConsulta = string.Format("SELECT RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo,COUNT(RR.IdRiesgo) as cant,RR.CodRiesgo,RCtrl.IdControl,PRI.IdProbabilidad,PRI.IdImpacto,LTRIM(RTRIM(ISNULL(RCtrl.efectividad, ''))) efectividad,LTRIM(RTRIM(ISNULL(RCtrl.operatividad, ''))) operatividad , (SELECT count(1)" +
                        "FROM[Riesgos].[ControlesRiesgo] where IdRiesgo = RR.IdRiesgo) as cantRiesgo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual " +
                    "FROM Riesgos.Riesgo RR " +
                    "LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad  AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto " +
                    "LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo " +
                    "LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl " +
                    "LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "GROUP BY RR.IdClasificacionRiesgo, RCtrl.IdControl, PRI.IdProbabilidad, PRI.IdImpacto, RCtrl.efectividad, RCtrl.operatividad, PCR.NombreClasificacionRiesgo,RR.IdRiesgo,RR.CodRiesgo " +
                    ",RiesgoResidual.NombreRiesgoInherente,RiesgoResidual.ValorRiesgoInherente " +
                    "ORDER BY RR.IdClasificacionRiesgo,RiesgoResidual.ValorRiesgoInherente");
                }if(opc == 4)
                {
                    strConsulta = string.Format("SELECT SUBSTRING(RR.CodRiesgo,1, CHARINDEX('-', RR.CodRiesgo) -1) as RiesgoControl,RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo,COUNT(RR.IdRiesgo) as cant,RR.CodRiesgo,RCtrl.IdControl,PRI.IdProbabilidad,PRI.IdImpacto,LTRIM(RTRIM(ISNULL(RCtrl.efectividad, ''))) efectividad,LTRIM(RTRIM(ISNULL(RCtrl.operatividad, ''))) operatividad , (SELECT count(1)" +
                        "FROM[Riesgos].[ControlesRiesgo] where IdRiesgo = RR.IdRiesgo) as cantRiesgo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual " +
                    "FROM Riesgos.Riesgo RR " +
                    "LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad  AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto " +
                    "LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo " +
                    "LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl " +
                    "LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "GROUP BY RR.IdClasificacionRiesgo, RCtrl.IdControl, PRI.IdProbabilidad, PRI.IdImpacto, RCtrl.efectividad, RCtrl.operatividad, PCR.NombreClasificacionRiesgo,RR.IdRiesgo,RR.CodRiesgo " +
                    ",RiesgoResidual.NombreRiesgoInherente,RiesgoResidual.ValorRiesgoInherente,PRI.ValorRiesgoInherente  " +
                    "ORDER BY SUBSTRING(RR.CodRiesgo,1, CHARINDEX('-', RR.CodRiesgo) -1), PRI.ValorRiesgoInherente");
                    //"ORDER BY SUBSTRING(RR.CodRiesgo,1, CHARINDEX('-', RR.CodRiesgo) -1) ,RR.IdClasificacionRiesgo,RiesgoResidual.ValorRiesgoInherente");
                }
                if(opc == 5)
                {
                    strConsulta = string.Format("SELECT SUBSTRING(RR.CodRiesgo,1, CHARINDEX('-', RR.CodRiesgo) -1) as RiesgoControl,RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo,COUNT(RR.IdRiesgo) as cant,RR.CodRiesgo,RCtrl.IdControl,PRI.IdProbabilidad,PRI.IdImpacto,LTRIM(RTRIM(ISNULL(RCtrl.efectividad, ''))) efectividad,LTRIM(RTRIM(ISNULL(RCtrl.operatividad, ''))) operatividad , (SELECT count(1)" +
                        "FROM[Riesgos].[ControlesRiesgo] where IdRiesgo = RR.IdRiesgo) as cantRiesgo,LTRIM(RTRIM(ISNULL(RiesgoResidual.NombreRiesgoInherente, ''))) RiesgoResidual " +
                    "FROM Riesgos.Riesgo RR " +
                    "LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto " +
                    "LEFT JOIN Parametrizacion.RiesgoInherente RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad  AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto " +
                    "LEFT JOIN Riesgos.ControlesRiesgo RCR ON RR.IdRiesgo = RCR.IdRiesgo " +
                    "LEFT JOIN Riesgos.Control RCtrl ON RCR.IdControl = RCtrl.IdControl " +
                    "LEFT JOIN Parametrizacion.Periodicidad PPer ON PPer.IdPeriodicidad = RCtrl.IdPeriodicidad " +
                    "GROUP BY RR.IdClasificacionRiesgo, RCtrl.IdControl, PRI.IdProbabilidad, PRI.IdImpacto, RCtrl.efectividad, RCtrl.operatividad, PCR.NombreClasificacionRiesgo,RR.IdRiesgo,RR.CodRiesgo " +
                    ",RiesgoResidual.NombreRiesgoInherente,RiesgoResidual.ValorRiesgoInherente,PRI.ValorRiesgoInherente  " +
                    "ORDER BY SUBSTRING(RR.CodRiesgo,1, CHARINDEX('-', RR.CodRiesgo) -1), RiesgoResidual.NombreRiesgoInherente");
                }
                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCantidadRiesgoInherente(ref DataTable dtCaracOut, ref string strErrMsg, string Riesgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PCR.NombreClasificacionRiesgo,count(RR.IdRiesgo) as cantRiesgo "+
"FROM  Parametrizacion.ClasificacionRiesgo AS PCR "+
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo "+
"LEFT JOIN Parametrizacion.RiesgoInherente AS PRI ON PRI.IdProbabilidad = RR.IdProbabilidad AND PRI.IdImpacto = RR.IdImpacto "+
"where PCR.NombreClasificacionRiesgo = '{0}' "+
"GROUP BY PCR.NombreClasificacionRiesgo ORDER BY PCR.NombreClasificacionRiesgo" , Riesgo);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCantidadRiesgoPropabilidad(ref DataTable dtCaracOut, ref string strErrMsg, string Riesgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT count(RR.IdRiesgo) as cantRiesgo FROM  Parametrizacion.ClasificacionRiesgo AS PCR " +
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
"LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad "+
"AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto  " +
"where RiesgoResidual.NombreRiesgoInherente = '{0}' and PCR.NombreClasificacionRiesgo = '{1}'" +
" GROUP BY PCR.NombreClasificacionRiesgo,RiesgoResidual.NombreRiesgoInherente" +
                         " ORDER BY PCR.NombreClasificacionRiesgo", Riesgo, NombreClasificacion);*/
                    strConsulta = string.Format("SELECT SUM( isnull(RiesgoResidual.IdProbabilidad,0)) as IdProbabilidad "+
"FROM  Parametrizacion.ClasificacionRiesgo AS PCR "+
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo "+
"LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RiesgoResidual.IdProbabilidad = RR.IdProbabilidadResidual AND RiesgoResidual.IdImpacto = RR.IdImpacto "+
"where PCR.NombreClasificacionRiesgo = '{0}' "+
"GROUP BY PCR.NombreClasificacionRiesgo ORDER BY PCR.NombreClasificacionRiesgo", Riesgo);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCantidadRiesgoImpacto(ref DataTable dtCaracOut, ref string strErrMsg, string Riesgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT count(RR.IdRiesgo) as cantRiesgo FROM  Parametrizacion.ClasificacionRiesgo AS PCR " +
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
"LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RR.IdProbabilidadResidual = RiesgoResidual.IdProbabilidad "+
"AND RR.IdImpactoResidual = RiesgoResidual.IdImpacto  " +
"where RiesgoResidual.NombreRiesgoInherente = '{0}' and PCR.NombreClasificacionRiesgo = '{1}'" +
" GROUP BY PCR.NombreClasificacionRiesgo,RiesgoResidual.NombreRiesgoInherente" +
                         " ORDER BY PCR.NombreClasificacionRiesgo", Riesgo, NombreClasificacion);*/
                strConsulta = string.Format("SELECT SUM(isnull(RiesgoResidual.IdImpacto,0)) as IdImpacto " +
"FROM  Parametrizacion.ClasificacionRiesgo AS PCR " +
"LEFT JOIN Riesgos.Riesgo AS RR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
"LEFT JOIN Parametrizacion.RiesgoInherente AS RiesgoResidual ON RiesgoResidual.IdProbabilidad = RR.IdProbabilidadResidual AND RiesgoResidual.IdImpacto = RR.IdImpacto " +
"where PCR.NombreClasificacionRiesgo = '{0}' " +
"GROUP BY PCR.NombreClasificacionRiesgo ORDER BY PCR.NombreClasificacionRiesgo", Riesgo);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los perfiles del riesgo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCantidadTotalRiesgos(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("select count(IdRiesgo) as cantTotalRiesgo from Riesgos.Riesgo");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad total del resgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarRiesgoInherente(ref DataTable dtCaracOut, ref string strErrMsg, int probabilidad, int impacto)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("select NombreRiesgoInherente from Parametrizacion.RiesgoInherente "+
                "where IdProbabilidad = {0} and IdImpacto = {1}", probabilidad, impacto);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad total del resgos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarValorRiesgoInherente(ref DataTable dtCaracOut, ref string strErrMsg, int probabilidad, int impacto)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("select ValorRiesgoInherente from Parametrizacion.RiesgoInherente " +
                "where IdProbabilidad = {0} and IdImpacto = {1}", probabilidad, impacto);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad total del resgos. [{0}]", ex.Message);
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