using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DAL
{
    public class clsDALReportePerfilRiesgo
    {
        public bool mtdConsultarPerfilRiesgoInherente(ref DataTable dtCaracOut, ref string strErrMsg, int opc)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {


                strConsulta = string.Format("SELECT COUNT(RR.IdRiesgo) AS NumeroRegistros, RR.IdProbabilidad, RR.IdImpacto,RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo " +
                "FROM Riesgos.Riesgo RR   " +
                "LEFT JOIN Parametrizacion.ClasificacionRiesgo AS PCR ON RR.IdClasificacionRiesgo = PCR.IdClasificacionRiesgo " +
                "WHERE (RR.Anulado = 0) GROUP BY RR.IdProbabilidad, RR.IdImpacto,RR.IdClasificacionRiesgo,PCR.NombreClasificacionRiesgo " +
                "ORDER BY PCR.NombreClasificacionRiesgo ");


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
    }
}