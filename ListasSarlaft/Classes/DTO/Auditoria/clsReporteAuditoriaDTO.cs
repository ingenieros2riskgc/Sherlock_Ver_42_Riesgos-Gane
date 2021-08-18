using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteAuditoriaDTO
    {
        private int _IdDependencia;
        private string _NombreDependencia;
        private int _IdProceso;
        private string _NombreProceso;
        private int _IdTipoProceso;
        private string _Tema;
        private string _Objetivo;
        private string _NombreObjetivo;
        private string _DescripcionObjetivo;
        private string _Estado;
        private int _IdGrupoAuditoria;
        private string _GrupoAuditoria;
        private string _Periodicidad;
        private string _Mes;
        private int _IdMesExe;
        private string _MesExe;
        private string _SemanaExe;
        private string _Especial;
        private string _FechaInicio;
        private string _FechaCierre;
        public int intIdDependencia
        {
            get { return _IdDependencia; }
            set { _IdDependencia = value; }
        }
        public string strNombreDependencia
        {
            get { return _NombreDependencia; }
            set { _NombreDependencia = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public string strNombreProces
        {
            get { return _NombreProceso; }
            set { _NombreProceso = value; }
        }
        public int intIdTipoProceso
        {
            get { return _IdTipoProceso; }
            set { _IdTipoProceso = value; }
        }
        public string strTema
        {
            get { return _Tema; }
            set { _Tema = value; }
        }
        public string strEstado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string strObjetivo
        {
            get { return _Objetivo; }
            set { _Objetivo = value; }
        }
        public string strNombreObjetivo
        {
            get { return _NombreObjetivo; }
            set { _NombreObjetivo = value; }
        }
        public string strDescripcionObjetivo
        {
            get { return _DescripcionObjetivo; }
            set { _DescripcionObjetivo = value; }
        }
        public int intIdGrupoAuditoria
        {
            get { return _IdGrupoAuditoria; }
            set { _IdGrupoAuditoria = value; }
        }
        public string strGrupoAuditoria
        {
            get { return _GrupoAuditoria; }
            set { _GrupoAuditoria = value; }
        }
        public string strPeriodicidad
        {
            get { return _Periodicidad; }
            set { _Periodicidad = value; }
        }
        public string strMes
        {
            get { return _Mes; }
            set { _Mes = value; }
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
        public string strEspecial
        {
            get { return _Especial; }
            set { _Especial = value; }
        }
        public string strMesExe
        {
            get { return _MesExe; }
            set { _MesExe = value; }
        }
        public clsReporteAuditoriaDTO() { }
    }
    public class clsReporteConsolidadoDTO
    {
        public clsReporteConsolidadoDTO() { }
        private string _Planeacion;
        private int _IdRegistro;
        private string _Tema;
        private double _Realizadas;
        private double _Programadas;
        private double _Cumplimiento;
        private string _FechaInicio;
        private string _FechaCierre;
        public string strPlaneacion
        {
            get { return _Planeacion; }
            set { _Planeacion = value; }
        }
        public int intIdRegistro
        {
            get { return _IdRegistro; }
            set { _IdRegistro = value; }
        }
        public string strTema
        {
            get { return _Tema; }
            set { _Tema = value; }
        }
        public double dbRealizadas
        {
            get { return _Realizadas; }
            set { _Realizadas = value; }
        }
        public double dbProgramadas
        {
            get { return _Programadas; }
            set { _Programadas = value; }
        }
        public double dbCumplimiento
        {
            get { return _Cumplimiento; }
            set { _Cumplimiento = value; }
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
    }
 }