<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Turmas.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Turmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="row mt-4">
               <div class="col-12 d-flex justify-content-between align-items-center">
                   <h2>Pessoas</h2>
                   <a href="<%: GetRouteUrl("novaTurma", null) %>" class="btn btn-outline-dark">
                       <i class="fas fa-plus-square"></i> &nbsp; Cadastrar
                   </a>
               </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="turmasGridView" runat="server"  ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema!" AutoGenerateColumns="false" DataKeyNames="pessoaId" OnRowCommand="OnRowCommandEventHandler" 
                    CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" OnPageIndexChanging="OnPageChangingIndex" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="turmaNome" HeaderStyle-CssClass="text-center"  HeaderText="Nome"/>
                        <asp:BoundField DataField="pessoaNome" HeaderStyle-CssClass="text-center"  HeaderText="Professor"/>
                        <asp:BoundField DataField="turmaMax" HeaderStyle-CssClass="text-center"  HeaderText="Tamanho da turma"/>
                        <asp:BoundField DataField="servicoNome" HeaderStyle-CssClass="text-center"  HeaderText="Serviço"/>
                        <asp:TemplateField HeaderText="Dias/Horarios">
                            <ItemTemplate>
                                <asp:LinkButton ID="editButton" runat="server" CommandName="editarPessoa" CssClass="btn btn-outline-primary">
                                    <i class="fas fa-eye"></i> Verificar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
