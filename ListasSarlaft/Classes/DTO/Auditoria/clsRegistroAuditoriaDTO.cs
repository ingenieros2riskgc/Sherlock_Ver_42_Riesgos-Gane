using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsRegistroAuditoriaDTO
    {
        private string _codigoAuditoria;
        private int _IdRegistroAuditoria;
        private int _IdAuditoria;
        private int _IdPlaneacion;
        private string _Tema;
        private int _IdEstandar;
        private string _Estandar;
        private string _Tipo;
        private int _IdDependencia;
        private string _NombreDP;
        private int _IdProceso;
        private string _FechaRegistro;
        private int _IdUsuario;
        private string _User;
        private int _IdEmpresa;
        private string _Estado;
        private string _Encabezado;
        private string _Metodologia;
        private string _Objetivo;
        private string _Recursos;
        private string _Alcance;
        private string _NivelImportancia;
        private int _IdDetalle_TipoNaturaleza;
        private string _FechaInicio;
        private string _FechaCierre;
        private string _Conclusion;
        private string _Obsevaciones;
        private string _Especial;
        private int _Periodicidad;
        private int _IdMesExe;
        private string _SemanaExe;
        private int _IdArea;
        public int intIdArea
        {
            get { return _IdArea; }
            set { _IdArea = value; }
        }
        public string strCodigoAuditoria
        {
            get { return _codigoAuditoria; }
            set { _codigoAuditoria = value; }
        }
        public int intIdRegistroAuditoria
        {
            get { return _IdRegistroAuditoria; }
            set { _IdRegistroAuditoria = value; }
        }
        public int intIdAuditoria
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public int intIdPlaneacion
        {
            get { return _IdPlaneacion; }
            set { _IdPlaneacion = value; }
        }
        public int intIdEstandar
        {
            get { return _IdEstandar; }
            set { _IdEstandar = value; }
        }
        public string strEstandar
        {
            get { return _Estandar; }
            set { _Estandar = value; }
        }
        public int intIdDependencia
        {
            get { return _IdDependencia; }
            set { _IdDependencia = value; }
        }
        public string strNombreDP
        {
            get { return _NombreDP; }
            set { _NombreDP = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string strUser
        {
            get { return _User; }
            set { _User = value; }
        }
        public int intIdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        public int intIdDetalle_TipoNaturaleza
        {
            get { return _IdDetalle_TipoNaturaleza; }
            set { _IdDetalle_TipoNaturaleza = value; }
        }
        public string strTema
        {
            get { return _Tema; }
            set { _Tema = value; }
        }
        public string strTipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public string strFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public string strEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string strEncabezado
        {
            get { return _Encabezado; }
            set { _Encabezado = value; }
        }
        public string strMetodologia
        {
            get { return _Metodologia; }
            set { _Metodologia = value; }
        }
        public string strObjetivo
        {
            get { return _Objetivo; }
            set { _Objetivo = value; }
        }
        public string strRecursos
        {
            get { return _Recursos; }
            set { _Recursos = value; }
        }
        public string strAlcance
        {
            get { return _Alcance; }
            set { _Alcance = value; }
        }
        public string strNivelImportancia
        {
            get { return _NivelImportancia; }
            set { _NivelImportancia = value; }
        }
        public string strFechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }
        public string strFechaCierre
        {
            get { return _FechaCierre; }
            set { _FechaCierre = value; }
        }
        public string strConclusion
        {
            get { return _Conclusion; }
            set { _Conclusion = value; }
        }
        public string strObsevaciones
        {
            get { return _Obsevaciones; }
            set { _Obsevaciones = value; }
        }
        public string strEspecial
        {
            get { return _Especial; }
            set { _Especial = value; }
        }
        public int intIdMesExe
        {
            get { return _IdMesExe; }
            set { _IdMesExe = value; }
        }
        public string strSemanaExe
        {
            get { return _SemanaExe; }
            set { _SemanaExe = value; }
        }
        public int intPeriodicidad
        {
            get { return _Periodicidad; }
            set { _Periodicidad = value; }
        }
        public clsRegistroAuditoriaDTO() { }
    }
}