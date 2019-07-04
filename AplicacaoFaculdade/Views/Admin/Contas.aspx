<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Contas.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Contas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-piggy-bank"></i> Contas</h2>
                <button type="button" class="btn btn-outline-dark" data-toggle="modal" data-target="#adicionarContaModal">
                    <i class="fas fa-plus"></i> Adicionar
                </button>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView runat="server" CssClass="table table-borderless border-0 text-center" AutoGenerateColumns="false"
                    OnRowCommand="OnRowCommandEventHandler" ID="contasGridView" DataKeyNames="contaId" ShowHeaderWhenEmpty="true" EmptyDataText="Ainda nao ha contas">
                    <Columns>
                        <asp:BoundField HeaderText="Nome" DataField="contaNome" />
                        <asp:BoundField HeaderText="Saldo" DataField="contaSaldo" DataFormatString="R$ {0:F}" />
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" OnClientClick="return confirmDelete(this)" CommandName="excluirConta">
                                    <i class="fas fa-times"></i> Remover
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal fade" id="adicionarContaModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">
                        <i class="fas fa-plus"></i> Adicionar Conta
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <label for="contaNome">Nome da conta</label>
                                    <asp:TextBox runat="server" ID="contaNome" required="true" CssClass="form-control" placeholder="Banco"/>
                                    <small class="text-muted">Dê um nome à essa conta</small>
                                </div>
                                <div class="form-group">
                                    <label for="contaSaldo">Saldo inicial</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">R$</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="contaSaldo" required="true" type="number" CssClass="form-control" placeholder="0.00" value="0"/>
                                    </div>
                                    <small class="text-muted">Ela terá saldo inicial?</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" OnClick="OnAdicionarContaClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
