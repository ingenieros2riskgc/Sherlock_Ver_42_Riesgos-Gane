<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="ReportePlanAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.ReportePlanAuditoria" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RPA" TagName="ReportePlanAuditoria" Src="~/UserControls/MAuditoria/Reportes/ReportePlanAuditoria.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RPA:ReportePlanAuditoria ID="ReportePlanAuditoria1" runat="server" />
</asp:Content>