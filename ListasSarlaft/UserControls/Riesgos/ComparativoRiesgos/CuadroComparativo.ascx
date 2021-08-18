<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuadroComparativo.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.ComparativoRiesgos.CuadroComparativo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="CCbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCC" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Cuadro Comparativo de Riesgos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="dvGirdData" class="ColumnStyle" runat="server">
            <table class="tabla" align="center" width="100%">
                <tr align="center">
                    <td>
                        <asp:Label ID="lblRiesgosInherente" runat="server" Text="Riesgos Inherente"></asp:Label>
                    </td>
                    </tr>
                <tr align="center">
                    <td>
                <asp:GridView ID="GVcomparativo" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" PageSize="20"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                    OnPageIndexChanging="GVcomparativo_PageIndexChanging"
                            CssClass="Apariencia" Font-Bold="False"   >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="strClasificacionRiesgo" HeaderText="Clasificación Riesgo" SortExpression="strClasificacionRiesgo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strRiesgoInherente" HeaderText="Riesgo Inherente" SortExpression="strRiesgoInherente" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intCantRiesgoInherente" HeaderText="Cantidad Riesgo Inherente" SortExpression="intCantRiesgoInherente" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="strRiesgoResidual" HeaderText="Riesgo Residual" SortExpression="strRiesgoResidual" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="intCantRiesgoResidual" HeaderText="Cantidad Riesgo Residual" SortExpression="intCantRiesgoResidual" ItemStyle-HorizontalAlign="Center" Visible="false" />
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
                <tr align="center">
                    <td>
                        <asp:Label ID="lblRiesgosResidual" runat="server" Text="Riesgos Residual"></asp:Label>
                    </td>
                    </tr>
                <tr align="center">
                    <td>
                <asp:GridView ID="GVcomparativoResidual" runat="server" CellPadding="4"
                            ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" PageSize="20"
                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                    OnPageIndexChanging="GVcomparativoResidual_PageIndexChanging"
                            CssClass="Apariencia" Font-Bold="False"   >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="strClasificacionRiesgo" HeaderText="Clasificación Riesgo" SortExpression="strClasificacionRiesgo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="strRiesgoInherente" HeaderText="Riesgo Inherente" SortExpression="strRiesgoInherente" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="intCantRiesgoInherente" HeaderText="Cantidad Riesgo Inherente" SortExpression="intCantRiesgoInherente" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                 <asp:BoundField DataField="strRiesgoResidual" HeaderText="Riesgo Residual" SortExpression="strRiesgoResidual" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="intCantRiesgoResidual" HeaderText="Cantidad Riesgo Residual" SortExpression="intCantRiesgoResidual" ItemStyle-HorizontalAlign="Center" />
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
        </ContentTemplate>
    </asp:UpdatePanel>