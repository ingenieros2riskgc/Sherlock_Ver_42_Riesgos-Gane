<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="ReporteIndicadores.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.ReporteIndicadores" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RI" TagName="ReporteIndicadores" Src="~/UserControls/MAuditoria/Reportes/ReporteIndicadores.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <Ri:ReporteIndicadores ID="ReporteIndicadores1" runat="server" />
</asp:Content>