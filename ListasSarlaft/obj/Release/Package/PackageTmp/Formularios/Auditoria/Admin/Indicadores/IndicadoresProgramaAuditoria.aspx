<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="IndicadoresProgramaAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.Indicadores.IndicadoresProgramaAuditoria" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCREC" TagName="IndicadorProgramaAuditoria" Src="~/UserControls/MAuditoria/Indicadores/IndicadoresProgramaAuditoria.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCREC:IndicadorProgramaAuditoria ID="IndicadoresProgramaAuditoria1" runat="server" />
</asp:Content>
