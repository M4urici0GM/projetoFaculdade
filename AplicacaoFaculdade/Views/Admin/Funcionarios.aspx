<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Funcionarios.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Funcionarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="row mt-4">
                <div class="col-12 d-flex justify-content-between align-items-center">
                    <h2> <i class="fas fa-user-tie"></i> Funcionarios</h2>
                    <a class="btn btn-outline-dark" href="<%: GetRouteUrl("adminHome", null) %>">
                        <i class="fas fa-arrow-left"></i> Voltar
                    </a>
                </div>
            </div>
        </div>
        <hr />
        <div class="col-12">
            <div class="row">
                <div class="col-12">
                    <h4> <i class="fas fa-user-plus"></i> Adicionar um novo funcionario: </h4>
                </div>
                <div class="col-12 col-sm-12 col-md-3">
                    <label for="pessoasDropDown">Pesquisar uma pessoa</label>
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="pesquisaPessoaIdHidden" Visible="false"></asp:TextBox>
                        <asp:TextBox runat="server" CssClass="form-control" type="number" MaxLength="11" ID="pessoaPesquisaTxt" placeholder="CPF / RG"/>
                        <div class="input-group-append">
                            <asp:LinkButton runat="server" ID="pesquisarPessoaBtn" OnClick="PesquisarPessoaClickEventHandler" CssClass="btn btn-outline-primary">
                                <i class="fas fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <small class="text-muted">Digite um documento e clique na lupa.</small>
                </div>
                <div class="col-12 col-sm-12 col-md-3">
                    <label>Pessoa pesquisada: </label>
                    <asp:TextBox CssClass="form-control" ID="pessoaPesquisaNome" runat="server" placeholder="Nome da pessoa" Enabled="false"></asp:TextBox>
                    <small class="text-muted text-center">O nome da pessoa pesquisada ira aparecer aqui.</small> <br />
                    <small class="text-muted text-center">Caso esteja errado, favor verificar os documentos e tentar novamente.</small>
                </div>
                <div class="col-12 col-sm-12 col-md-3">
                    <label>Selecione um cargo:</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="cargoDropDown" placeholder="Selecione um cargo"></asp:DropDownList>
                    <small class="text-muted">Selecione o cargo que esse funcionario ira exercer</small>
                </div>
                <div class="col-12 col-sm-12 col-md-3">
                    <label>Adicionar aluno</label>
                    <asp:LinKButton runat="server" ID="btnSalvarNovoFuncionario" OnClick="AdicionarFuncionarioSaveClickEventHandler" Cssclass="btn btn-outline-dark btn-block">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinKButton>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 tex-center">
                <h4 class="text-center">Funcionarios cadastrados</h4>
                 <hr />
            </div>
            <div class="col-12 table-responsive mt-2">
                <asp:GridView runat="server" ID="funcionarioGridView" ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema." AutoGenerateColumns="false"
                    DataKeyNames="funcionarioId" OnRowCommand="OnRowCommandClickEventHandler" CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" 
                    OnPageIndexChanging="OnPageChangingIndex" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField HeaderText="Nome" DataField="pessoaNome" HeaderStyle-CssClass="text-center"/>
                        <asp:BoundField HeaderText="CPF" DataField="pessoaCpf" DataFormatString="{0:#########-##}" HeaderStyle-CssClass="text-center"/>
                        <asp:BoundField HeaderText="Telefone p/ Contato" DataField="pessoaTelefone" DataFormatString="{0:(##)####-####}" HeaderStyle-CssClass="text-center"/>
                        <asp:BoundField HeaderText="celular p/ Contato" DataField="pessoaCelular" DataFormatString="{0:(##)#####-####}" HeaderStyle-CssClass="text-center"/>
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" OnClientClick="return confirmDelete(this)" CommandName="excluirFuncionario">
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
