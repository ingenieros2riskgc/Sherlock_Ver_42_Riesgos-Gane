<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="SeguimientoRecomendaciones.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.SeguimientoRecomendaciones" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="SR" TagName="SeguimientoRecomendaciones" Src="~/UserControls/MAuditoria/Reportes/SeguimientoRecomendaciones.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <SR:SeguimientoRecomendaciones ID="SeguimientoRecomendaciones1" runat="server" />
</asp:Content>