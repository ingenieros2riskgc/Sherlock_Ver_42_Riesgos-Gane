<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeguimientoRecomendaciones.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Reportes.SeguimientoRecomendaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="SRIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadSR" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Seguimiento de Recomendaciones" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="form" class="TableContains" runat="server">
                <Table class="tabla" align="center" width="65%">
                    
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lfecha" runat="server" Text="Año de Seguimiento:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="TXfechaInicial" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaConsulta" runat="server" Enabled="true" TargetControlID="TXfechaInicial"
                            Format="yyyy" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaConsul" runat="server" ControlToValidate="TXfechaInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="Report" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>    
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblFechaActual" runat="server" Text="Fecha del Seguimiento:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txbFechaActual" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="ceFecha" runat="server" Enabled="true" TargetControlID="txbFechaActual"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txbFechaActual"
                                    ErrorMessage="Debe ingresar la fecha del seguimiento." ToolTip="Debe ingresar la fecha del seguimiento."
                                    ValidationGroup="Report" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                            
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="Report" 
                                ToolTip="Buscar" OnClick="IBsearch_Click"  />
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="formRiesgos" class="TableContains" runat="server" visible="false">
                <Table class="tabla" align="center" width="65%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblRiesgos" runat="server" Text="Nivel de Riesgos:"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlRiesgos" runat="server" Width="300px">
                                <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Alto" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Medio" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Bajo" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlRiesgos"
                                    ErrorMessage="Debe seleccionar el nivel de riesgo." ToolTip="Debe seleccionar el nivel del riesgo."
                                    ValidationGroup="ReportDtpo" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>                          
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbSearchRiesgo" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="ReportDtpo" 
                                ToolTip="Buscar" OnClick="ImbSearchRiesgo_Click"/>
            
            <asp:ImageButton ID="ImbCancelDtpo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="ImbCancelDtpo_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="BodyGridSR" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVseguimientoRecomendaciones" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False"  >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                </td>
                            </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GVvalorRiesgo" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVvalorRiesgo_PreRender"  >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                    </td>
                </tr>
            </Table>
        </div>
        <div id="BodyGridRiesgos" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>Para exportar a Excel:
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click"/>
                        </td>
                    
                </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVriesgosDetalle" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdDependencia,intIdNivelRiesgo"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GVriesgosDetalle_PageIndexChanging" OnPreRender="GVriesgosDetalle_PreRender"  >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdAuditoria" HeaderText="No.AUD" SortExpression="intIdAuditoria" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Auditoria" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strTema" runat="server" Text='<% # Bind("strTema")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strFechaRegistro" HeaderText="Fecha Informe" DataFormatString="{0:d}" SortExpression="strFechaRegistro" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:BoundField DataField="intMesDiff" HeaderText="Edad (Meses)" SortExpression="intMesDiff" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="strHallazgo" HeaderText="Hallazgo" SortExpression="strHallazgo" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="strObservaciones" HeaderText="Recomendación" ReadOnly="True" SortExpression="strObservaciones" ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="strResponsable" HeaderText="Responsable" SortExpression="strResponsable" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="dtFechaSeguimiento" HeaderText="Fecha Seguimiento" ReadOnly="True" SortExpression="dtFechaSeguimiento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strSeguimiento" HeaderText="Observaciones" ReadOnly="True" SortExpression="strSeguimiento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strRiesgo" HeaderText="Nivel Riesgo" ReadOnly="True" SortExpression="strRiesgo" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                </td>
                            </tr>
            </Table>
        </div>
        </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ImButtonExcelExport" />
    </Triggers>
    </asp:UpdatePanel>