<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="IndicadoresResultados.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.Indicadores.IndicadoresResultados" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCREC" TagName="IndicadoresResultados" Src="~/UserControls/MAuditoria/Indicadores/IndicadoresResultados.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCREC:IndicadoresResultados ID="IndicadoresResultados1" runat="server" />
</asp:Content>
