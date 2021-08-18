<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Indicadores.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.Indicadores" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="I" TagName="Indicadores" Src="~/UserControls/MAuditoria/Indicadores.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <I:Indicadores ID="Indicadores1" runat="server" />
</asp:Content>
