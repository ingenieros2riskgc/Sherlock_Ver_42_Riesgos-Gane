<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="IndicadorRecomendaciones.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.IndicadorRecomendaciones" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="IR" TagName="IndicadorRecomendaciones" Src="~/UserControls/MAuditoria/Reportes/IndicadorRecomendaciones.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <IR:IndicadorRecomendaciones ID="IndicadorRecomendaciones1" runat="server" />
</asp:Content>