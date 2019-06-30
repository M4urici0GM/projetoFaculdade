<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="EditarPessoa.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.EditarPessoa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:TextBox runat="server" ID="pessoaIdHidden" Visible="false" />
    <asp:TextBox runat="server" ID="usuarioIdHidden" Visible="false" />
    <div class="container mb-4">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-user-plus"></i> Cadastrar pessoa</h2>
                <div>
                    <a href="<%: GetRouteUrl("pessoas", null) %>" class="btn btn-outline-dark">
                        <i class="fas fa-arrow-left"></i>&nbsp; Voltar
                    </a>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mt-4">
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNome">Nome<span class="text-danger">*</span>:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaNome" CssClass="form-control" placeholder="Nome da pessoa" />
                    <small class="text-muted">Qual o nome ?</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaSobrenome">Sobrenome<span class="text-danger">*</span>:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaSobrenome" CssClass="form-control" placeholder="Nome da pessoa" />
                    <small class="text-muted">Qual o sobrenome ?</small>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNome">Sexo<span class="text-danger">*</span>:</label>
                    <asp:DropDownList runat="server" ID="pessoaSexo" CssClass="form-control">
                        <asp:ListItem Text="Masculino" Value="1" />
                        <asp:ListItem Text="Feminino" Value="0" />
                        <asp:ListItem Text="Não binário" Value="null" />
                    </asp:DropDownList>
                    <small class="text-muted">Qual o sexo da pessoa ?</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNascimento">Nascimento<span class="text-danger">*</span>:</label>
                    <div class="input-group date" id="datetimepicker4" data-target-input="nearest">
                        <asp:TextBox runat="server" ID="pessoaNascimento" CssClass="form-control datetimepicker-input" data-target="#datetimepicker4" placeholder="Data de nascimento" />
                        <div class="input-group-append" data-target="#datetimepicker4" data-toggle="datetimepicker">
                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                        </div>
                    </div>
                    <small class="text-muted">Qual a data de nascimento ?</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4><i class="fas fa-phone"></i> Contato </h4>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaSobrenome">Telefone*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaTelefone" CssClass="form-control" placeholder="Telefone (opcional)" />
                    <small class="text-muted">Insira um telefone para contato.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaCelular">Telefone*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCelular" CssClass="form-control" placeholder="Celular (opcional)" />
                    <small class="text-muted">Insira um numero de celular para contato.</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4><i class="fas fa-file-alt"></i> Documentação</h4>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaRg">RG*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaRg" CssClass="form-control" placeholder="RG" required="true" />
                    <small class="text-muted">Digite o RG (obrigatório).</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCpf">CPF:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCpf" CssClass="form-control" placeholder="CPF" required="true" />
                    <small class="text-muted">Digite o CPF.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCnpj">CNPJ:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCnpj" CssClass="form-control" placeholder="CNPJ (Opcional)" required="true" />
                    <small class="text-muted">Digite o CNPJ se for jurídica.</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4><i class="fas fa-map-marked-alt"></i> Endereço</h4>
            </div>
            <div class="col-12">
                <div class="form-group">
                    <label for="pessoaCep">CEP:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCep" CssClass="form-control" placeholder="Insira o CEP" required="true" onchange="getCEP(this)" />
                    <small class="text-muted">Digite o CEP.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-8">
                <div class="form-group">
                    <label for="pessoaEndereco">Logradouro:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaEndereco" CssClass="form-control" placeholder="Endereço da casa" required="true" />
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaNumeroRua">Numero:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaNumeroRua" placeholder="Numero da casa" CssClass="form-control" required="true" />
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaComplemento">Complemento:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaComplemento" placeholder="Complemento (Opcional)" CssClass="form-control" required="true" />
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCidade">Cidade:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCidade" placeholder="Cidade" CssClass="form-control" required="true" />
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaEstado">Estado:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaEstado" placeholder="Estado" CssClass="form-control" required="true" />
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h4><i class="fas fa-user"></i> Usuarios</h4>
                <button  class="btn btn-outline-dark" id="adicionarUsuarioBtn" onclick="event.preventDefault()" data-toggle="modal" data-target="adicionarUsuarioModal">
                    <i class="fas fa-user-plus"></i>Adicionar
                </button>
            </div>
            <div class="col-12 mt-2">
                <asp:GridView ID="pessoaUsuariosGridView" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema!" AutoGenerateColumns="false" DataKeyNames="usuarioId" OnRowCommand="OnRowCommandEventHandler"
                    CssClass="table table-borderless table-hover text-center border-0">
                    <Columns>
                        <asp:BoundField DataField="usuarioLogin" HeaderStyle-CssClass="text-center" HeaderText="Usuario" />
                        <asp:BoundField DataField="nivelAcessoNome" HeaderStyle-CssClass="text-center" HeaderText="Nível de acesso" />
                        <asp:BoundField DataField="usuarioDataRegistro" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd/MM/yyyy HH:mm:ss:tt}" HeaderText="Data de cadastro" />
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="editButton" runat="server" CommandName="editarUsuario" CssClass="btn btn-outline-primary">
                                    <i class="fas fa-user-cog"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="delButton" runat="server" CommandName="excluirUsuario" OnClientClick="return confirmDelete(this)" CssClass="btn btn-outline-danger btnExcluirPessoa">
                                    <i class="fas fa-user-minus"></i> Excluir
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />
        <div class="row mt-4">
            <div class="col-12 col-md-6 offset-md-6">
                <div class="row">
                    <div class="col-6">
                        <a class="btn btn-outline-dark btn-block" href="#">
                            <i class="fas fa-arrow-left"></i>&nbsp; Voltar
                        </a>
                    </div>
                    <div class="col-6">
                        <asp:LinkButton runat="server" OnClick="EditarPessoaClickEventHandler" class="btn btn-outline-primary btn-block">
                            <i class="fas fa-check-square"></i> &nbsp; Salvar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="adicionarUsuarioModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Adicionar Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="col-12">
                            <div class="form-group">
                                <label for="usuarioLogin">Email:</label>
                                <asp:TextBox runat="server" ID="usuarioLogin" CssClass="form-control" placeholder="email@exemplo.com" />
                                <small class="text-muted">Será o email para ele fazer login. (Obrigatório)</small>
                            </div>
                            <div class="form-group">
                                <label for="usuarioLogin">Senha:</label>
                                <asp:TextBox runat="server" ID="usuarioSenha" CssClass="form-control" type="password" placeholder="Senha do Usuario" />
                                <small class="text-muted">Será o email para ele fazer login. (Obrigatório)</small>
                            </div>
                            <div class="form-group">
                                <label for="nivelAcessoDropDown">Nivel de acesso:</label>
                                <asp:DropDownList ID="nivelAcessoDropDown" runat="server" CssClass="form-control" placeholder="Obrigatorio">
                                    <asp:ListItem Text="Selecione um item..." Enabled="false" Selected="true" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <small class="text-muted">Qual o nivel de acesso desse usuario ?</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-secondary">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" OnClick="AdicionarUsuarioClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="editarUsuarioModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarUsuarioModalTitle">Editar Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="col-12">
                            <div class="form-group">
                                <label for="usuarioLogin">Email:</label>
                                <asp:TextBox runat="server" ID="editarUsuarioLogin" CssClass="form-control" placeholder="email@exemplo.com" />
                                <small class="text-muted">Será o email para ele fazer login. (Obrigatório)</small>
                            </div>
                            <div class="form-group">
                                <label for="usuarioLogin">Senha:</label>
                                <asp:TextBox runat="server" ID="editarUsuarioSenha" CssClass="form-control" type="password" placeholder="Nova senha" />
                                <small class="text-muted">Senha de login. (Obrigatório)</small>
                            </div>
                            <div class="form-group">
                                <label for="editarNivelAcessoDropDown">Nivel de acesso:</label>
                                <asp:DropDownList ID="editarNivelAcessoDropDown" runat="server" CssClass="form-control" placeholder="Obrigatorio">
                                    <asp:ListItem Text="Selecione um item..." Enabled="false" Selected="true" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <small class="text-muted">Qual o nivel de acesso desse usuario ?</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-secondary">
                        <i class="fas fa-times"></i> Fechar
                    </button>
                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary" OnClick="EditarUsuarioClickEventHandler">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
