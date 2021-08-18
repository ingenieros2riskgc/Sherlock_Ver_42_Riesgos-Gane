using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDatos;
using clsDTO;
using ListasSarlaft.Classes.DTO.Calidad;

namespace ListasSarlaft.Classes
{
    public class clsDtVerCaracterizacion: IDisposable
    {
        cDataBase cDataBase = new cDataBase();
        public bool mtdConsultarVerCaracterizacion(clsVerCaracterizacion objCaracIn, ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdProceso", SqlDbType = SqlDbType.Int, Value = objCaracIn.intIdProceso}
                };
                dtCaracOut = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[VerCaracterizacion]", parametros);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            return booResult;
        }

        public DataTable VerCamposCaracterizacion(clsVerCaracterizacion obj)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdProceso", SqlDbType = SqlDbType.Int, Value = obj.intIdProceso}
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[VerCaracterizacion]", parametros);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool mtdConsultarVerCaracterizacionDetalle(clsVerCaracterizacion objCaracIn, ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [DescripcionEntrada],[Proveedor],[DescripcionActividad],[CargoResponsable],[DescripcionSalida],[Cliente],[DescripcionProcedimiento]" +
                    "FROM [dbo].[vwVerCaracterizacion] " +
                    "where IdProceso = {0} order by [DescripcionEntrada],[Proveedor],[DescripcionActividad],[CargoResponsable],[DescripcionSalida],[Cliente],[DescripcionProcedimiento]",
                    objCaracIn.intIdProceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarVerCaracterizacionIndicador(clsVerCaracterizacion objCaracIn, ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [NombreIndicador],[Descripcion],ISNULL([IdIndicador], '')IdIndicador,ISNULL([Codigo],'')Codigo,ISNULL([NombreRiesgo],'')NombreRiesgo,ISNULL([DescripcionRiesgo],'')DescripcionRiesgo,ISNULL([IdRiesgo],'')IdRiesgo,ISNULL([CodigoControl],'')CodigoControl,ISNULL([NombreControl],'')NombreControl" +
                    " FROM [dbo].[vwVerCaracterizacionIndicadorRiesgo] " +
                    "where IdProceso = {0} order by CodigoControl",
                    objCaracIn.intIdProceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public System.Data.DataSet mtdConsultarVerCaracterizacionRPT(clsVerCaracterizacion objCaracIn, ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            bool booResult = false;
            //DataTable dtInformacion = new DataTable();
            System.Data.DataSet dsInformacion = new System.Data.DataSet();
            string strConsulta = string.Empty;
            SqlParameter[] objSqlParams = new SqlParameter[1];
            #endregion Vars

            try
            {
                objSqlParams[0] = new SqlParameter("@IdProceso", objCaracIn.intIdProceso);
                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_getHeadCaracterizacion", "DataSet2", objSqlParams);
                
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }
        
        public DataTable mtdHeaderCaracterizacion(int IdProceso, ref DataTable informacion, string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT ProCarac.[Id],ProMacro.[Nombre],ProMacro.[Objetivo] " +
                    "FROM [Procesos].[tblCaracterizacion] as ProCarac " +
                    "INNER JOIN [Procesos].[Macroproceso] as ProMacro on ProCarac.[IdProceso] = ProMacro.IdMacroProceso " +
                    "where ProCarac.idProceso = {0} ",
                    IdProceso);

                cDataBase.conectar();
                informacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return informacion;
        }

        // Documentos asociados caracterización
        public List<DocumentosCaracterizacion> ConsultarDocumentos(int idMacroProceso)
        {
            try
            {
                List<DocumentosCaracterizacion> lst = new List<DocumentosCaracterizacion>();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdMacroProceso", SqlDbType = SqlDbType.Int, Value = idMacroProceso }
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarDocumentos]", parametros);
                if (dt != null && dt.Rows.Count > 0 )
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        lst.Add(new DocumentosCaracterizacion()
                        {
                            NombreDocumento = Row["NombreDocumento"].ToString(),
                            TipoDocumento = Row["TipoDocumento"].ToString(),
                            CodigoDocumento = Row["CodigoDocumento"].ToString(),
                            FechaImplementacion = Row["FechaImplementacion"].ToString(),
                            NombreResponsable = Row["NombreResponsable"].ToString(),
                            CargoResponsable = Row["CArgoResponsable"].ToString(),
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {

        }
    }
}