using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsIndicadorRecomendacionesDtpoDAL
    {
        public bool mtdConsultarIndicadorRecomendacionesDtpo(ref DataTable dtCaracOut, ref string strErrMsg, string año,
            string fechaInicial, string FechaFinal, string planeacion, string IdArea)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                if (fechaInicial != "" && FechaFinal != "")
                {
                    strCondicion = string.Format("and (FechaRegistro BETWEEN CONVERT(datetime, '{0} 00:00', 120) AND CONVERT(datetime, '{1} 23:59', 120))", fechaInicial, FechaFinal);
                }
                if (planeacion != "0")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format(" and planeacion = '{0}'", planeacion);
                    else
                        strCondicion = string.Format(" and planeacion = '{0}'", planeacion);
                }
                if(IdArea != "" && IdArea != "0")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format(" and PA.IdArea = '{0}'", IdArea);
                    else
                        strCondicion = string.Format(" and PA.IdArea = '{0}'", IdArea);
                }
                /*strConsulta = string.Format("SELECT [IdIndicador],[Indicador],[MetodologiaNominador],[MetodologiaDenominador]" +
                    ",[Frecuencia],[Responsable],[Meta],ISNULL([Acumulado],0) as Acumulado,[NivelCumplimiento],ISNULL([AcumuladoDtpo],0) as AcumuladoDtpo" +
                    ",[UsuarioRegistro], LU.Usuario,[FechaRegistro]" +
                    " FROM [Auditoria].[AuditoriaIndicadores] as AAI" +
                    " INNER JOIN Listas.Usuarios as LU on LU.IdUsuario = AAI.UsuarioRegistro" +
                    " where AAI.año = '{0}' {1}", año, strCondicion);*/
                strConsulta = string.Format("SELECT [IdIndicador],[Indicador],[MetodologiaNominador],[MetodologiaDenominador]"+
                    ",[Frecuencia],[Responsable],[Meta], ISNULL([Acumulado], 0) as Acumulado,[NivelCumplimiento],ISNULL([AcumuladoDtpo],0) as AcumuladoDtpo"+
                    ",[UsuarioRegistro], LU.Usuario,AAI.[FechaRegistro],PA.NombreArea"+
                    " FROM [Auditoria].[AuditoriaIndicadores] as AAI "+
                    " INNER JOIN[Parametrizacion].[JerarquiaOrganizacional] as PJO on PJO.NombreHijo = AAI.Responsable "+
                    " INNER JOIN[Parametrizacion].[DetalleJerarquiaOrg] AS PDJO ON PDJO.idHijo = PJO.idHijo" +
                    " INNER JOIN[Parametrizacion].[Area] AS PA ON PA.IdArea = PDJO.IdArea"+
                    " INNER JOIN Listas.Usuarios as LU on LU.IdUsuario = AAI.UsuarioRegistro " +
                    " where AAI.año = '{0}' {1}", año, strCondicion);
                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los datos del reporte. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdConsultarAreasResponsables(ref DataTable dtCaracOut, ref string strErrMsg, string año)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT DISTINCT(PA.IdArea) as IdArea, PA.NombreArea as Area FROM [Auditoria].[AuditoriaIndicadores] as AAI" +
                    " INNER JOIN[Parametrizacion].[JerarquiaOrganizacional] as PJO on PJO.NombreHijo = AAI.Responsable" +
                    " INNER JOIN[Parametrizacion].[DetalleJerarquiaOrg] AS PDJO ON PDJO.idHijo = PJO.idHijo"+
                    " INNER JOIN[Parametrizacion].[Area] AS PA ON PA.IdArea = PDJO.IdArea"+
                    " where YEAR(AAI.FechaRegistro) = {0}", año);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los departamentos. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public int mtdGetValueRecomendacionesImplementadasDtpo(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses, string IdArea)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            int value = 0;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT count([IdRecomendacion]) as countRecomendacion  FROM [Auditoria].[Recomendacion]" +
                " where YEAR(FechaRegistro) = '{0}' and MONTH(FechaRegistro) in ({1}) and Estado='Implementada' and IdDependenciaRespuesta={2}", año, meses, IdArea);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                foreach (DataRow row in dtCaracOut.Rows)
                {
                    value = Convert.ToInt32(row["countRecomendacion"].ToString());
                }
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad de recomendaciones. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return value;
        }
        public int mtdGetValueRecomendacionesRealizadaDtpo(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses, string IdArea)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            int value = 0;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT count([IdRecomendacion]) as countRecomendacion  FROM[Auditoria].[Recomendacion]" +
                " where YEAR(FechaRegistro) = '{0}' and MONTH(FechaRegistro) in ({1}) and Estado='Realizada' and IdDependenciaRespuesta={2}", año, meses, IdArea);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                foreach (DataRow row in dtCaracOut.Rows)
                {
                    value = Convert.ToInt32(row["countRecomendacion"].ToString());
                }
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad de recomendaciones. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return value;
        }
        public bool mtdUpdateAcumuladoDtpo(int IdIndicador, double acumulado, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[AuditoriaIndicadores] SET [AcumuladoDtpo] = {1} WHERE IdIndicador = {0}", IdIndicador, acumulado);


                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el acumulado. [{0}]", ex.Message);
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