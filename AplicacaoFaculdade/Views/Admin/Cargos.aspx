<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Cargos.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Cargos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:TextBox ID="editarCargoIdHidden" runat="server" Visible="false" />
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-users-cog"></i>Cargos</h2>
                <button class="btn btn-outline-dark" type="button" data-toggle="modal" data-target="#adicionarCargoModal">
                    <i class="fas fa-plus"></i>&nbsp; Cadastrar
                </button>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="cargosGridView" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema!" AutoGenerateColumns="false" DataKeyNames="cargoId" OnRowCommand="OnRowCommandClickEventHandler"
                    CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanged" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="cargoNome" HeaderStyle-CssClass="text-center" HeaderText="Nome" />
                        <asp:BoundField DataField="cargoSalario" HeaderStyle-CssClass="text-center" HeaderText="Salario Atual" />
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="editarCargo" runat="server" CommandName="editarCargo" CssClass="btn btn-outline-primary">
                                    <i class="fas fa-user-cog"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="excluirCargo" runat="server" CommandName="excluirCargo" OnClientClick="return confirmDelete(this)" CssClass="btn btn-outline-danger">
                                    <i class="fas fa-user-minus"></i> Excluir
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="modal fade" id="adicionarCargoModal" tabindex="-1" role="dialog" aria-labelledby="adicionarCargoModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">
                        <i class="fas fa-users-cog"></i> Adicionar novo cargo
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <label for="cargoNome">Nome:</label>
                                <asp:TextBox runat="server" ID="cargoNome" CssClass="form-control" placeholder="Ex: Instrutor"></asp:TextBox>
                                <small class="text-muted">Qual sera o nome desse cargo ?</small>
                            </div>
                            <div class="col-12 mt-2">
                                <label for="cargoSalario">Salario:</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">R$</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="cargoSalario" CssClass="form-control" placeholder="0.00" type="number"></asp:TextBox>
                                </div>
                                <small class="text-muted">Qual o salario atual desse cargo ?</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" ID="btnAdicionarNovoCargo" OnClick="OnAdicionarCargoClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="editarCargoModal" tabindex="-1" role="dialog" aria-labelledby="adicionarCargoModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarCargoModalTitle">
                        <i class="fas fa-users-cog"></i> Adicionar novo cargo
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <label for="cargoNome">Nome:</label>
                                <asp:TextBox runat="server" ID="editarCargoNome" CssClass="form-control" placeholder="Ex: Instrutor"></asp:TextBox>
                                <small class="text-muted">Qual sera o nome desse cargo ?</small>
                            </div>
                            <div class="col-12 mt-2">
                                <label for="cargoSalario">Salario:</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">R$</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="editarCargoSalario" CssClass="form-control" placeholder="0.00" type="number"></asp:TextBox>
                                </div>
                                <small class="text-muted">Qual o salario atual desse cargo ?</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" ID="editarSalarioButton" OnClick="OnEditarCargoClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
