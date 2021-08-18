<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmReporteSeguimientoRiesgos.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReporteSeguimientoRiesgos" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCRS" TagName="ReporteSeguimientoRiesgos" Src="~/UserControls/MAuditoria/ReporteSeguimientoRiesgos.ascx" %>
<%@ PreviousPageType VirtualPath="~/Formularios/Auditoria/Admin/AudAdmSeguimiento.aspx" %> 
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRS:ReporteSeguimientoRiesgos ID="ReporteSeguimientoRiesgos1" runat="server" />
</asp:Content>
