<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComparativoRiesgos.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.ComparativoRiesgos.ComparativoRiesgos" %>
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
        <div class="TituloLabel" id="HeadSRI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Cuadro Comparativo de Riesgos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="dvGirdData" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                <asp:GridView ID="GVcomparativo" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                            CssClass="Apariencia" Font-Bold="False"   >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="intNumeroRegistro" HeaderText="Cantidad Registros" SortExpression="intNumeroRegistro" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intSumatoriaProbabilidad" HeaderText="Sumatoria Frecuencia" SortExpression="intSumatoriaProbabilidad" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intSumatoriaImpacto" HeaderText="Sumatoria Impacto" SortExpression="intSumatoriaImpacto" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intPromedioProbabilidad" HeaderText="Promedio Frecuencia" SortExpression="intPromedioProbabilidad" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intPromedioImpacto" HeaderText="Promedio Impacto" SortExpression="intPromedioImpacto" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strPerfil" HeaderText="Perfil" SortExpression="strPerfil" ItemStyle-HorizontalAlign="Center" />
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
                </table>
            </div>
        <div>
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
            <asp:Chart ID="ChartSaro" runat="server" Height="400px" Width="550px" >
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
        <asp:ChartArea Name="ChartSaroArea" BorderWidth="0" />
    </ChartAreas>
</asp:Chart> 
                        <asp:Chart ID="Chart1" runat="server" Height="400px" Width="550px" >
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
        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
    </ChartAreas>
</asp:Chart> 
                        </td>
                    </tr>
                </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>