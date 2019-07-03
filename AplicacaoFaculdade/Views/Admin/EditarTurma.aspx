<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="EditarTurma.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.EditarTurma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        window.onload = () => {
            $('#dateTimePickerHorario1').datetimepicker({
                format: 'LT'
            });
            $('#dateTimePickerHorario2').datetimepicker({
                format: 'LT'
            });
        }
    </script>
    <div class="container">
        <div class="row mt-4">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2><i class="fas fa-plus"></i>  Cadastrar nova turma</h2>
                <a href="<%: GetRouteUrl("turmas", null) %>" class="btn btn-outline-dark">
                    <i class="fas fa-arrow-left"></i> &nbsp; Voltar
                </a>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 col-sm-12 col-md-3">
                <div class="form-group">
                    <label for="turmaNome">Nome da turma:</label>
                    <asp:TextBox runat="server" ID="turmaNome" CssClass="form-control" placeholder="Turma C1"></asp:TextBox>
                    <small class="text-muted">Dê um nome para a turma, facilitando a identificação :) (Obrigatório)</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-3">
                <div class="form-group">
                    <label for="turmaMax">Quantia max. de alunos</label>
                    <asp:TextBox runat="server" ID="turmaMax" CssClass="form-control" type="number" placeholder="10"></asp:TextBox>
                    <small class="text-muted">Qual a quantidade máxima de alunos nessa turma ?(Obrigatório)</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-3">
                <label for="turmaServico">Serviço:</label>
                <asp:DropDownList runat="server" ID="servicoDropDown" CssClass="form-control"></asp:DropDownList>
                <small class="text-muted">Qual serviço essa turma irá oferecer ? (Obrigatório)</small>
            </div>
            <div class="col-12 col-sm-12 col-md-3">
                <label for="turmaServico">Instrutor:</label>
                <asp:DropDownList runat="server" ID="funcionarioDropDown" CssClass="form-control"></asp:DropDownList>
                <small class="text-muted">Qual instutor dará aula ? (Obrigatório)</small>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-content-center">
                    <h4><i class="fas fa-calendar"></i> Horarios</h4>
                </div>
                <small class="text-muted">Aqui você irá adicionar os horarios que essa turma irá ter aula, não se preocupe, poderá fazer isso depois. :)</small> <br /> 
                <small class="text-muted">Selecione o dia da semana, o horario de entrada, saida e clique no botão ao lado, é possivel adicionar multiplos horários.</small>
            </div>            
        </div>
        <div class="row mt-2">
            <div class="col-12 col-sm-12">
                <label>Dia da semana:</label>
                <asp:ListBox runat="server" ID="turmaHorarioDiaList" SelectionMode="Multiple" CssClass="form-control">
                    <asp:ListItem Value="1">Domingo</asp:ListItem>
                    <asp:ListItem Value="2">Segunda</asp:ListItem>
                    <asp:ListItem Value="3">Terça</asp:ListItem>
                    <asp:ListItem Value="4">Quarta</asp:ListItem>
                    <asp:ListItem Value="5">Quinta</asp:ListItem>
                    <asp:ListItem Value="6">Sexta</asp:ListItem>
                    <asp:ListItem Value="7">Sabado</asp:ListItem>
                </asp:ListBox>
                <small class="text-muted">Qual dia da semana ?</small>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <label for="turmaHorarioInicio">Entrada<span class="text-danger">*</span>:</label>
                <div class="input-group date" id="dateTimePickerHorario1" data-target-input="nearest">
                    <asp:TextBox runat="server" id="turmaHorarioInicio" Cssclass="form-control datetimepicker-input" data-target="#datetimepicker4" placeholder="Horario de entrada"/>
                    <div class="input-group-append" data-target="#dateTimePickerHorario1" data-toggle="datetimepicker">
                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                    </div>
                </div>
                <small class="text-muted">Qual o horario de entrada?</small>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <label for="turmaHorarioFim">Saida<span class="text-danger">*</span>:</label>
                <div class="input-group date" id="dateTimePickerHorario2" data-target-input="nearest">
                    <asp:TextBox runat="server" id="turmaHorarioFim" Cssclass="form-control datetimepicker-input" data-target="#datetimepicker4" placeholder="Horario de saida"/>
                    <div class="input-group-append" data-target="#dateTimePickerHorario2" data-toggle="datetimepicker">
                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                    </div>
                </div>
                <small class="text-muted">E o de saida ?</small>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col-12">
                <asp:LinkButton runat="server"  OnClick="AdicionarHorariosClickEventHandler" ID="adicionarNovoHorario" CssClass="btn btn-outline-dark float-right btn-block">
                <i class="fas fa-plus"></i> Salvar
            </asp:LinkButton>
            </div>
        </div>
        <hr />
        <div class="row mt-3">
            <div class="col-12 table-responsive">
                <asp:GridView runat="server" EmptyDataText="Adicione um horario clicando em 'adicionar'." ID="horariosGridView"
                    AllowPaging="false" ShowHeaderWhenEmpty="true" OnRowCommand="OnHorariosRowCommandEventHandler"
                    CssClass="table table-hover text-center border-0 mt-2 table-borderless" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Horario Início" DataFormatString="{0}" DataField="turmaHorarioInicio"/>
                        <asp:BoundField HeaderText="Horario Saida" DataFormatString="{0}" DataField="turmaHorarioFim"/>
                        <asp:BoundField HeaderText="Dia" DataField="turmaHorarioDiaSemana"/>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="excluirHorario">
                                    <i class="fas fa-times"></i> Remover
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4><i class="fas fa-users"></i> Alunos</h4>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-12 table-responsive">
                <asp:GridView runat="server" EmptyDataText="Essa turma ainda nao tem alunos." ID="alunosGridView"
                    AllowPaging="false" ShowHeaderWhenEmpty="true" OnRowCommand="OnAlunosRowCommandEventHandler"
                    CssClass="table table-hover text-center border-0 mt-2 table-borderless" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Nome" DataField="pessoaNome"/>
                        <asp:BoundField HeaderText="Telefone p/ Contato" DataField="pessoaTelefone"/>
                        <asp:BoundField HeaderText="Celular p/ Contato" DataField="pessoaCelular"/>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="removerAluno">
                                    <i class="fas fa-times"></i> Remover
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 mb-3">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-6 offset-md-6">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-6">
                            <a class="btn btn-outline-dark btn-block" href="<%: GetRouteUrl("turmas", null) %>">
                                <i class="fas fa-arrow-left"></i> Voltar
                            </a>
                        </div>
                        <div class="col-12 col-sm-12 col-md-6">
                           <asp:LinkButton runat="server" OnClick="EditarTurmaClickEventHandler" CssClass="btn btn-outline-primary btn-block">
                               <i class="fas fa-check-square"></i> Salvar
                           </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
