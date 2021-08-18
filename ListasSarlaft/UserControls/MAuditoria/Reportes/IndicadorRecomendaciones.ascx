<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndicadorRecomendaciones.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Reportes.IndicadorRecomendaciones" %>
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
        <div class="TituloLabel" id="HeadIR" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Indicadores de Recomendaciones" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="form" class="TableContains" runat="server">
                <Table class="tabla" align="center" width="65%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblTipoReporte" runat="server" Text="Tipo de reporte:"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlTipoReporte" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="General" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Por Departamento" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte"
                                    ErrorMessage="Debe seleccionar el tipo del reporte." ToolTip="Debe seleccionar el tipo del reporte."
                                    ValidationGroup="Report" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lfecha" runat="server" Text="Año de Indicador:" CssClass="Apariencia"></asp:Label>
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
                                <asp:Label ID="lblFechaInicialGen" runat="server" Text="Fecha Inicial:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txbFechaInicialGen" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="ceFechaInicialGen" runat="server" Enabled="true" TargetControlID="txbFechaInicialGen"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                        </tr>   
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblFechaFinalGen" runat="server" Text="Fecha Final:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txbFechaFinalGen" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="ceFechaFinalGen" runat="server" Enabled="true" TargetControlID="txbFechaFinalGen"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                        </tr>       
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblPlaneacion" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddlPlaneacion" runat="server" Width="300px">
                                    <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Planeado" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Implementado" Value="I"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>                  
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="Report" 
                                ToolTip="Buscar" OnClick="IBsearch_Click" />
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="formDtpo" class="TableContains" runat="server" visible="false">
                <Table class="tabla" align="center" width="65%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblDtpos" runat="server" Text="Departamentos:"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDepartamentos" runat="server" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlDepartamentos"
                                    ErrorMessage="Debe seleccionar el departamento." ToolTip="Debe seleccionar el departamento."
                                    ValidationGroup="ReportDtpo" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>    
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblFechaPlaneacion" runat="server" Text="Fecha Inicial:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txbFechaInicialDep" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="ceFechaPlaneacion" runat="server" Enabled="true" TargetControlID="txbFechaInicialDep"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                        </tr>      
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblFechaFinalDep" runat="server" Text="Fecha Final:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txbFechaFinalDep" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="ceFechaFinalDep" runat="server" Enabled="true" TargetControlID="txbFechaFinalDep"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                        </tr>  
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblPlaneacionDep" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddPlaneacionDep" runat="server" Width="300px">
                                    <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Planeado" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Implementado" Value="I"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>                   
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbSearchDtpo" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="ReportDtpo" 
                                ToolTip="Buscar" OnClick="ImbSearchDtpo_Click" />
            
            <asp:ImageButton ID="ImbCancelDtpo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="ImbCancelDtpo_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="Dbutton" runat="server" visible="false" class="ColumnStyle">
            <Table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                        <td>
                            Para exportar a PDF:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" />
                    </td>
                    <td>
                            Para exportar a Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click"/>
                    </td>
                    <td>
                            Limpiar:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImbCancel" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCancel_Click" />
                    </td>
                    </tr>
                </Table>
        </div>
        <div id="BodyGridIR" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                
                                <asp:GridView ID="GVindicadorRecomendaciones" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdFrecuencia,intIdIndicador"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowDataBound="OnRowDataBound" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <%--<asp:BoundField DataField="intIdIndicador" HeaderText="Código" SortExpression="intIdIndicador" ItemStyle-HorizontalAlign="Center" /> --%>
                                        <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="intIdIndicador" runat="server" Text='<% # Bind("intIdIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="25" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="25" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strIndicador" runat="server" Text='<% # Bind("strIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <%-- 
                                        <asp:BoundField DataField="strMetodologia" HeaderText="Metodologia" SortExpression="strMetodologia" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:BoundField DataField="strFrecuencia" HeaderText="Frecuencia" SortExpression="strFrecuencia" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="strResponsable" HeaderText="Responsable" SortExpression="strResponsable" HtmlEncodeFormatString="True"/>
                                         <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="intAcumulado" HeaderText="Acumulado" ReadOnly="True" SortExpression="intAcumulado" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intCumplimiento" HeaderText="Cumplimiento" ReadOnly="True" SortExpression="intCumplimiento" ItemStyle-HorizontalAlign="Center" />
                                        --%>
                                        <asp:TemplateField HeaderText="Metodologia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strMetodologia" runat="server" Text='<% # Bind("strMetodologia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frecuencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strFrecuencia" runat="server" Text='<% # Bind("strFrecuencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Responsable" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strResponsable" runat="server" Text='<% # Bind("strResponsable")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Meta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="intMeta" runat="server" Text='<% # Bind("intMeta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acumulado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="intAcumulado" runat="server" Text='<% # Bind("intAcumulado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cumplimiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="intCumplimiento" runat="server" Text='<% # Bind("intCumplimiento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pnRecomendaciones" runat="server">
                                                <asp:GridView ID="gvRecomendaciones" runat="server" AutoGenerateColumns="true" CssClass = "ChildGrid">
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        <div id="BodyGridDtpo" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVrecomendacionesDtpo" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdFrecuencia,intIdIndicador"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowDataBound="OnRowDataBoundDtpo" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Código" SortExpression="intIdIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strIndicador" runat="server" Text='<% # Bind("strIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strMetodologia" HeaderText="Metodologia" SortExpression="strMetodologia" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:BoundField DataField="strFrecuencia" HeaderText="Frecuencia" SortExpression="strFrecuencia" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="strResponsable" HeaderText="Responsable" SortExpression="strResponsable" HtmlEncodeFormatString="True"/>
                                         <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="intAcumuladoDtpo" HeaderText="Acumulado" ReadOnly="True" SortExpression="intAcumuladoDtpo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intCumplimiento" HeaderText="Cumplimiento" ReadOnly="True" SortExpression="intCumplimiento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pnRecomendaciones" runat="server">
                                                <asp:GridView ID="GVrecomendacionesDtpo" runat="server" AutoGenerateColumns="true" CssClass = "ChildGrid">
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        <div id="BodyGridDtpoDetalle" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVdeptoDetalle" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdFrecuencia,intIdIndicador"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowDataBound="OnRowDataBoundDtpoDetallado" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Código" SortExpression="intIdIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strIndicador" runat="server" Text='<% # Bind("strIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strMetodologia" HeaderText="Metodologia" SortExpression="strMetodologia" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:BoundField DataField="strFrecuencia" HeaderText="Frecuencia" SortExpression="strFrecuencia" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="strResponsable" HeaderText="Responsable" SortExpression="strResponsable" HtmlEncodeFormatString="True"/>
                                         <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="intAcumuladoDtpo" HeaderText="Acumulado" ReadOnly="True" SortExpression="intAcumuladoDtpo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intCumplimiento" HeaderText="Cumplimiento" ReadOnly="True" SortExpression="intCumplimiento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pnRecomendaciones" runat="server">
                                                <asp:GridView ID="GVrecomendacionesDtpo" runat="server" AutoGenerateColumns="true" CssClass = "ChildGrid">
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        <div id="dvGrafico" class="ColumnStyle" runat="server" visible="false">
            <asp:Chart ID="Chart1" runat="server" Width="850px">
            <Series>
            <asp:series Name="Series1"></asp:series>
                <asp:series Name="Series2"></asp:series>
            </Series>
            <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            </ChartAreas>
            </asp:Chart>
            </div>
        </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="IBsearch" />
        <asp:PostBackTrigger ControlID="ImbSearchDtpo" />
        <asp:PostBackTrigger ControlID="ImButtonPDFexport" />
        <asp:PostBackTrigger ControlID="ImButtonExcelExport" />
    </Triggers>
    </asp:UpdatePanel>