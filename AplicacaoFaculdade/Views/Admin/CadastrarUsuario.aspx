<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="CadastrarUsuario.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.CadastrarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <div class="container">
        <div class="row mt-4">
           <div class="col-12 d-flex justify-content-between align-items-center">
               <h2>Cadastar usuario</h2>
               <a href="<%: GetRouteUrl("usuarios", null) %>" class="btn btn-outline-dark">
                   <i class="fas fa-arrow-left"></i> &nbsp; Voltar
               </a>
           </div>
        </div>
        <hr />
        <div class="row mt-4">
            <div class="col-12">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="usuarioLogin" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
