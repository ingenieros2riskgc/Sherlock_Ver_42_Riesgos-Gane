using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsIndicadorRecomendacionesDAL
    {
        public bool mtdConsultarIndicadorRecomendaciones(ref DataTable dtCaracOut, ref string strErrMsg, string año,
            string fechaInicial, string FechaFinal, string planeacion)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                if(fechaInicial != "" && FechaFinal != "")
                {
                    strCondicion = string.Format("and (FechaRegistro BETWEEN CONVERT(datetime, '{0} 00:00', 120) AND CONVERT(datetime, '{1} 23:59', 120))", fechaInicial,FechaFinal);
                }
                if(planeacion != "0")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format( " and planeacion = '{0}'", planeacion);
                    else
                        strCondicion = string.Format(" and planeacion = '{0}'", planeacion);
                }
                strConsulta = string.Format("SELECT [IdIndicador],[Indicador],[MetodologiaNominador],[MetodologiaDenominador]"+
                    ",[Frecuencia],[Responsable],[Meta],[Acumulado],[NivelCumplimiento]"+
                    ",[UsuarioRegistro], LU.Usuario,[FechaRegistro]"+
                    " FROM [Auditoria].[AuditoriaIndicadores] as AAI"+
                    " INNER JOIN Listas.Usuarios as LU on LU.IdUsuario = AAI.UsuarioRegistro"+
                    " where AAI.año = '{0}' {1}",año, strCondicion);

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
        public int mtdGetValueRecomendacionesImplementadas(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses)
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
                /*strConsulta = string.Format("SELECT count([IdRecomendacion]) as countRecomendacion  FROM[Auditoria].[Recomendacion]"+
                " where YEAR(FechaRegistro) = '{0}' and MONTH(FechaRegistro) in ({1}) and Estado='Implementada'", año, meses);*/
                strConsulta = string.Format("SELECT count(AR.[IdRecomendacion]) as countRecomendacion FROM [Auditoria].[Auditoria] AS AA "+
                    "INNER JOIN [Auditoria].[Hallazgo] AS AH ON AH.IdAuditoria = AA.IdAuditoria "+
                    "INNER JOIN [Auditoria].[Recomendacion] AS AR ON AR.IdHallazgo = AH.IdHallazgo "+
                    "where YEAR(AA.FechaRegistro) = '{0}' and MONTH(AA.FechaRegistro) in ({1}) and AA.Estado <> 'CUMPLIDA' and AA.Estado <> 'CERRADA'", año, meses);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                foreach(DataRow row in dtCaracOut.Rows)
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
        public int mtdGetValueRecomendacionesRealizada(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses)
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
                /*strConsulta = string.Format("SELECT count([IdRecomendacion]) as countRecomendacion  FROM[Auditoria].[Recomendacion]" +
                " where YEAR(FechaRegistro) = '{0}' and MONTH(FechaRegistro) in ({1}) and Estado='Realizada'", año, meses);*/
                strConsulta = string.Format("SELECT count(AR.[IdRecomendacion]) as countRecomendacion FROM [Auditoria].[Auditoria] AS AA " +
                    "INNER JOIN [Auditoria].[Hallazgo] AS AH ON AH.IdAuditoria = AA.IdAuditoria " +
                    "INNER JOIN [Auditoria].[Recomendacion] AS AR ON AR.IdHallazgo = AH.IdHallazgo " +
                    "where YEAR(AA.FechaRegistro) = '{0}' and MONTH(AA.FechaRegistro) in ({1}) and AA.Estado = 'CUMPLIDA' and AA.Estado = 'CERRADA'", año, meses);

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
        public bool mtdUpdateAcumulado(int IdIndicador, double acumulado, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Auditoria].[AuditoriaIndicadores] SET [Acumulado] = {1} WHERE IdIndicador = {0}",IdIndicador, acumulado);


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