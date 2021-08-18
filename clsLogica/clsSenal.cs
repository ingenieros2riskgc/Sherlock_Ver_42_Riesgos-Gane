using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using clsDatos;
using clsDTO;

namespace clsLogica
{
    public class clsSenal
    {
        // Trae las posiciones donde se guardan estos campos
        readonly string SenalAlertaPosTipoIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIdenCabecero"].ToString();
        readonly string SenalAlertaPosNumeroIdenCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIdenCabecero"].ToString();
        readonly string SenalAlertaPosNombreCabecero = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombreCabecero"].ToString();
        readonly string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        readonly string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        readonly string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        #region Senal
        public List<clsDTOSenal> mtdCargarInfoSenal(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenal = new clsDTOSenal();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaSenal(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objSenal = new clsDTOSenal(
                            dr["IdSenal"].ToString().Trim(),
                            dr["CodigoSenal"].ToString().Trim(),
                            dr["DescripcionSenal"].ToString().Trim(),
                            dr["EsAutomatico"].ToString().Trim() == "True" ? true : false);

                        lstSenal.Add(objSenal);
                    }
                }
                else
                {
                    lstSenal = null;
                    strErrMsg = "No hay información de señales de alerta.";
                }
            }
            else
                lstSenal = null;

            return lstSenal;
        }

        public int mtdAgregarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intUltimoInsertado = 0;

            intUltimoInsertado = cDtSenal.mtdAgregarSenalRet(objSenal, ref strErrMsg);

            return intUltimoInsertado;
        }

        public void mtdActualizarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdActualizarSenal(objSenal, ref strErrMsg);
        }

        public void mtdEliminarSenal(clsDTOSenal objSenal, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable(objSenal.StrIdSenal, string.Empty, string.Empty, string.Empty, string.Empty, false);

            cDtSenal.mtdEliminarFormula(objFormula, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                cDtSenal.mtdEliminarSenal(objSenal, ref strErrMsg);
        }

        public clsDTOSenal mtdConsultarSenal(clsDTOSenal objSenalIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenalOut = new clsDTOSenal();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultarSenal(objSenalIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdSenal], [CodigoSenal], [DescripcionSenal], [EsAutomatico]
                    objSenalOut = new clsDTOSenal(
                        dtInfo.Rows[0]["IdSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["CodigoSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["DescripcionSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["EsAutomatico"].ToString().Trim() == "True" ? true : false);
                }
            }
            return objSenalOut;
        }

        #region Tabla Procesos
        public void mtdInsertarNroRegistrosSA(int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdInsertarNroRegistrosSA(intNroRegistros, strNombreUsuario, strDescripcion, ref strErrMsg);
        }

        public int mtdConteoRegistros(ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intConteo = 0;

            intConteo = cDtSenal.mtdConteoRegistros(ref strErrMsg);

            return intConteo;
        }

        public void mtdInsertarRegOperacion(int intIdUsuario,
            string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro,// IdConteo
            int intConteoOcurrencias,// Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string strCodigoSenal,// IdSenal
            string strDescripcionSenal,// DescSenal
            string strIndicador, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            cDtSenal.mtdInsertarRegOperacion(intIdUsuario,
                strIdentificacion, strNombreApellido,
                intConteoTblConteoRegistro,// IdConteo
                intConteoOcurrencias,// Cant
                intValor, intFrecuencia, strTipoCliente,
                strCodigoSenal,// IdSenal
                strDescripcionSenal, strIndicador, ref strErrMsg);
        }
        #endregion
        #endregion

        #region Operadores
        public List<clsDTOOperador> mtdCargarInfoOps(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaOperador(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objOp = new clsDTOOperador(dr["IdOperador"].ToString().Trim(),
                            dr["NombreOperador"].ToString().Trim(), dr["IdentificadorOperador"].ToString().Trim());

                        lstOps.Add(objOp);
                    }
                }
                else
                {
                    lstOps = null;
                    strErrMsg = "No hay información de operadores.";
                }
            }
            else
                lstOps = null;

            return lstOps;
        }

        public clsDTOOperador mtdBuscarOperador(clsDTOOperador objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperador(objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperador(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }

        public List<clsDTOOperadorGlobal> mtdCargarInfoOpGlobal(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperadorGlobal objOp = new clsDTOOperadorGlobal();
            List<clsDTOOperadorGlobal> lstOps = new List<clsDTOOperadorGlobal>();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultaOperadorGlobal(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objOp = new clsDTOOperadorGlobal(dr["IdOperador"].ToString().Trim(),
                            dr["NombreOperador"].ToString().Trim(), dr["IdentificadorOperador"].ToString().Trim());

                        lstOps.Add(objOp);
                    }
                }
                else
                {
                    lstOps = null;
                    strErrMsg = "No hay información de operadores.";
                }
            }
            else
                lstOps = null;

            return lstOps;
        }

        public clsDTOOperadorGlobal mtdBuscarOperadorGlobal(clsDTOOperadorGlobal objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperadorGlobal objOp = new clsDTOOperadorGlobal();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperadorGlobal(objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperadorGlobal(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }
        #endregion

        #region Formulas
        public void mtdGuardarFormula(List<object> LstFormula, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdGuardarFormula(LstFormula, ref strErrMsg);
        }

        public string mtdCargarFormulas(clsDTOSenal objSenal, ref List<object> LstFormula, ref bool booOk, ref string strErrMsg)
        {
            #region Vars
            bool booIsFirst = true, booIsGlobal = false;
            string strFormula = string.Empty;
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsParamArchivo cParam = new clsParamArchivo();

            //clsDTOVariable objVarIn, objVarOut = new clsDTOVariable();
            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn, objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn, objOpGOut = new clsDTOOperadorGlobal();
            #endregion

            lstFormula = mtdConsultarFormula(objSenal, ref strErrMsg);
            int contadorVariables = 0;
            int contadorExpresion = 0;
            #region Recorrido Formula
            foreach (clsDTOSenalVariable objFormula in lstFormula)
            {

                // Se valida si están comparando 2 variables en la expresión
                if (contadorExpresion < 3)
                {
                    if (objFormula.StrIdOperando == "1")
                    {
                        contadorVariables++;
                    }
                }
                else
                {
                    if (objFormula.StrIdOperando == "1")
                    {
                        contadorVariables = 1;
                        contadorExpresion = 0;
                    }
                    else
                    {
                        contadorExpresion = 0;
                        contadorVariables = 0;
                    }
                }

                #region Operacion
                switch (objFormula.StrIdOperando)
                {
                    case "1"://Variable
                        #region Variable
                        objCampoIn = new clsDTOEstructuraCampo(objFormula.StrValor, string.Empty,
                               string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                        objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                        if (string.IsNullOrEmpty(strFormula))
                            strFormula = objCampoOut.StrNombreCampo + " ";
                        else if(contadorVariables == 2)
                            strFormula = strFormula + objCampoOut.StrNombreCampo + " ";
                        else
                        {
                            if (booIsGlobal)
                            {
                                strFormula = strFormula + " ( " + objCampoOut.StrNombreCampo + " ) ";
                                booIsGlobal = false;
                            }
                            else
                                strFormula = strFormula + " y " + objCampoOut.StrNombreCampo + " ";
                        }

                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "2": //Operador
                        #region Operador
                        objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                        objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);
                        strFormula += objOpOut.StrIdentificadorOperador + " ";
                        LstFormula.Add(objFormula);
                        booOk = false;
                        #endregion

                        break;
                    case "3": //Otro
                        #region Otro
                        strFormula += objFormula.StrValor + " ";
                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "4": //Rango
                        #region Rango
                        if (booIsFirst)
                        {
                            strFormula = strFormula + " " + objFormula.StrValor;
                            booIsFirst = false;
                        }
                        else
                            strFormula = strFormula + " y " + objFormula.StrValor;

                        LstFormula.Add(objFormula);
                        booOk = true;
                        #endregion

                        break;
                    case "5": //Global
                        #region Operador Global
                        booIsGlobal = true;
                        objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                        objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                        if (string.IsNullOrEmpty(strFormula))
                            strFormula = objOpGOut.StrIdentificadorOperador + " ";
                        else
                            strFormula = strFormula + " y " + objOpGOut.StrIdentificadorOperador + " ";
                        #endregion

                        break;
                }
                #endregion

                if (!string.IsNullOrEmpty(strErrMsg))
                {
                    LstFormula = new List<object>();
                    strFormula = string.Empty;
                    booOk = false;
                    break;
                }

                contadorExpresion++;
            }
            #endregion

            return strFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormula(clsDTOSenal objSenal, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulaXSenal(objSenal, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormulas(ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulas(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(clsDTOSenal objSenal, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(objSenal, booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion], [EsGlobal]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
                else
                    lstFormula = null;
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public void mtdEliminarFormula(clsDTOSenalVariable objFormula, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdEliminarFormula(objFormula, ref strErrMsg);
        }

        public void mtdEjecutarSenalGlobal(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1, intPos = 0, intResultado = 0;
            bool booFormGlobal = false, booIsFirst = true, booResult = false, booIsThereGlobal = false;
            string strFormulaGlobal = string.Empty, strFiltro = string.Empty, strCondGlobal = string.Empty,
                strVariableRango = string.Empty, strFiltroTotal = string.Empty, strTextoCliente = string.Empty;

            DataTable dtSerializedData = new DataTable(), dtInfoId, dtFiltrados;
            DataRow dr;

            clsTupla objFormGlobal = new clsTupla(), objCondGlobal = new clsTupla();
            List<clsTupla> lstFormulaGlobal = new List<clsTupla>(), lstCondicionGlobal = new List<clsTupla>();

            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn = new clsDTOOperadorGlobal(), objOpGOut = new clsDTOOperadorGlobal();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();

            object objInfoRegistro = new object();
            List<object> lstInfoRegistro = new List<object>();
            #endregion

            #region Crea la estructura
            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
            {
                dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
            }
            #endregion

            #region Serializacion de los datos e insercion en la estructura
            dr = dtSerializedData.NewRow();
            foreach (DataRow drInfo in dtInfoCargada.Rows)
            {
                if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                {
                    dtSerializedData.Rows.Add(dr);
                    dr = dtSerializedData.NewRow();
                    intContador = 1;
                }

                dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                intContador++;
            }
            dtSerializedData.Rows.Add(dr);
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            //Segundo genero la formula de filtros globales
            #region Recorrido Formula

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {
                if (objFormula.BooEsGlobal)
                {
                    booIsThereGlobal = true;

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                strFormulaGlobal = strFormulaGlobal + " ( [" + objCampoOut.StrNombreCampo + "] ) ";
                                objFormGlobal = new clsTupla(intPos, strFormulaGlobal, objFormula.StrIdSenal);
                                lstFormulaGlobal.Add(objFormGlobal);
                                strFormulaGlobal = string.Empty;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strFiltro))
                                    strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                                else
                                    strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                                strVariableRango = objCampoOut.StrNombreCampo;
                            }
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strCondGlobal = ">= ";
                                else
                                    strCondGlobal = objOpOut.StrIdentificadorOperador + " ";
                            }
                            else
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strFiltro += ">= ";
                                else
                                    strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            }
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            if (booFormGlobal)
                            {
                                strCondGlobal = strCondGlobal + "" + objFormula.StrValor + " ";
                                objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                lstCondicionGlobal.Add(objCondGlobal);
                                strCondGlobal = string.Empty;
                                booFormGlobal = false;
                            }
                            else
                                strFiltro = strFiltro + " " + objFormula.StrValor + " ";

                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = strCondGlobal + " " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                }
                                else
                                    strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = "<= " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                    booFormGlobal = false;
                                }
                                else
                                    strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                                #endregion
                            }
                            #endregion

                            break;
                        case "5": //Global
                            #region Operador Global
                            intPos++;
                            booFormGlobal = true;
                            objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                            objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                            strFormulaGlobal = objOpGOut.StrIdentificadorOperador + " ";

                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
            }
            #endregion
            #endregion
            if (booIsThereGlobal)
            {
                #region Generacion de Registros
                //creo ciclo 1 para que se realice busquedas sobre cada registro de acuerdo al filtro de identificacion 
                dtInfoId = dtSerializedData.DefaultView.ToTable(true, SenalAlertaPosNumeroIden);

                foreach (DataRow drId in dtInfoId.Rows)
                {
                    #region Filtros
                    strFiltroTotal = string.Format($"[{SenalAlertaPosNumeroIden}] = '{0}' AND {1}",
                        drId[0].ToString(), strFiltro);
                    dtFiltrados = dtSerializedData.Select(strFiltroTotal).CopyToDataTable();

                    if (dtFiltrados.Rows.Count == 0)
                        continue;
                    #endregion

                    #region Recorrido para hacer comparaciones y Enviar Alertas
                    //creo ciclo 2 para realizar los filtros de las formulas y operaciones globales.
                    foreach (clsTupla objT1 in lstFormulaGlobal)
                    {
                        #region Recorrido para hacer comparaciones globales
                        intResultado = (int)dtFiltrados.Compute(objT1.StrInfo, "");
                        booResult = false;
                        foreach (clsTupla objT2 in lstCondicionGlobal)
                        {
                            if (objT1.IntPos == objT2.IntPos)
                            {
                                booResult = mtdCompararValores(objT2.StrInfo.Trim(), intResultado, ref strErrMsg);

                                if (!string.IsNullOrEmpty(strErrMsg))
                                    break;
                            }
                        }
                        #endregion

                        if ((string.IsNullOrEmpty(strErrMsg)) && booResult)
                        {
                            string strNroId = dtFiltrados.Rows[0][SenalAlertaPosNumeroIden].ToString(),
                                strTipoId = dtFiltrados.Rows[0][SenalAlertaPosTipoIden].ToString(),
                                strNombreCliente = dtFiltrados.Rows[0][SenalAlertaPosNombre].ToString();

                            #region Notificacion
                            //genero alerta y notificacion
                            strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                strNombreCliente, strTipoId, strNroId);

                            clsDTOSenal objSenalIn = new clsDTOSenal(objT1.StrIdSenal, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                            objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                            cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                strTextoCliente, ref strErrMsg);
                            #endregion

                            #region Registro de operacion

                            mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                            string strIndicador = string.Format("{0} = {1}", objT1.StrInfo, intResultado);

                            mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                1,// IdConteo,
                                1,// Cant,
                                0,// Valor, 
                                0,// Frecuencia,
                                string.Empty,// TipoCliente,
                                objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                                ref strErrMsg);
                            #endregion
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
                #endregion
            }
        }

        public void mtdEjecutarSenalGlobal(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref int intOcurrencias, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1, intPos = 0;
            decimal decResultado = 0;
            bool booFormGlobal = false, booIsFirst = true, booResult = false, booIsThereGlobal = false, booIsSUM = false;
            string strFormulaGlobal = string.Empty, strFiltro = string.Empty, strCondGlobal = string.Empty,
                strVariableRango = string.Empty, strFiltroTotal = string.Empty, strTextoCliente = string.Empty;

            DataTable dtSerializedData = new DataTable(), dtInfoId, dtFiltrados;
            DataRow dr;

            clsTupla objFormGlobal = new clsTupla(), objCondGlobal = new clsTupla();
            List<clsTupla> lstFormulaGlobal = new List<clsTupla>(), lstCondicionGlobal = new List<clsTupla>();

            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();
            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOOperadorGlobal objOpGIn = new clsDTOOperadorGlobal(), objOpGOut = new clsDTOOperadorGlobal();
            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();

            object objInfoRegistro = new object();
            List<object> lstInfoRegistro = new List<object>();
            List<string> lstCamposSUM = new List<string>();
            List<string> lstCamposPos = new List<string>();
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            //Segundo genero la formula de filtros globales
            #region Recorrido Formula

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {
                if (objFormula.BooEsGlobal)
                {
                    booIsThereGlobal = true;

                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                strFormulaGlobal = strFormulaGlobal + " ( [" + objCampoOut.StrNombreCampo + "] ) ";
                                objFormGlobal = new clsTupla(intPos, strFormulaGlobal, objFormula.StrIdSenal);
                                lstFormulaGlobal.Add(objFormGlobal);
                                strFormulaGlobal = string.Empty;

                                if (booIsSUM)
                                    lstCamposSUM.Add(objCampoOut.StrNombreCampo);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strFiltro))
                                    strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                                else
                                    strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                                strVariableRango = objCampoOut.StrNombreCampo;
                            }
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (booFormGlobal)
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strCondGlobal = ">= ";
                                else
                                    strCondGlobal = objOpOut.StrIdentificadorOperador + " ";
                            }
                            else
                            {
                                if (objOpOut.StrIdentificadorOperador == "Entre")
                                    strFiltro += ">= ";
                                else
                                    strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            }
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            if (booFormGlobal)
                            {
                                strCondGlobal = strCondGlobal + "" + objFormula.StrValor + " ";
                                objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                lstCondicionGlobal.Add(objCondGlobal);
                                strCondGlobal = string.Empty;
                                booFormGlobal = false;
                            }
                            else
                                strFiltro = strFiltro + " '" + objFormula.StrValor + "' ";

                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = strCondGlobal + " " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                }
                                else
                                    strFiltro = strFiltro + " '" + objFormula.StrValor + "' ";
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                if (booFormGlobal)
                                {
                                    strCondGlobal = "<= " + objFormula.StrValor + " ";
                                    objCondGlobal = new clsTupla(intPos, strCondGlobal, objFormula.StrIdSenal);
                                    lstCondicionGlobal.Add(objCondGlobal);
                                    strCondGlobal = string.Empty;
                                    booFormGlobal = false;
                                }
                                else
                                    strFiltro = strFiltro + " AND " + strVariableRango + " <= '" + objFormula.StrValor + "' ";
                                #endregion
                            }
                            #endregion

                            break;
                        case "5": //Global
                            #region Operador Global
                            intPos++;
                            booFormGlobal = true;
                            objOpGIn = new clsDTOOperadorGlobal(objFormula.StrValor, string.Empty, string.Empty);
                            objOpGOut = mtdBuscarOperadorGlobal(objOpGIn, ref strErrMsg);

                            strFormulaGlobal = objOpGOut.StrIdentificadorOperador + " ";

                            if (objOpGOut.StrIdentificadorOperador == "SUM")
                                booIsSUM = true;
                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
            }
            #endregion
            #endregion

            if (booIsThereGlobal)
            {
                #region Crea la estructura
                bool booIsDec = false;
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    foreach (string strCampoSum in lstCamposSUM)
                    {
                        if (strCampoSum == objEstructura.StrNombreCampo)
                        {
                            booIsDec = true;
                            break;
                        }
                    }

                    if (booIsDec)
                    {
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(decimal));
                        lstCamposPos.Add(objEstructura.StrPosicion);
                        booIsDec = false;
                    }
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
                }
                #endregion

                #region Serializacion de los datos e insercion en la estructura
                booIsDec = false;
                dr = dtSerializedData.NewRow();
                foreach (DataRow drInfo in dtInfoCargada.Rows)
                {
                    if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                    {
                        dtSerializedData.Rows.Add(dr);
                        dr = dtSerializedData.NewRow();
                        intContador = 1;
                    }

                    foreach (string strCampoPos in lstCamposPos)
                    {
                        if (Convert.ToInt32(strCampoPos) == (Convert.ToInt32(drInfo["Posicion"].ToString())))
                        {
                            booIsDec = true;
                            break;
                        }
                    }

                    if (booIsDec)
                    {
                        dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = Convert.ToDecimal(drInfo["ValorCampoArchivo"].ToString());
                        booIsDec = false;
                    }
                    else
                        dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                    intContador++;
                }
                dtSerializedData.Rows.Add(dr);
                #endregion

                #region Generacion de Registros
                //creo ciclo 1 para que se realice busquedas sobre cada registro de acuerdo al filtro de identificacion 
                dtInfoId = dtSerializedData.DefaultView.ToTable(true, SenalAlertaPosNumeroIdenCabecero);

                foreach (DataRow drId in dtInfoId.Rows)
                {
                    #region Filtros
                    strFiltroTotal = string.Format($"[{SenalAlertaPosNumeroIdenCabecero}] = '{0}' AND {1}",
                        drId[0].ToString(), strFiltro);
                    if (dtSerializedData.Select(strFiltroTotal).Count() > 0)
                        dtFiltrados = dtSerializedData.Select(strFiltroTotal).CopyToDataTable();
                    else
                        continue;
                    #endregion

                    #region Recorrido para hacer comparaciones y Enviar Alertas
                    //creo ciclo 2 para realizar los filtros de las formulas y operaciones globales.
                    foreach (clsTupla objT1 in lstFormulaGlobal)
                    {
                        #region Recorrido para hacer comparaciones globales
                        decResultado = Convert.ToDecimal(dtFiltrados.Compute(objT1.StrInfo, ""));
                        booResult = false;
                        foreach (clsTupla objT2 in lstCondicionGlobal)
                        {
                            if (objT1.IntPos == objT2.IntPos)
                            {
                                booResult = mtdCompararValores(objT2.StrInfo.Trim(), decResultado, ref strErrMsg);

                                if (!string.IsNullOrEmpty(strErrMsg))
                                    break;
                            }
                        }
                        #endregion

                        if ((string.IsNullOrEmpty(strErrMsg)) && booResult)
                        {
                            if (booIsSUM)
                                intOcurrencias = intOcurrencias + 1;
                            else
                                intOcurrencias = intOcurrencias + (int)decResultado;

                            string strNroId = dtFiltrados.Rows[0][SenalAlertaPosNumeroIden].ToString(),
                                strTipoId = dtFiltrados.Rows[0][SenalAlertaPosTipoIden].ToString(),
                                strNombreCliente = dtFiltrados.Rows[0][SenalAlertaPosNombre].ToString();

                            #region Notificacion
                            //genero alerta y notificacion
                            strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                strNombreCliente, strTipoId, strNroId);

                            clsDTOSenal objSenalIn = new clsDTOSenal(objT1.StrIdSenal, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                            objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                            //cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                            //    strTextoCliente, ref strErrMsg);
                            #endregion

                            #region Registro de operacion

                            mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                            string strIndicador = string.Format("{0} = {1}", objT1.StrInfo, decResultado);

                            mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                1, 1, 0, 0, string.Empty,// IdConteo,// Cant,// Valor, // Frecuencia,// TipoCliente,
                                objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                                ref strErrMsg);
                            #endregion
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;
                }
                #endregion
            }
        }

        private bool mtdCompararValores(string strOperacion, decimal decValorEvaluar, ref string strErrMsg)
        {
            bool booResult = false, booIsNumber = false;
            string[] strPartes = strOperacion.Replace("'", "").Split(' ');

            if (clsUtilidades.mtdEsNumero(strPartes[1]))
                booIsNumber = true;
            else
                strErrMsg = "No se puede evaluar la señal por el tipo de comparación";

            if (booIsNumber)
            {
                switch (strPartes[0])
                {
                    case "<":
                        if (decValorEvaluar < Convert.ToDecimal(strPartes[1]))
                            booResult = true;
                        break;
                    case ">":
                        if (decValorEvaluar > Convert.ToDecimal(strPartes[1]))
                            booResult = true;
                        break;
                    case "<=":
                        if (decValorEvaluar <= Convert.ToDecimal(strPartes[1]))
                            booResult = true;
                        break;
                    case ">=":
                        if (decValorEvaluar >= Convert.ToDecimal(strPartes[1]))
                            booResult = true;
                        break;
                    case "=":
                        if (decValorEvaluar == Convert.ToDecimal(strPartes[1]))
                            booResult = true;
                        break;
                }
            }

            return booResult;
        }

        public void mtdEjecutarSenalManual(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref string strErrMsg)
        {
            #region Variables Formulas para filtros
            int intContador = 1;
            string strVariableRango = string.Empty, strFiltro = string.Empty, strFiltroTotal = string.Empty;
            bool booIsFirst = true;
            DataTable dtSerializedData = new DataTable(), dtFiltrados;
            DataRow dr;

            clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
            clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();

            clsParamArchivo cParam = new clsParamArchivo();
            clsUtilidades cUtil = new clsUtilidades();
            #endregion

            #region Crea la estructura
            foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
            {
                dtSerializedData.Columns.Add(objEstructura.StrNombreCampo, typeof(string));
            }
            #endregion

            #region Serializacion de los datos e insercion en la estructura
            dr = dtSerializedData.NewRow();
            foreach (DataRow drInfo in dtInfoCargada.Rows)
            {
                if (Convert.ToInt32(drInfo["Posicion"].ToString()) < intContador)
                {
                    dtSerializedData.Rows.Add(dr);
                    dr = dtSerializedData.NewRow();
                    intContador = 1;
                }

                dr[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString();
                intContador++;
            }
            dtSerializedData.Rows.Add(dr);
            #endregion

            #region Creacion de Formulas para filtros
            //Primero genero la formula de filtros individuales
            #region Recorrido Formula
            string strIdSenal = string.Empty;
            List<string> lstCamposResultado = new List<string>();

            foreach (clsDTOSenalVariable objFormula in lstFormulas)
            {
                strIdSenal = objFormula.StrIdSenal;

                #region Operacion
                switch (objFormula.StrIdOperando)
                {
                    case "1"://Variable
                        #region Variable
                        objCampoIn = new clsDTOEstructuraCampo(objFormula.StrValor, string.Empty,
                            string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                        objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);
                        lstCamposResultado.Add(objCampoOut.StrNombreCampo);

                        if (string.IsNullOrEmpty(strFiltro))
                            strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                        else
                            strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                        strVariableRango = objCampoOut.StrNombreCampo;
                        #endregion

                        break;
                    case "2": //Operador
                        #region Operador
                        objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                        objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                        if (objOpOut.StrIdentificadorOperador == "Entre")
                            strFiltro += ">= ";
                        else
                            strFiltro += objOpOut.StrIdentificadorOperador + " ";
                        #endregion

                        break;
                    case "3": //Otro Valor
                        #region Otro Valor

                        strFiltro = strFiltro + " " + objFormula.StrValor + " ";

                        #endregion

                        break;
                    case "4": //Rangos
                        #region Rangos
                        if (booIsFirst)
                        {
                            #region Rango Inferior
                            strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                            #endregion

                            booIsFirst = false;
                        }
                        else
                        {
                            #region Rango Superior
                            strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                            #endregion
                        }
                        #endregion

                        break;
                }
                #endregion

                if (!string.IsNullOrEmpty(strErrMsg))
                    break;
            }
            #endregion
            #endregion

            if (string.IsNullOrEmpty(strErrMsg))
            {
                #region Filtros
                dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                #endregion

                #region Generacion de Registros
                if (dtFiltrados.Rows.Count != 0)
                {
                    foreach (DataRow drFiltrados in dtFiltrados.Rows)
                    {
                        string strNroId = drFiltrados[SenalAlertaPosNumeroIden].ToString(),
                            strTipoId = drFiltrados[SenalAlertaPosTipoIden].ToString(),
                            strNombreCliente = drFiltrados[SenalAlertaPosNombre].ToString();

                        #region Notificacion
                        //genero alerta y notificacion
                        string strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                            strNombreCliente, strTipoId, strNroId);

                        clsDTOSenal objSenalIn = new clsDTOSenal(strIdSenal, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                        objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                        cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                            strTextoCliente, ref strErrMsg);
                        #endregion

                        #region Recorrido prara generar el indicador
                        string strIndicador = "Filtro: " + strFiltro + " Resultado: ";
                        foreach (string strCampoResultado in lstCamposResultado)
                        {
                            strIndicador = strIndicador + " " + strCampoResultado + "->" + drFiltrados[strCampoResultado].ToString();
                        }
                        #endregion

                        #region Registro de operacion

                        mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                        mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                            1, 1, 0, 0,// IdConteo,// Cant,// Valor, // Frecuencia,
                            string.Empty,// TipoCliente,
                            objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, strIndicador,
                            ref strErrMsg);
                        #endregion
                    }
                }
                #endregion
            }
        }

        public void mtdEjecutarSenalManual(List<clsDTOSenalVariable> lstFormulas, List<clsDTOEstructuraCampo> lstEstrucArchivo,
            DataTable dtInfoCargada, int intIdUsuario, string strNombreUsuario, ref int intOcurrencias, ref string strErrMsg)
        {
            try
            {
                #region Variables Formulas para filtros
                string strVariableRango = string.Empty, strFiltro = string.Empty, strFiltroTotal = string.Empty;
                bool booIsFirst = true;
                DataTable dtSerializedData = new DataTable(), dtFiltrados = new DataTable();

                clsDTOOperador objOpIn = new clsDTOOperador(), objOpOut = new clsDTOOperador();
                clsDTOEstructuraCampo objCampoIn = new clsDTOEstructuraCampo(), objCampoOut = new clsDTOEstructuraCampo();

                clsParamArchivo cParam = new clsParamArchivo();
                clsUtilidades cUtil = new clsUtilidades();
                #endregion

                #region Creacion de Formulas para filtros
                //Primero genero la formula de filtros individuales
                #region Recorrido Formula
                string strIdSenal = string.Empty;
                List<string> lstCamposResultado = new List<string>();
                string[] strCamposEnteros = new string[10];
                int intCounterI = 0;
                // Contador variables
                int contadorVariables = 0;
                int contadorExpresion = 0;
                foreach (clsDTOSenalVariable objFormula in lstFormulas)
                {
                    if (objFormula.BooEsGlobal)
                        continue;

                    // Se valida si están comparando 2 variables en la expresión
                    if (contadorExpresion < 3)
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables++;
                        }
                    }
                    else
                    {
                        if (objFormula.StrIdOperando == "1")
                        {
                            contadorVariables = 1;
                            contadorExpresion = 0;
                        }
                        else
                        {
                            contadorExpresion = 0;
                            contadorVariables = 0;
                        }
                    }


                    strIdSenal = objFormula.StrIdSenal;



                    #region Operacion
                    switch (objFormula.StrIdOperando)
                    {
                        case "1"://Variable
                            #region Variable
                            objCampoIn = new clsDTOEstructuraCampo(objFormula.StrValor, string.Empty,
                                string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                            objCampoOut = cParam.mtdBuscarCampo(objCampoIn, ref strErrMsg);
                            lstCamposResultado.Add(objCampoOut.StrNombreCampo);

                            if (string.IsNullOrEmpty(strFiltro))
                                strFiltro = "[" + objCampoOut.StrNombreCampo + "] ";
                            else if (contadorVariables == 2)
                                strFiltro = strFiltro + "[" + objCampoOut.StrNombreCampo + "] ";
                            else
                                strFiltro = strFiltro + " AND " + "[" + objCampoOut.StrNombreCampo + "] ";

                            strVariableRango = objCampoOut.StrNombreCampo;
                            #endregion

                            break;
                        case "2": //Operador
                            #region Operador
                            objOpIn = new clsDTOOperador(objFormula.StrValor, string.Empty, string.Empty);
                            objOpOut = mtdBuscarOperador(objOpIn, ref strErrMsg);

                            if (objOpOut.StrIdentificadorOperador == "Entre")
                                strFiltro += ">= ";
                            else
                                strFiltro += objOpOut.StrIdentificadorOperador + " ";
                            #endregion

                            break;
                        case "3": //Otro Valor
                            #region Otro Valor

                            strFiltro = strFiltro + $" '{objFormula.StrValor}' ";

                            #region Campo Entero
                            if (mtdEsNumero(objFormula.StrValor))
                            {
                                strCamposEnteros[intCounterI] = strVariableRango;
                                intCounterI++;

                            }
                            #endregion
                            #endregion

                            break;
                        case "4": //Rangos
                            #region Rangos
                            if (booIsFirst)
                            {
                                #region Rango Inferior
                                strFiltro = strFiltro + " " + objFormula.StrValor + " ";
                                #endregion

                                #region Campo Entero
                                if (mtdEsNumero(objFormula.StrValor))
                                {
                                    strCamposEnteros[intCounterI] = strVariableRango;
                                    intCounterI++;

                                }
                                #endregion

                                booIsFirst = false;
                            }
                            else
                            {
                                #region Rango Superior
                                strFiltro = strFiltro + " AND " + strVariableRango + " <= " + objFormula.StrValor + " ";
                                #endregion
                            }
                            #endregion

                            break;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(strErrMsg))
                        break;

                    contadorExpresion++;
                }
                #endregion
                #endregion

                #region Crea la estructura
                foreach (clsDTOEstructuraCampo objEstructura in lstEstrucArchivo)
                {
                    bool booIsInteger = false;
                    foreach (string strCampoTmp in strCamposEnteros)
                    {
                        if (strCampoTmp == objEstructura.StrNombreCampo)
                        {
                            booIsInteger = true;
                            break;
                        }
                    }

                    if (booIsInteger)
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(decimal));
                    else
                        dtSerializedData.Columns.Add(objEstructura.StrNombreCampo.ToUpper().Trim(), typeof(string));
                }
                #endregion

                #region Serializacion de los datos e insercion en la estructura
                int lineaActual = 2;
                object[] array = new object[dtSerializedData.Columns.Count];

                // Llena el DataTable donde se aplicarán los filtros
                foreach (DataRow drInfo in dtInfoCargada.Rows)
                {
                    if (lineaActual == Convert.ToInt32(drInfo["NumeroLinea"].ToString()))
                    {
                        array[Convert.ToInt32(drInfo["Posicion"].ToString()) - 1] = drInfo["ValorCampoArchivo"].ToString().Replace("\"", "");
                    }
                    else
                    {
                        dtSerializedData.Rows.Add(array);
                        lineaActual = Convert.ToInt32(drInfo["NumeroLinea"].ToString());
                    }
                }
                //dtSerializedData.Rows.Add(dr);
                #endregion

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (!string.IsNullOrEmpty(strFiltro))
                    {
                        #region Filtros
                        if (dtSerializedData.Select(strFiltro.ToUpper().Trim()).Count() > 0)
                            dtFiltrados = dtSerializedData.Select(strFiltro).CopyToDataTable();
                        #endregion

                        #region Generacion de Registros
                        if (dtFiltrados.Rows.Count != 0)
                        {
                            intOcurrencias = intOcurrencias + dtFiltrados.Rows.Count;
                            foreach (DataRow drFiltrados in dtFiltrados.Rows)
                            {
                                try
                                {
                                    string strNroId = drFiltrados[SenalAlertaPosNumeroIdenCabecero].ToString(),
                                        strTipoId = drFiltrados[SenalAlertaPosTipoIdenCabecero].ToString(),
                                        strNombreCliente = drFiltrados[SenalAlertaPosNombreCabecero].ToString();

                                    #region Notificacion
                                    //genero alerta y notificacion
                                    string strTextoCliente = string.Format("Alerta para el Cliente {0} con número de documento {1} {2}. ",
                                        strNombreCliente, strTipoId, strNroId);

                                    clsDTOSenal objSenalIn = new clsDTOSenal(strIdSenal, string.Empty, string.Empty, true), objSenalOut = new clsDTOSenal();
                                    objSenalOut = mtdConsultarSenal(objSenalIn, ref strErrMsg);

                                    //cUtil.mtdGenerarNotificacion(24, "SEÑAL DE ALERTA", objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, intIdUsuario,
                                    //    strTextoCliente, ref strErrMsg);
                                    #endregion

                                    #region Recorrido prara generar el indicador
                                    string strIndicador = "Filtro: " + strFiltro + " Resultado: ";
                                    foreach (string strCampoResultado in lstCamposResultado)
                                    {
                                        strIndicador = strIndicador + " " + strCampoResultado + "->" + drFiltrados[strCampoResultado].ToString();
                                    }
                                    #endregion

                                    #region Registro de operacion

                                    mtdInsertarNroRegistrosSA(1, strNombreUsuario, "Señal de alerta", ref strErrMsg);

                                    mtdInsertarRegOperacion(intIdUsuario, strNroId.Trim(), strNombreCliente.Trim(),//Identificacion y NombreApellido
                                        1, 1, 0, 0,// IdConteo,// Cant,// Valor, // Frecuencia,
                                        string.Empty,// TipoCliente,
                                        objSenalOut.StrCodigoSenal, objSenalOut.StrDescripcionSenal, strIndicador.Replace("'", " ").Trim(),
                                        ref strErrMsg);
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        /// <summary>
        /// Metodo que permite evaluar si una cadena es un numero.
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns>
        public bool mtdEsNumero(string strNumero)
        {
            bool booResult = false;
            Regex regex = new Regex(@"^[0-9]+$");

            if (regex.IsMatch(strNumero))
                booResult = true;

            return booResult;
        }

        #endregion

        /*
         * Metodos para el Servicio
         */
        #region Servicio
        public clsDTOSenal mtdConsultarSenal(string strOleConn, clsDTOSenal objSenalIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOSenal objSenalOut = new clsDTOSenal();
            #endregion Vars

            dtInfo = cDtSenal.mtdConsultarSenal(strOleConn, objSenalIn, ref strErrMsg);

            if (dtInfo != null)
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdSenal], [CodigoSenal], [DescripcionSenal], [EsAutomatico]
                    objSenalOut = new clsDTOSenal(
                        dtInfo.Rows[0]["IdSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["CodigoSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["DescripcionSenal"].ToString().Trim(),
                        dtInfo.Rows[0]["EsAutomatico"].ToString().Trim() == "True" ? true : false);
                }

            return objSenalOut;
        }

        public clsDTOOperador mtdBuscarOperador(string strOleConn, clsDTOOperador objOpIn, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtSenal cDtSenal = new clsDtSenal();
            clsDTOOperador objOp = new clsDTOOperador();
            #endregion Vars

            dtInfo = cDtSenal.mtdBuscarOperador(strOleConn, objOpIn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    //[IdOperador], [NombreOperador], [IdentificadorOperador]
                    objOp = new clsDTOOperador(dtInfo.Rows[0]["IdOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["NombreOperador"].ToString().Trim(),
                        dtInfo.Rows[0]["IdentificadorOperador"].ToString().Trim());
                }
            }
            return objOp;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormulas(string strOleConn, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormulas(strOleConn, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        public List<clsDTOSenalVariable> mtdConsultarFormSenalAuto(string strOleConn, bool booAutomatico, ref string strErrMsg)
        {
            #region Vars
            clsDTOSenalVariable objFormula = new clsDTOSenalVariable();
            List<clsDTOSenalVariable> lstFormula = new List<clsDTOSenalVariable>();
            clsDtSenal cDtSenal = new clsDtSenal();
            DataTable dtInfo = new DataTable();
            #endregion

            dtInfo = cDtSenal.mtdConsultarFormSenalAuto(strOleConn, booAutomatico, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {//[IdSenalVariable], [IdSenal], [IdOperando], [Valor], [Posicion]
                        objFormula = new clsDTOSenalVariable(dr["IdSenal"].ToString().Trim(),
                            dr["IdSenalVariable"].ToString().Trim(),
                            dr["IdOperando"].ToString().Trim(),
                            dr["Valor"].ToString().Trim(),
                            dr["Posicion"].ToString().Trim(),
                            dr["EsGlobal"].ToString().Trim() == "True" ? true : false);

                        lstFormula.Add(objFormula);
                    }
                }
            }
            else
                lstFormula = null;

            return lstFormula;
        }

        #region Tabla Procesos
        public void mtdInsertarNroRegistrosSA(string strOleConn, int intNroRegistros, string strNombreUsuario, string strDescripcion, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();

            cDtSenal.mtdInsertarNroRegistrosSA(strOleConn, intNroRegistros, strNombreUsuario, strDescripcion, ref strErrMsg);
        }

        public int mtdConteoRegistros(string strOleConn, ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            int intConteo = 0;

            intConteo = cDtSenal.mtdConteoRegistros(strOleConn, ref strErrMsg);

            return intConteo;
        }

        public void mtdInsertarRegOperacion(string strOleConn, int intIdUsuario,
            string strIdentificacion, string strNombreApellido,
            int intConteoTblConteoRegistro, int intConteoOcurrencias,// IdConteo // Cant
            int intValor, int intFrecuencia, string strTipoCliente,
            string strCodigoSenal, string strDescripcionSenal,// IdSenal // DescSenal
            string strIndicador,
            ref string strErrMsg)
        {
            clsDtSenal cDtSenal = new clsDtSenal();
            cDtSenal.mtdInsertarRegOperacion(strOleConn, intIdUsuario,
                strIdentificacion, strNombreApellido,
                intConteoTblConteoRegistro,// IdConteo
                intConteoOcurrencias,// Cant
                intValor, intFrecuencia, strTipoCliente,
                strCodigoSenal,// IdSenal
                strDescripcionSenal, strIndicador, ref strErrMsg);
        }
        #endregion
        #endregion
    }

    public class clsTupla
    {
        int _intPos;
        string _strInfo;
        string _strIdSenal;

        public string StrIdSenal
        {
            get { return _strIdSenal; }
            set { _strIdSenal = value; }
        }

        public int IntPos
        {
            get { return _intPos; }
            set { _intPos = value; }
        }

        public string StrInfo
        {
            get { return _strInfo; }
            set { _strInfo = value; }
        }

        public clsTupla()
        {
        }

        public clsTupla(int intPos, string strInfo, string strIdSenal)
        {
            IntPos = intPos;
            StrInfo = strInfo;
            StrIdSenal = strIdSenal;
        }
    }
}
