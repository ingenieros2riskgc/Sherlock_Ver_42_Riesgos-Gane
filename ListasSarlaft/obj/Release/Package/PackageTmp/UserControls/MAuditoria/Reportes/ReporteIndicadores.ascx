<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteIndicadores.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Reportes.ReporteIndicadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="RIAbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadRI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reporte Indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="form" class="TableContains" runat="server">
                <Table class="tabla" align="center" width="80%" border="1">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblAnoText" runat="server" Text="Año de Indicadores:"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txbAno" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="ceAno" runat="server" Format="yyyy"
                                    Enabled="True" TargetControlID="txbAno">
                                </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="txbAno"
                                    ForeColor="Red" ValidationGroup="reporte" ErrorMessage="Seleccine el año" ToolTip="Seleccine el año">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblMeses" Text="Meses:" runat="server" CssClass="Apariencia"></asp:Label>
                            </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlMesEjecucion" runat="server" Width="300px">
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
                            <asp:RequiredFieldValidator ID="rfvMeses" runat="server" ControlToValidate="ddlMesEjecucion"
                                    ForeColor="Red" InitialValue="0" ValidationGroup="reporte" ErrorMessage="Seleccine un mes" ToolTip="Seleccine un mes">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>   
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImbSearchFiltro" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="reporte" ToolTip="Buscar" OnClick="ImbSearchFiltro_Click" />
            
            <asp:ImageButton ID="ImbCancel" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="ImbCancel_Click"  />
                        </td>
                    </tr>
                    </Table>
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
                        <asp:ImageButton ID="ImbCancelB" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCancelB_Click" />
                    </td>
                    </tr>
                </Table>
        </div>
        <div id="dvGridReporte" runat="server" visible="false" class="TableContains">
            <asp:GridView ID="GVreporte" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" 
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" 
                                                Font-Bold="False"  ShowHeaderWhenEmpty="True" PageSize="12" 
                                                >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="strNombreMes" runat="server" Text='<% # Bind("strNombreMes")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Auditorias Realizadas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="dbRealizadas" runat="server" Text='<% # Bind("dbRealizadas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Auditorias Programadas" ItemStyle-HorizontalAlign="Left">
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
                                                    <asp:Label ID="dbCumplimiento" DataFormatString="{0:n2}" runat="server" Text='<% # Bind("dbCumplimiento")%>'></asp:Label>
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
        <div runat="server" id="dvGraficos" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartRiesgoInherente" runat="server" Height="400px" Width="550px" >
    <Titles>
        <asp:Title ShadowOffset="3" Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
    </Legends>
    <Series>
        <asp:Series Name="Default" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartRiesgoInherenteArea" BorderWidth="0" />
    </ChartAreas>
</asp:Chart> 
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        
                    </td>
                </tr>
            </table>
            
        </div>
        </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ImButtonPDFexport" />
        <asp:PostBackTrigger ControlID="ImButtonExcelExport" />
    </Triggers>
    </asp:UpdatePanel>