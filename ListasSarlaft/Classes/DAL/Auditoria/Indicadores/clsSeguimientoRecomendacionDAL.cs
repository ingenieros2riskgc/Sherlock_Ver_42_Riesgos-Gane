using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DAL.Auditoria.Indicadores
{
    public class clsSeguimientoRecomendacionDAL
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private Object thisLock = new Object();
        cControl cControl = new cControl();
        public DataTable mdtIndicadorSeguimientoRecomendacion(int idPlaneacion, ref string mensaje)
        {
            DataTable dtInformacion = new DataTable();
            // Se pasa la condicion a la consulta del procedimiento almacenado
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@idPlaneacion", SqlDbType = SqlDbType.VarChar, Value =  idPlaneacion }
                        };
                dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[dbo].[SP_HallazgosRecomendacionesAuditoria]", parametros);

            }catch(Exception ex)
            {
                mensaje = "Error en la consulta: " + ex.Message;
            }


            return dtInformacion;
        }
        public DataTable mdtIndicadorProgramaAuditoria(int idPlaneacion, ref string mensaje)
        {
            DataTable dtInformacion = new DataTable();
            // Se pasa la condicion a la consulta del procedimiento almacenado
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                        {
                            new SqlParameter() { ParameterName = "@idPlaneacion", SqlDbType = SqlDbType.VarChar, Value =  idPlaneacion }
                        };
                dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[dbo].[SP_IndicadoresProgramaAuditoria]", parametros);

            }
            catch (Exception ex)
            {
                mensaje = "Error en la consulta: " + ex.Message;
            }


            return dtInformacion;
        }
    }
}