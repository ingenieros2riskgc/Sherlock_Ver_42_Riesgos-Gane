using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsArchivosAuditoriaDAL
    {
        public bool Guardar(string nombrearchivo, int length, byte[] archivo, string IdRegistro, ref string strErrMsg, string extension, 
            int TipoArchivo, string path, int idHallazgo)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInfo = new DataTable();
            int Secuencia = 0;
            #endregion Vars
            try
            {
                if(TipoArchivo == 1)
                {
                    strConsulta = string.Format("SELECT count([IdArchivoAuditoria]) as secuencia FROM [Auditoria].[ArchivosAuditoria] " +
                                                    "where [IdAuditoria] = {0} and TipoArchivo = 1", IdRegistro);
                    cDatabase.conectar();
                    dtInfo = cDatabase.ejecutarConsulta(strConsulta);
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {
                            Secuencia = Convert.ToInt32(dtInfo.Rows[rows]["secuencia"].ToString().Trim()) + 1;
                        }
                    }
                }else
                {
                    Secuencia = 0;
                }
                
                strConsulta = string.Format("INSERT INTO [Auditoria].[ArchivosAuditoria] " +
                    "([IdAuditoria],[TipoArchivo],[Path],[length],[extension],[nombre],[archivo],[Secuencia],[idHallazgo]) " +
                    "VALUES ({0}, {1},'{2}', {3}, '{4}','{5}',@PdfData,{6},{7})",
                    IdRegistro,TipoArchivo, path, length, extension, nombrearchivo, Secuencia, idHallazgo);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta, archivo);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el archivo. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;

        }
        public DataTable mtdGetPathFile(ref string strErrMsg, int IdAuditoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT AA.[IdAuditoria],AAA.nombre, AAA.archivo, AAA.TipoArchivo"+
                    " FROM[Auditoria].[Auditoria] as AA"+
                    " INNER JOIN[Auditoria].[ArchivosAuditoria] as AAA on AAA.IdAuditoria = AA.IdAuditoria"+
                    " where AA.IdAuditoria = {0} and AAA.TipoArchivo = 2", IdAuditoria);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el archivo adjunto. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public int mtdGetSecuencia(ref string strErrMsg, int IdAuditoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtInfo = new DataTable();
            int Secuencia = 0;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT count([IdArchivoAuditoria]) as secuencia FROM [Auditoria].[ArchivosAuditoria] " +
                                "where [IdAuditoria] = {0} and TipoArchivo = 1", IdAuditoria);
                cDatabase.conectar();
                dtInfo = cDatabase.ejecutarConsulta(strConsulta);
                if (dtInfo.Rows.Count > 0)
                {
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        Secuencia = Convert.ToInt32(dtInfo.Rows[rows]["secuencia"].ToString().Trim());
                    }
                }

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la secuencia del archivo adjunto. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return Secuencia;
        }
    }
}