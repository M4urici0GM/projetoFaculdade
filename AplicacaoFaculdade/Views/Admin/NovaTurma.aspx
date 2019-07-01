<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="NovaTurma.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.NovaTurma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2>Pessoas</h2>
                <a href="<%: GetRouteUrl("turmas", null) %>" class="btn btn-outline-dark">
                    <i class="fas fa-arrow-left"></i> &nbsp; Voltar
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="row">
                    <asp:LinkButton runat="server" OnClick="Unnamed_Click"></asp:LinkButton>
                    <asp:GridView ID="gridTeste" runat="server"></asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
