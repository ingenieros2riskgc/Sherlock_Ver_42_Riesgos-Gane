using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteIndicadoresDTO
    {
        private string _Ano;
        private int _mes;
        private string _NombreMes;
        private double _Realizadas;
        private double _Programadas;
        private double _Cumplimiento;
        public string strAno
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        public int intMes
        {
            get { return _mes; }
            set { _mes = value; }
        }
        public string strNombreMes
        {
            get {return _NombreMes; }
            set { _NombreMes = value; }
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
        public clsReporteIndicadoresDTO() { }
    }
}