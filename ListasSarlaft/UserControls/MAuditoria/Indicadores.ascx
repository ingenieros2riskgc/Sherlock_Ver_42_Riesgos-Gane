<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicadores.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Indicadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:SqlDataSource ID="SqlDSindicadores" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[AuditoriaIndicadores] WHERE [IdIndicador] = @IdIndicador"
    InsertCommand="INSERT INTO [Auditoria].[AuditoriaIndicadores] ([Indicador],[MetodologiaNominador],[MetodologiaDenominador],[Frecuencia],[Responsable]
           ,[Meta],[NivelCumplimiento],[UsuarioRegistro],[FechaRegistro],[año],[Planeacion])  VALUES (@Indicador,@MetodologiaNominador,@MetodologiaDenominador
           ,@Frecuencia,@Responsable,@Meta,@NivelCumplimiento,@UsuarioRegistro,@FechaRegistro,@año,@planeacion)"
    SelectCommand="SELECT [IdIndicador],[Indicador],[MetodologiaNominador],[MetodologiaDenominador]
      ,[Frecuencia],[Responsable],[Meta],[Acumulado],[NivelCumplimiento]
      ,[UsuarioRegistro],LU.Usuario,[FechaRegistro],[año],AAI.[Planeacion]
  FROM [Auditoria].[AuditoriaIndicadores] as AAI
INNER JOIN Listas.Usuarios as LU on LU.IdUsuario = AAI.UsuarioRegistro"
    UpdateCommand="UPDATE [Auditoria].[AuditoriaIndicadores] SET [Indicador] = @Indicador,[MetodologiaNominador] = @MetodologiaNominador
      ,[MetodologiaDenominador] = @MetodologiaDenominador,[Frecuencia] = @Frecuencia,[Responsable] = @Responsable,[Meta] = @Meta
      ,[NivelCumplimiento] = @NivelCumplimiento, [año] = @año,[Planeacion]=@planeacion
        WHERE IdIndicador = @IdIndicador"
    >
    
    <DeleteParameters>
        <asp:Parameter Name="IdIndicador" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Indicador" Type="String" />
        <asp:Parameter Name="MetodologiaNominador" Type="String" />
        <asp:Parameter Name="MetodologiaDenominador" Type="String" />
        <asp:Parameter Name="Frecuencia" Type="Int32" />
        <asp:Parameter Name="Responsable" Type="String" />
        <asp:Parameter Name="Meta" Type="Int32" />
        <asp:Parameter Name="NivelCumplimiento" Type="Int32" />
        <asp:Parameter Name="UsuarioRegistro" Type="Int32" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="año" Type="String" />
        <asp:Parameter Name="planeacion" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdIndicador" Type="Int32" />
        <asp:Parameter Name="Indicador" Type="String" />
        <asp:Parameter Name="MetodologiaNominador" Type="String" />
        <asp:Parameter Name="MetodologiaDenominador" Type="String" />
        <asp:Parameter Name="Frecuencia" Type="Int32" />
        <asp:Parameter Name="Responsable" Type="String" />
        <asp:Parameter Name="Meta" Type="Int32" />
        <asp:Parameter Name="NivelCumplimiento" Type="Int32" />
        <asp:Parameter Name="año" Type="String" />
        <asp:Parameter Name="planeacion" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="SRIbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Indicadores de Recomendación" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridI" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVIndicadores" runat="server" CellPadding="4"
                                    DataSourceID="SqlDSindicadores"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="Meta,Acumulado,NivelCumplimiento,UsuarioRegistro,FechaRegistro,Usuario,Responsable"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVIndicadores_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdIndicador" HeaderText="Código" SortExpression="IdIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Indicador" HeaderText="Indicador" SortExpression="Indicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MetodologiaNominador" HeaderText="Metodologia Nominador" SortExpression="MetodologiaNominador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MetodologiaDenominador" HeaderText="Metodologia Denominador" SortExpression="MetodologiaDenominador" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:BoundField DataField="Frecuencia" HeaderText="Frecuencia" SortExpression="Frecuencia" HtmlEncodeFormatString="True"/>
                                        <asp:BoundField DataField="año" HeaderText="Año" SortExpression="Año" HtmlEncodeFormatString="True"/> 
                                        <asp:BoundField DataField="planeacion" HeaderText="Planeación" SortExpression="planeacion" HtmlEncodeFormatString="false" ItemStyle-HorizontalAlign="Center"/> 
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Insertar" OnClick="btnInsertarNuevo_Click" />
                    </td>
                </tr>
            </Table>
        </div>
        <div id="BodyFormI" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblIndicador" runat="server" Text="Indicador:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbIndicador" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvIndicador" runat="server" ControlToValidate="txbIndicador"
                                                ErrorMessage="Debe ingresar el Indicador." ToolTip="Debe ingresar el Indicador."
                                                ValidationGroup="indicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblMetodologiaNominador" runat="server" Text="Metodología Nominador:" CssClass="Apariencia" Width="300px" style="height: 13px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbMetodologiaNominador" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvMetodNominador" runat="server" ControlToValidate="txbMetodologiaNominador"
                                                ErrorMessage="Debe ingresar el metodo del nominador." ToolTip="Debe ingresar el metodo del nominador."
                                                ValidationGroup="indicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr >
                        <td class="RowsText">
                            <asp:Label ID="lblMetodologiaDenominador" runat="server" Text="Metodología Denominador:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbMetodologiaDenominador" runat="server" Font-Names="Calibri" Font-Size="Small" 
                                ValidationGroup="indicadores" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="rfvMetodDenominador" runat="server" ControlToValidate="txbMetodologiaDenominador"
                                                ErrorMessage="Debe ingresar el metodo del denominador." ToolTip="Debe ingresar el metodo del denominador."
                                                ValidationGroup="indicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr >
                        <td class="RowsText">
                            <asp:Label ID="lblFrecuencia" runat="server" Text="Frecuencia:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFrecuencia" runat="server" Width="300px">
                                <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Mensual" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Bimestral" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Trimestral" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Cuatrimestral" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Semestral" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Anual" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvFrecuencia" runat="server" ControlToValidate="ddlFrecuencia"
                                                ErrorMessage="Debe seleccionar la frecuencia." ToolTip="Debe seleccionar la frecuencia."
                                                ValidationGroup="indicadores" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                                <asp:Label ID="lblResponsable" runat="server" Text="Responsable:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbResponsable" runat="server" Width="300px" CssClass="Apariencia" ></asp:TextBox>
                            </td>
                        <td>
                            <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
                                <asp:PopupControlExtender ID="popupDependencia4" runat="server" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                    PopupControlID="pnlDependencia4" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                                                    OnClientClick="$find('popup4').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView4" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView4_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup4').hidePopup(); return false;" />
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            <asp:RequiredFieldValidator ID="rfvResponsable" runat="server" ControlToValidate="txbResponsable"
                                                ErrorMessage="Debe seleccionar el responsable." ToolTip="Debe seleccionar el responsable."
                                                ValidationGroup="indicadores" ForeColor="Red" >*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblMeta" runat="server" Text="Meta" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbMeta" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblNivelCumplimiento" runat="server" Text="Nivel Cumplimiento" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbNivelCumplimiento" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lfecha" runat="server" Text="Año de Indicador:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txbAño" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaConsulta" runat="server" Enabled="true" TargetControlID="txbAño"
                            Format="yyyy" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaConsul" runat="server" ControlToValidate="txbAño"
                                    ErrorMessage="Debe ingresar el año del indicador." ToolTip="Debe ingresar el año del indicador."
                                    ValidationGroup="indicadores" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblPlaneacion" runat="server" Text="Planeación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPlaneacion" runat="server" Width="300px">
                                    <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Planeado" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Implementado" Value="I"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                
                        <asp:RequiredFieldValidator ID="rfvPlaneacion" runat="server" ControlToValidate="ddlPlaneacion"
                                    ErrorMessage="Debe seleccionar la Planeación." ToolTip="Debe seleccionar la Planeación."
                                    ValidationGroup="indicadores" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                            
                        </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                            
                        </td>
                        </tr>
                    
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="IBinsertI" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="indicadores" ToolTip="Insertar" Visible="false" OnClick="IBinsertI_Click" />
            <asp:ImageButton ID="IBupdateI" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="indicadores" ToolTip="Actualizar" Visible="false" OnClick="IBupdateI_Click" />
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>