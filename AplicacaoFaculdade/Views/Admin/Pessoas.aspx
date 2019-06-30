<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Pessoas.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Pessoas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="row mt-4">
               <div class="col-12 d-flex justify-content-between align-items-center">
                   <h2>Pessoas</h2>
                   <a href="<%: GetRouteUrl("novaPessoa", null) %>" class="btn btn-outline-dark">
                       <i class="fas fa-user-plus"></i> &nbsp; Cadastrar
                   </a>
               </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="pessoaGridView" runat="server"  ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema!" AutoGenerateColumns="false" DataKeyNames="pessoaId" OnRowCommand="OnRowCommandEventHandler" 
                    CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" OnPageIndexChanging="OnPageChangingIndex" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="pessoaNome" HeaderStyle-CssClass="text-center"  HeaderText="Nome"/>
                        <asp:BoundField DataField="pessoaSobrenome" HeaderStyle-CssClass="text-center"  HeaderText="Sobrenome" />
                        <asp:BoundField DataField="pessoaTelefone" HeaderStyle-CssClass="text-center"  HeaderText="Telefone" />
                        <asp:BoundField DataField="pessoaCelular" HeaderStyle-CssClass="text-center" NullDisplayText="--"  HeaderText="Celular" />
                        <asp:BoundField DataField="pessoaCidade" HeaderStyle-CssClass="text-center" NullDisplayText="--"  HeaderText="Cidade" />
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center" >
                            <ItemTemplate>
                                <asp:LinkButton ID="editButton" runat="server" CommandName="editarPessoa" CssClass="btn btn-outline-primary">
                                    <i class="fas fa-user-cog"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="delButton" runat="server" CommandName="excluirPessoa" OnClientClick="return confirmDelete(this)"  CssClass="btn btn-outline-danger btnExcluirPessoa">
                                    <i class="fas fa-user-minus"></i> Excluir
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    
</asp:Content>
