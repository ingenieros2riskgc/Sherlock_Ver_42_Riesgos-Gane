using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoRecomendacionesDTO
    {
        private int _IdAuditoria;
        private string _Tema;
        private string _FechaRegistro;
        private int _MesDiff;
        private string _Hallazgo;
        private int _IdDependencia;
        private string _Responsable;
        private string _Observaciones;
        private int _IdNivelRiesgo;
        private string _Riesgo;
        private DateTime _FechaSeguimiento;
        private string _Seguimiento;

        public int intIdAuditoria
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public string strTema
        {
            get { return _Tema; }
            set { _Tema = value; }
        }
        public string strFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intMesDiff
        {
            get { return _MesDiff; }
            set { _MesDiff = value; }
        }
        public string strHallazgo
        {
            get { return _Hallazgo; }
            set { _Hallazgo = value; }
        }
        public int intIdDependencia
        {
            get { return _IdDependencia; }
            set { _IdDependencia = value; }
        }
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }
        public int intIdNivelRiesgo
        {
            get { return _IdNivelRiesgo; }
            set { _IdNivelRiesgo = value; }
        }
        public string strRiesgo
        {
            get { return _Riesgo; }
            set { _Riesgo = value; }
        }
        public DateTime dtFechaSeguimiento
        {
            get { return _FechaSeguimiento; }
            set { _FechaSeguimiento = value; }
        }
        public string strSeguimiento
        {
            get { return _Seguimiento; }
            set { _Seguimiento = value; }
        }
        public clsSeguimientoRecomendacionesDTO() { }
    }
}