<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="MovimentoFinanceiro.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.MovimentoFinanceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="container">
                <div class="row mt-4">
                    <div class="col-12 d-flex justify-content-between align-items-center">
                        <h2><i class="fas fa-piggy-bank"></i> Movimento Financeiro</h2>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mt-3">
            <div class="col-12">
                <div class="container">
                    <div class="row">
                        <div class="col-12 d-flex justify-content-between align-items-center">
                            <div>
                                <small class="text-muted">Para filtar os dados, preencha os campos abaixo e clique no botão ao lado.</small>
                            </div>
                            <div>
                                <asp:LinkButton runat="server" ID="filtrarBtn" CssClass="btn btn-block btn-outline-dark">
                                    <i class="fas fa-filter"></i> Filtrar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-12 col-sm-12 col-md-3">
                            <div class="form-group">
                                <label for="pessoaNascimento">Início<span class="text-danger">*</span>:</label>
                                <div class="input-group date" id="filtroDataInicialPicker" data-target-input="nearest">
                                    <asp:TextBox runat="server" id="filtroDataInicial" Cssclass="form-control datetimepicker-input" data-target="#filtroDataInicialPicker" placeholder="Data de nascimento"/>
                                    <div class="input-group-append" data-target="#filtroDataInicialPicker" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                                <small class="text-muted">Qual a data inicial ?</small>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-3">
                            <div class="form-group">
                                <label for="pessoaNascimento">Fim<span class="text-danger">*</span>:</label>
                                <div class="input-group date" id="filtroDataFinalPicker" data-target-input="nearest">
                                    <asp:TextBox runat="server" id="filtroDataFinal" Cssclass="form-control datetimepicker-input" data-target="#filtroDataFinalPicker" placeholder="Data de nascimento"/>
                                    <div class="input-group-append" data-target="#filtroDataFinalPicker" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                                <small class="text-muted">Qual a data final ?</small>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-2">
                            <div class="form-group">
                                <label>Qual Tipo ?</label>
                                <asp:DropDownList runat="server" ID="movimentoTipoDropDown" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Pagamento" />
                                    <asp:ListItem Value="2" Text="Recebimento" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-2">
                            <div class="form-group">
                                <label>Qual Origem ?</label>
                                <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Mensalidades" />
                                    <asp:ListItem Value="2" Text="Pag. Funcionario" />
                                    <asp:ListItem Value="3" Text="Outros pagamentos" />
                                    <asp:ListItem Value="4" Text="Outros recebimentos" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-2">
                            <div class="form-group">
                                <label>Status ?</label>
                                <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Pago" />
                                    <asp:ListItem Value="2" Text="Em aberto" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mt-3">
            <div class="col-12">
                <asp:GridView runat="server" ID="movimentoGridView" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                    EmptyDataText="Ainda sem movimento financeiro" DataKeyNames="movimentoId" CssClass="table table-hover table-borderless border-0">
                    <Columns>
                        <asp:BoundField HeaderText="Tipo" DataField="movimentoTipo"/>
                        <asp:BoundField HeaderText="Origem" DataField="movimentoOrigem" />
                        <asp:BoundField HeaderText="Valor" DataField="movimentoValor" />
                        <asp:BoundField HeaderText="Multa" DataField="movimentoMulta" />
                        <asp:BoundField HeaderText="Pago" DataField="movimentoPago" />
                        <asp:BoundField HeaderText="Conta" DataField="contaNome" />
                        <asp:BoundField HeaderText="Favorecido" DataField="pessoaNome" />
                        <asp:BoundField HeaderText="Emissão" DataField="movimentoDataEmissao" />
                        <asp:BoundField HeaderText="Pagamento" DataField="movimentoDataPagamento" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
