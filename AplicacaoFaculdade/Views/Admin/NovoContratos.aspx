<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="NovoContratos.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.NovoContratos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-file-alt"></i> Novo contrato</h2>
                <a href="<%: GetRouteUrl("contratos", null) %>" class="btn btn-outline-dark">
                    <i class="fas fa-arrow-left"></i> &nbsp; Voltar
                </a>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <div class="row">
                    <div class="col-12 d-flex justify-content-between">
                        <div>
                            <h4> Procurar aluno</h4>
                            <small class="text-muted">Digite o documento do aluno e clique na lupa, e por fim no botão ao lado para continuar</small>
                        </div>
                        <div>
                            <asp:LinkButton CssClass="btn btn-outline-primary" runat="server">
                                <i class="fas fa-check"></i> Aceitar
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-12 col-sm-12 col-md-6">
                        <div class="form-group">
                            <label>Pesquisar Aluno</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="pesquisarAlunoTextBox" CssClass="form-control" placeholder="Documento (CPF / RG / CNPJ) do aluno" />
                                <div class="input-group-append">
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" ID="pesquisarAlunoBtn" >
                                        <i class="fas fa-search"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-12 col-md-6">
                        <div class="form-group">
                            <label>Pesquisar Aluno</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="pessoaPesquisaNome" CssClass="form-control" placeholder="Nome da pessoa pesquisada" Enabled="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mt-3">
            <div class="col-12">
                <div class="row">
                    <div class="col-12 d-flex justify-content-between">
                        <div>
                            <h4><i class="fas fa-dolly"></i> Serviços</h4>
                        </div>
                        <div>
                            <asp:LinkButton runat="server" CssClass="btn btn-outline-dark">
                                <i class="fas fa-check"></i> Adicionar
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-8">
                        <label>Selecione um serviço:</label>
                        <asp:DropDownList runat="server" ID="servicosDropDown" AutoPostBack="true" OnSelectedIndexChanged="servicosDropDown_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                        <small class="text-muted">Selecione um serviço (Obrigatório)</small>
                    </div>
                    <div class="col-12 col-sm-12 col-md-4">
                        <label>Mensalidade:</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">R$</span>
                            </div>
                            <asp:TextBox runat="server" ID="servicoMensalidadeValor" CssClass="form-control" Enabled="false"/>
                        </div>
                        <small class="text-muted">Preço atual da mensalidade.</small>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-12">
                <asp:GridView runat="server" ID="servicosGridView" ShowHeaderWhenEmpty="true" CssClass="table table-hover table-borderless border-0 text-center"
                    EmptyDataText="Selecione um serviço e clique em adicionar" OnRowCommand="OnRowCommandEventHandler" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Serviço" DataField="servicoNome" />
                         <asp:BoundField DataField="servicoPreco" DataFormatString="R$ {0:F}" HeaderStyle-CssClass="text-center" HeaderText="Mensalidade" />
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="removerServiço">
                                    <i class="fas fa-times"></i> Remover
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-6 offset-md-6">
                <div class="row">
                    <div class="col-12">
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Valor total R$</span>
                                </div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="valorTotal" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Desconto R$</span>
                                </div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="desconto" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mt-3">
            <div class="col-12 col-sm-12 col-md-6 offset-md-6">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-6">
                        <asp:LinkButton runat="server" class="btn btn-outline-dark btn-block">
                            <i class="fas fa-arrow-left"></i> Voltar
                        </asp:LinkButton>
                    </div>
                    <div class="col-12 col-sm-12 col-md-6">
                        <asp:LinkButton runat="server" class="btn btn-outline-primary btn-block">
                            <i class="fas fa-check"></i> Salvar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
