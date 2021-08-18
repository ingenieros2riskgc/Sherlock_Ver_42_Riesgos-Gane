using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsPlanificacionBLL
    {
        /// <summary>
        /// Metodo para replicar los registros de la planeacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdReplicarPlanificacion(int IdPlan, ref string strErrMsg, int IdUsuario, int IdRegistros, int NewPlan)
        {
            bool booResult = false;
            clsPlanificacionDAL cDtAuditoria = new clsPlanificacionDAL();

            booResult = cDtAuditoria.mtdReplicarPlanificacion(IdPlan, ref strErrMsg, IdUsuario, IdRegistros, NewPlan);

            return booResult;
        }
        /// <summary>
        /// Metodo para replicar los registros de la planeacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdReplicarPlanificacion(int IdPlan, ref string strErrMsg, int IdUsuario)
        {
            bool booResult = false;
            clsPlanificacionDAL cDtAuditoria = new clsPlanificacionDAL();

            booResult = cDtAuditoria.mtdReplicarPlanificacionNew(IdPlan, ref strErrMsg, IdUsuario);

            return booResult;
        }
    }
}