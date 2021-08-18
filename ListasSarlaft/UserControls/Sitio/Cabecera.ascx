<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cabecera.ascx.cs" Inherits="ListasSarlaft.UserControls.Cabecera" %>
<table width="100%" bgcolor="#D2D3D5">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="left" width="15%">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Logos/LogoSE.gif" />
                    </td>
                    <td align="center" width="70%">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Logos/LogoSherlock.jpg"
                            Width="200px" />
                    </td>
                    <td align="right" width="15%">
                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Logos/Risk2.png" Width="150px" />--%>
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Logos/Risk.png" Width="110px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
