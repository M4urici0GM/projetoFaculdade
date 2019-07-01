<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Servicos.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Servicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:TextBox ID="editarServicoIdHidden" runat="server" Visible="false"></asp:TextBox>
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-dolly"></i>Serviços </h2>
                <div>
                    <a class="btn btn-outline-dark" href="<%: GetRouteUrl("adminHome", null) %>">
                        <i class="fas fa-arrow-left"></i>Voltar
                    </a>
                    <button class="btn btn-outline-dark" type="button" data-toggle="modal" data-target="#adicionarServicoModal">
                        <i class="fas fa-plus"></i>&nbsp; Adicionar
                    </button>
                </div>
            </div>
        </div>
        <hr />
        <div class="col-12 table-responsive">
            <asp:GridView ID="servicosGridView" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema!"
                AutoGenerateColumns="false" DataKeyNames="servicoId" OnRowCommand="OnRowCommandClickEventHandler"
                CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true"
                OnPageIndexChanging="OnPageIndexChanged" PagerStyle-CssClass="pagination-ys">
                <Columns>
                    <asp:BoundField DataField="servicoNome" HeaderStyle-CssClass="text-center" HeaderText="Nome" />
                    <asp:BoundField DataField="precoServicoValor" DataFormatString="{0:F}" HeaderStyle-CssClass="text-center" HeaderText="Preço (R$)" />
                    <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="editarServico" runat="server" CommandName="editarServico" CssClass="btn btn-outline-primary">
                                <i class="fas fa-user-cog"></i> Editar
                            </asp:LinkButton>
                            <asp:LinkButton ID="excluirServico" runat="server" CommandName="excluirServico" OnClientClick="return confirmDelete(this)" CssClass="btn btn-outline-danger">
                                <i class="fas fa-user-minus"></i> Excluir
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="modal fade" id="adicionarServicoModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle"><i class="fas fa-plus"></i> Adicionar Serviço</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i> 
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="servicoNome">Nome do serviço: </label>
                            <asp:TextBox runat="server" ID="servicoNome" CssClass="form-control" required="true" placeholder="Ex: Musculação"/>
                            <small class="text-muted">Digite o nome do serviço (Obrigatório)</small>
                        </div>
                        <div class="form-group">
                            <label for="precoServicoValor">Digite o valor (Mensal):</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">R$</span>
                                </div>
                                <asp:TextBox runat="server" ID="precoServicovalor" CssClass="form-control" type="number" placeholder="15.99"/>
                            </div>
                            <small class="text-muted">Insira o valor mensal do serviço, (em reais, ex: 15.00)</small>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" ID="adicionarServicoBtn" OnClick="OnAdicionarServicoClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="editarServicoModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarServicoModalTitle"><i class="fas fa-cogs"></i> Editar Serviço</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i> 
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="servicoNome">Nome do serviço: </label>
                            <asp:TextBox runat="server" ID="editarServicoNome" CssClass="form-control" required="true" placeholder="Ex: Musculação"/>
                            <small class="text-muted">Digite o nome do serviço (Obrigatório)</small>
                        </div>
                        <div class="form-group">
                            <label for="precoServicoValor">Digite o valor (Mensal):</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">R$</span>
                                </div>
                                <asp:TextBox runat="server" ID="editarServicoValor" CssClass="form-control" type="number" placeholder="15.99"/>
                            </div>
                            <small class="text-muted">Insira o valor mensal do serviço, (em reais, ex: 15.00)</small>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" ID="editarServicoSalvarBtn" OnClick="OnSalvarServicoClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
