<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportePlanAuditoria.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Reportes.ReportePlanAuditoria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:SqlDataSource ID="SqlDSplaneaciones" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion],[Nombre] FROM [Auditoria].[Planeacion] where [IdArea] = @IdArea">
    <SelectParameters>
        <asp:Parameter Name="IdArea" Type="Int32" />    
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDSmacroproceso" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdMacroProceso], [Nombre] 
FROM [Procesos].[Macroproceso] 
inner join Procesos.CadenaValor as CV on Macroproceso.IdCadenaValor = CV.IdCadenaValor
where Macroproceso.Estado = 1 and CV.Estado = 1"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDSproceso" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdProceso], [Nombre] FROM [Procesos].[Proceso] WHERE [IdMacroProceso] = @IdMacroProceso and Estado = 1">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblIdMacro" Name="IdMacroProceso" PropertyName="Text"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDSgrupoAuditores" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdGrupoAuditoria], [Nombre] FROM [Auditoria].[GrupoAuditoria]"></asp:SqlDataSource>
<asp:UpdatePanel ID="RPAbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadRPA" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reporte Plan Auditoria" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="dvFiltros" class="TableContains" runat="server">
                <Table class="tabla" align="center" width="80%" border="1">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblPlaneacion" runat="server" Text="Planeacion:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddlPlaneacion" runat="server"  Font-Names="Calibri"
                                                                                    Font-Size="Small" Width="450px" DataSourceID="SqlDSplaneaciones"
                                                                                    DataTextField="Nombre" DataValueField="IdPlaneacion" OnDataBound="ddlPlaneacion_DataBound" OnSelectedIndexChanged="ddlPlaneacion_SelectedIndexChanged" AutoPostBack="True">
                                                                                </asp:DropDownList>
                            </td>
                        </tr>       
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblRegistrosAuditoria" runat="server" Text="Registro Auditoria:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlRegistrosAud" runat="server"  Font-Names="Calibri"
                                                                                    Font-Size="Small" Width="450px" DataTextField="Tema"  DataValueField="IdRegistroAuditoria" OnDataBound="ddlRegistrosAud_DataBound"
                                                                                    >
                                                                                </asp:DropDownList>
                        </td>
                    </tr>               
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImbSearchFiltro" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="metas" ToolTip="Buscar" OnClick="ImbSearchFiltro_Click"/>
            
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="ImageButton2_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="form" class="TableContains" runat="server" visible="false">
                <Table class="tabla" align="center" width="80%" border="1">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LmacroProceso" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                                <asp:Label id="lblIdMacro" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddlMacroProceso" runat="server" Width="450px" CssClass="Apariencia"
                            OnDataBound="ddlMacroProceso_DataBound" DataSourceID="SqlDSmacroproceso" DataTextField="Nombre"
                            DataValueField="IdMacroProceso" AutoPostBack="True" OnSelectedIndexChanged="ddlMacroProceso_SelectedIndexChanged">
                        </asp:DropDownList>
                            </td>
                        </tr>       
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblProceso" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlProceso" runat="server" Width="450px" CssClass="Apariencia" AutoPostBack="True" 
                            DataTextField="Nombre"
                            DataValueField="IdProceso" OnDataBound="ddlProceso_DataBound">
                        </asp:DropDownList>
                        </td>
                    </tr>     
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblMeses" Text="Meses:" runat="server" CssClass="Apariencia"></asp:Label>
                            </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlMesEjecucion" runat="server" Width="450px">
                                                    <asp:ListItem Text="--seleccionar--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                        </td>
                    </tr>   
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblPeriodicidad" Text="Periodicidad:" runat="server" CssClass="Apariencia"></asp:Label>
                            </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="450px">
                                                    <asp:ListItem Text="--seleccionar--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Mensual" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Semestral" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Trimestral" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Cuatrimestral" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Semestral" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Anual" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblAuditor" Text="Auditor Resposable:" runat="server" CssClass="Apariencia"></asp:Label>
                            </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlGrupoAud2" runat="server"  Font-Names="Calibri"
                                                                                    Font-Size="Small" Width="450px" DataSourceID="SqlDSgrupoAuditores"
                                                                                    DataTextField="Nombre" DataValueField="IdGrupoAuditoria" OnDataBound="ddlGrupoAud2_DataBound">
                                                                                </asp:DropDownList>
                        </td>
                    </tr>               
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="metas" ToolTip="Buscar" OnClick="IBsearch_Click"/>
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        <div id="DbuttonConsolidado" runat="server" visible="false" class="ColumnStyle">
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
                    </tr>
                </Table>
        </div>
        <div id="DbuttonsAud" runat="server" visible="false" class="ColumnStyle">
            <Table id="TbuttonsAud" class="tabla" align="center" width="25%">
                <tr align="center">
                        <td>
                            Para exportar a PDF:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexportAud" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexportAud_Click"  />
                    </td>
                    <td>
                            Para exportar a Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelexportAud" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelexportAud_Click" />
                    </td>
                    </tr>
                </Table>
        </div>
        <div id="dvGridConsolidado" runat="server" visible="false" class="TableContains">
            <asp:GridView ID="GVreporteConsolidado" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" 
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" 
                                                Font-Bold="False"  ShowHeaderWhenEmpty="True" OnRowCommand="GVreporteConsolidado_RowCommand"  
                                                DataKeyNames="intIdRegistro" OnPreRender="GVreporteConsolidado_PreRender"
                                                >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Planeacion" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strPlaneacion" runat="server" Text='<% # Bind("strPlaneacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intIdRegistro" runat="server" Text='<% # Bind("intIdRegistro")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tema" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strTema" runat="server" Text='<% # Bind("strTema")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Realizadas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="dbRealizadas" runat="server" Text='<% # Bind("dbRealizadas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Programadas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="dbProgramadas" runat="server" Text='<% # Bind("dbProgramadas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cumplimiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="dbCumplimiento" runat="server" Text='<% # Bind("dbCumplimiento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strFechaInicio" runat="server" Text='<% # Bind("strFechaInicio")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Fecha Cierre" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strFechaCierre" runat="server" Text='<% # Bind("strFechaCierre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Detalle" 
                                                        CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
        </div>
        <div id="dvGrid" runat="server" visible="false">
            <asp:GridView ID="GVreporteAuditoria" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" 
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" 
                                                Font-Bold="False"  ShowHeaderWhenEmpty="True"  
                                                
                                                >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nombre Dependencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strNombreDependencia" runat="server" Text='<% # Bind("strNombreDependencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strNombreProces" runat="server" Text='<% # Bind("strNombreProces")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tema" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strTema" runat="server" Text='<% # Bind("strTema")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Objetivo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strObjetivo" runat="server" Text='<% # Bind("strObjetivo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strEstado" runat="server" Text='<% # Bind("strEstado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Auditor" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strGrupoAuditoria" runat="server" Text='<% # Bind("strGrupoAuditoria")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Especial o Permanente" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strEspecial" runat="server" Text='<% # Bind("strEspecial")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strPeriodicidad" runat="server" Text='<% # Bind("strPeriodicidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strMes" runat="server" Text='<% # Bind("strMes")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strFechaInicio" runat="server" Text='<% # Bind("strFechaInicio")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Fecha Cierre" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strFechaCierre" runat="server" Text='<% # Bind("strFechaCierre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Mes de ejecución" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strMesExe" runat="server" Text='<% # Bind("strMesExe")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Semana de ejecución" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strSemanaExe" runat="server" Text='<% # Bind("strSemanaExe")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
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
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
        </div>
</ContentTemplate>
    </asp:UpdatePanel>