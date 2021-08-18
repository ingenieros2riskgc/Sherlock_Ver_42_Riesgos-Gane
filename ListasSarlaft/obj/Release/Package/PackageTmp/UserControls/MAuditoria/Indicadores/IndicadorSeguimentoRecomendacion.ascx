<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndicadorSeguimentoRecomendacion.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Indicadores.IndicadorSeguimentoRecomendacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../../../Imagenes/Icons/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../../../Imagenes/Icons/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
<style type="text/css">
    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
    .Apariencia {
        margin-bottom: 0px;
    }
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .ajax__tab_header
    {
        text-align: center;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .ajax__tab_header
    {
        color: #909090;
        background-color: #EEEEEE;
    }

    .ajax__tab_xp .ajax__tab_body
    {
        background-color: #EEEEEE;
        border: 1px solid #EAEAEA;
        padding: 5px;
        list-style-type: none;
        font-size: 20px;
    }

    .ajax__tab_default .ajax__tab_inner
    {
        height: 100%;
    }

    .ajax__tab_default .ajax__tab_tab
    {
        height: 100%;
        font-size: 12px;
        background-color: #EEEEEE;
    }

    .ajax__tab_xp .ajax__tab_hover .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_active .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_inner
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_hover .ajax__tab_inner
    {
        height: 100%;
        color: #365F91;
    }

    .ajax__tab_xp .ajax__tab_active .ajax__tab_inner
    {
        height: 100%;
        color: #365F91;
    }

    #Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }

    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }

    .style1
    {
        height: 74px;
    }
    body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
        .buttonClass
        {
            font-size: small;
        }
        .buttonClass:hover
        {
            font-size: XX-Large;
        }
        .buttonClassNumber
        {
            font-size: XX-Large;
        }
        .LabelClass
        {
            Font-Size: small;
            font-family:Calibri;
        }
</style>
<asp:SqlDataSource ID="SqlDSregistrosAud" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdRegistroAuditoria],[Tema]
  FROM [Auditoria].[RegistrosAuditoria]
where [IdPlaneacion] = @IdPlaneacion and [IdArea] = @IdArea ">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
        <asp:Parameter Name="IdArea" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion] where [IdArea] = @IdArea">
    <SelectParameters>
        <asp:Parameter Name="IdArea" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource18" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdAuditoria], [Tema] 
                   FROM [Auditoria].[Auditoria] 
                   WHERE ([IdPlaneacion] = @IdPlaneacion AND [Estado] = 'INFORME')">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgok" runat="server" Text="Ok" OnClick="btnImgok_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblAccion" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Panel ID="pnlPlaneacion" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <table>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="GridView8" runat="server" DataSourceID="SqlDataSource8" Font-Names="Calibri"
                                        Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                                        BorderStyle="Solid" DataKeyNames="IdPlaneacion" AllowPaging="True" AllowSorting="True"
                                        ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                                        CellPadding="4" OnSelectedIndexChanged="GridView8_SelectedIndexChanged"
                                        >
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="IdPlaneacion" HeaderText="Código" InsertVisible="False"
                                                ReadOnly="True" SortExpression="IdPlaneacion">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/Audit.png"
                                                ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" Font-Bold="True" />
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
                                    <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
       

        
        
        <table width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Indicador Seguimiendo Recomendaciones"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" width="100%">
                        <tr align="center" bgcolor="#EEEEEE" id="filaAuditoria" runat="server">
                            <td>
                                <table class="tabla">
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label67" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodPlaneacion" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label66" runat="server" CssClass="Apariencia" Text="Planeación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomPlaneacion" runat="server" CssClass="Apariencia" Width="400px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnPlaneacion" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupPlanea" runat="server" BehaviorID="popupPlaneacion"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlPlaneacion" Position="Bottom"
                                                TargetControlID="imgBtnPlaneacion" OffsetX="-200">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
            </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" width="15%">
                        <tr>
                <td>
                         <asp:Label ID="txbTextoExcel" runat="server" Text="Para exportar a Excel:"></asp:Label>
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click"/>
                    </td>
            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                                        <td>
                                            <br />
                                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                            Font-Bold="False"
                                                            ShowHeaderWhenEmpty="True" >
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="I-P" HeaderText="I-P"
                                                                    ReadOnly="True" SortExpression="IdRecomendacion">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="No.AUD" HeaderText="No.AUD"
                                                                    SortExpression="No.AUD" ReadOnly="True" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="(MM/DD/AAAA)" HeaderText="(MM/DD/AAAA)" SortExpression="(MM/DD/AAAA)"
                                                                    ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MES" HeaderText="MES"
                                                                    SortExpression="Nombre" ReadOnly="True" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EDAD" HeaderText="EDAD"
                                                                    SortExpression="Nombre" ReadOnly="True" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="HALLAZGO" HeaderText="HALLAZGO" SortExpression="Tipo"
                                                                    ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="RECOMENDACIÓN" HeaderText="RECOMENDACIÓN"
                                                                    SortExpression="Nombre_DP">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ÁREA" HeaderText="ÁREA"
                                                                    SortExpression="FechaRegistro" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO"
                                                                    SortExpression="edadRecomendacion" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="RIESGO" HeaderText="RIESGO"
                                                                    SortExpression="edadRecomendacion" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
        </ContentTemplate>
    <Triggers>
         <asp:PostBackTrigger ControlID="ImButtonExcelExport" />
    </Triggers>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
