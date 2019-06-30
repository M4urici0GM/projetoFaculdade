<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Alunos.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Alunos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        console.log($('#pessoaPesquisaNome').val());
    </script>
    <div class="container-fluid">
        <div class="container">
            <div class="row mt-4">
                <div class="col-12 d-flex justify-content-between align-items-center">
                    <h2> <i class="fas fa-user"></i> Alunos</h2>
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
                    <h4> <i class="fas fa-user-plus"></i> Adicionar um novo aluno: </h4>
                </div>
                <div class="col-12 col-sm-12 col-md-5">
                    <label for="pessoasDropDown">Qual pessoa deseja definir como aluno ?</label>
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
                <div class="col-12 col-sm-12 col-md-5">
                    <label>Pessoa pesquisada: </label>
                    <asp:TextBox CssClass="form-control" ID="pessoaPesquisaNome" runat="server" placeholder="Nome da pessoa" Enabled="false"></asp:TextBox>
                    <small class="text-muted text-center">O nome da pessoa pesquisada ira aparecer aqui.</small> <br />
                    <small class="text-muted text-center">Caso esteja errado, favor verificar os documentos e tentar novamente.</small>
                </div>
                <div class="col-12 col-sm-12 col-md-2">
                    <label>Adicionar aluno</label>
                    <asp:LinKButton runat="server" ID="btnSalvarNovoAluno" OnClick="AdicionarAlunoSaveClickEventHandler" Cssclass="btn btn-outline-dark btn-block">
                        <i class="fas fa-check-square"></i> Salvar
                    </asp:LinKButton>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 tex-center">
                <h4 class="text-center">Alunos cadastrados</h4>
                 <hr />
            </div>
            <div class="col-12 table-responsive mt-2">
                <asp:GridView runat="server" ID="alunosGriView" ShowHeaderWhenEmpty="true" EmptyDataText="Sem registros no sistema." AutoGenerateColumns="false"
                    DataKeyNames="alunoId" OnRowCommand="AlunosRowCommandClickEventHandler" CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" 
                    OnPageIndexChanging="OnPageChangingIndex" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField HeaderText="Nome" DataField="pessoaNome" HeaderStyle-CssClass="text-center"/>
                        <asp:BoundField HeaderText="CPF" DataField="pessoaCpf" HeaderStyle-CssClass="text-center"/>
                        <asp:BoundField HeaderText="Telefone p/ Contato" DataField="pessoaTelefone" HeaderStyle-CssClass="text-center"/>
                        <asp:TemplateField HeaderText="Turmas" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandName="verTurmas" CssClass="btn btn-outline-success">
                                    <i class="fas fa-eye"></i> Verificar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" OnClientClick="return confirmDelete(this)" CommandName="desativarAluno">
                                    <i class="fas fa-user-minus"></i> Excluir
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
        <%--<div class="modal fade" id="adicionarAlunoModal" role="dialog" aria-labelledby="adicionarAlunoModal" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Adicionar Aluno</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group w-100">
                                 <label for="pessoasDropDown">Qual pessoa deseja definir como aluno ?</label>
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
                            <div class="form-group">
                                <label>Pessoa pesquisada: </label>
                                <asp:TextBox CssClass="form-control" ID="pessoaPesquisaNome" runat="server" placeholder="Nome da pessoa" Enabled="false"></asp:TextBox>
                                <small class="text-muted text-center">O nome da pessoa pesquisada ira aparecer aqui.</small> <br />
                                <small class="text-muted text-center">Caso esteja errado, favor verificar os documentos e tentar novamente.</small>
                            </div>
                        </div>
                    </div>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                    <i class="fas fa-times"></i> Fechar
                </button>
                <asp:LinkButton runat="server" OnClick="AdicionarAlunoModalSaveClickEventHandler">
                    <i class="fas fa-check-square"></i> Salvar
                </asp:LinkButton>
              </div>
            </div>
          </div>
        </div>--%>
</asp:Content>
