<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="IndicadorSeguimentoRecomendacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.Indicadores.IndicadorSeguimentoRecomendacion" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCREC" TagName="IndicadorSeguimentoRecomendacion" Src="~/UserControls/MAuditoria/Indicadores/IndicadorSeguimentoRecomendacion.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCREC:IndicadorSeguimentoRecomendacion ID="IndicadorSeguimentoRecomendacion1" runat="server" />
</asp:Content>
