using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLcalificaControl
    {
        // Diccionario que guarda los valores de acuerdo al DropdownList
        public static Dictionary<string, int> tempValores = new Dictionary<string, int>();
        public Boolean mtdCalificaControlMasico(List<cControlEntity> lstControles, ref string strErrMsg)
        {
            Boolean flag = false;
            try
            {
                
                clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
                clsDALCategoriaVariableControl cCategoria = new clsDALCategoriaVariableControl();
                clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
                cControl cControl = new cControl();
                clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
                foreach (cControlEntity control in lstControles)
                {
                    List<clsDTOControlxVariable> lstControlxVariable = new List<clsDTOControlxVariable>();
                    lstControlxVariable = cControlxVariable.mtdConsultarVariablexContol(ref lstControlxVariable, ref strErrMsg, control.IdControl);
                    //Se consulta en la tabla control el ID guardado para cada una de las variables
                    DataTable dt = cControl.SeleccionarCategoriasControl(control.IdControl);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            
                            // Agrega al diccionario los DropdownList
                            var _valor = tempValores.FirstOrDefault(x => x.Key == Row["DescripcionVariable"].ToString());
                            if (_valor.Key is null)
                                tempValores.Add(Row["DescripcionVariable"].ToString(), Convert.ToInt32(Row["IdCategoriaVariableControl"].ToString()));
                            else
                                tempValores[_valor.Key] = Convert.ToInt32(Row["IdCategoriaVariableControl"].ToString());
                        }
                        double total = 0;
                        // Se recorre el diccionario para hacer el calculo
                        foreach (var tempValor in tempValores)
                        {
                            string _tempVariable = tempValor.Key;
                            int tempCategoria = tempValor.Value;
                            double PorcentajeVariable = cPorcentaje.mtdConsultarPorcentajesxVariable(ref strErrMsg, ref _tempVariable);
                            //Se busca la calificacion de la categoria y se guarda en la variable IdCategoria
                            int PesoCategoria = cCategoria.mtdPesoCategoria(ref strErrMsg, tempCategoria);
                            double ValorCalificacion = 0;
                            ValorCalificacion = ValorCalificacion + (PorcentajeVariable * PesoCategoria);
                            ValorCalificacion = (ValorCalificacion / 100);
                            total = Math.Round(total + ValorCalificacion);
                        }
                        string cod = control.CodigoControl;
                        List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
                        lstCalificacion = cPorcentaje.mtdConsultarCalificacionControl(ref lstCalificacion, ref strErrMsg);
                        int IdCalificacionControl = 0;
                        if (lstCalificacion != null)
                        {
                            foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                            {
                                if (total >= objCalificacion.intLimiteInferior && total <= objCalificacion.intLimiteSuperior)
                                {
                                    IdCalificacionControl = objCalificacion.intIdCalificacionControl;
                                    break;
                                }
                            }
                        }
                        cControlEntity controlEntity = new cControlEntity();
                        // Se comienza a llenar el objeto control para hacer la inserción de datos
                        controlEntity.IdControl = control.IdControl;
                        controlEntity.NombreControl = control.NombreControl;
                        controlEntity.DescripcionControl = control.DescripcionControl;
                        controlEntity.ObjetivoControl = control.ObjetivoControl;
                        controlEntity.Responsable = control.Responsable;
                        controlEntity.IdPeriodicidad = control.IdPeriodicidad;
                        controlEntity.IdTest = control.IdTest;
                        controlEntity.IdCalificacionControl = IdCalificacionControl;
                        controlEntity.IdMitiga = control.IdMitiga;
                        controlEntity.IdUsuario = control.IdUsuario;
                        controlEntity.ResponsableEjecucion =control.ResponsableEjecucion;
                        controlEntity.Procedimiento = control.Procedimiento;
                        // Se guardan los SelectedValue de los Dropdownlist Dinámicos
                        controlEntity.IdClaseControl = control.IdClaseControl;
                        controlEntity.IdTipoControl = control.IdTipoControl;
                        controlEntity.IdResponsableExperiencia = control.IdResponsableExperiencia;
                        controlEntity.IdDocumentacion = control.IdDocumentacion;
                        controlEntity.IdResponsabilidad = control.IdResponsabilidad;
                        controlEntity.Variable6 = control.Variable6;
                        controlEntity.Variable7 = control.Variable7;
                        controlEntity.Variable8 = control.Variable8;
                        controlEntity.Variable9 = control.Variable9;
                        controlEntity.Variable10 = control.Variable10;
                        controlEntity.Variable11 = control.Variable11;
                        controlEntity.Variable12 = control.Variable12;
                        controlEntity.Variable13 = control.Variable13;
                        controlEntity.Variable14 = control.Variable14;
                        controlEntity.Variable15 = control.Variable15;
                        controlEntity.efectividad = mtdGetEfectividad(total);
                        controlEntity.operatividad = mtdGetValorPeriodicidad(control.IdPeriodicidad.ToString());
                        /*control.CodControlUser = control.CodControlUser;*/
                        int scope = 0;
                        scope = cControl.InsertControl(controlEntity);
                    }
                }
            }catch(Exception ex)
            {
                strErrMsg = "Error en el calculo: " + ex.Message;
            }
            finally
            {
                flag = true;
            }
            return flag;
        }
        public string mtdGetValorPeriodicidad(string periodicidad)
        {
            decimal valor = 0;
            string calificacion = string.Empty;
            DataTable dtInfo = new DataTable();
            cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
            dtInfo = cParametrizacionRiesgos.loadInfoPeriodicidadById(periodicidad);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    valor = Convert.ToDecimal(dtInfo.Rows[rows]["Valor"].ToString().Trim());
                }
                if (valor > 93)
                    calificacion = "Alta";
                if (valor > 61 && valor <= 93)
                    calificacion = "Media";
                if (valor > 35 && valor <= 61)
                    calificacion = "Baja";
                if (valor <= 35)
                    calificacion = "Muy Baja";
            }
            return calificacion;
        }
        public string mtdGetEfectividad(double total)
        {
            string efictividad = string.Empty;
            if (total > 93)
                efictividad = "Muy Fuerte";
            if (total > 61 && total <= 93)
                efictividad = "Fuerte";
            if (total > 35 && total <= 61)
                efictividad = "Moderada";
            if (total <= 35)
                efictividad = "Débil";
            return efictividad;
        }
    }
}