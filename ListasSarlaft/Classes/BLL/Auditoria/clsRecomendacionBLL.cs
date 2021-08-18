using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes.DAL.Auditoria;
namespace ListasSarlaft.Classes.BLL.Auditoria
{
    public class clsRecomendacionBLL
    {
        public Boolean mtdCalculaEdadRecomendacion(ref string strErrMsg, string IdHallazgo)
        {
            clsRecomendacionDAL dbRecomendacion = new clsRecomendacionDAL();
            Boolean flag = false;
            DataTable dtInfo = new DataTable();
            dtInfo = dbRecomendacion.mtdGetRecomendaciones(IdHallazgo);
            if(dtInfo.Rows.Count > 0)
            {
                DateTime dtFecha = new DateTime();
                foreach(DataRow row in dtInfo.Rows)
                {
                    dtFecha = Convert.ToDateTime(row["FechaRegistro"].ToString());
                    DateTime dtHoy = DateTime.Now;
                    int Result = (dtHoy.Month - dtFecha.Month);
                    flag = dbRecomendacion.mtdUpdateRecomendacionEdad(ref strErrMsg, IdHallazgo, Result);
                }
            }
            return flag;
        }
    }
}