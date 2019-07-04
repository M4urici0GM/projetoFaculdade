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
                                <asp:LinkButton runat="server" ID="filtrarBtn" OnClick="OnFiltrarClickEventHandler" CssClass="btn btn-block btn-outline-dark">
                                    <i class="fas fa-filter"></i> Filtrar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-12 col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>Data inicial<span class="text-danger">*</span>:</label>
                                <div class="input-group date" id="datetimepicker4" data-target-input="nearest">
                                    <asp:TextBox runat="server" CssClass="form-control datetimepicker-input" ID="filtroDataInicial" placeholder="Data inicial de pesquisa." data-target="#datetimepicker4"/>
                                    <div class="input-group-append" data-target="#datetimepicker4" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>Data final<span class="text-danger">*</span>:</label>
                                <div class="input-group date" id="datetimepicker5" data-target-input="nearest">
                                    <asp:TextBox runat="server" CssClass="form-control datetimepicker-input" ID="filtroDataFinal" placeholder="Data final de pesquisa." data-target="#datetimepicker5"/>
                                    <div class="input-group-append" data-target="#datetimepicker5" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
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
                                <asp:DropDownList runat="server" ID="movimentoOrigemDropDown" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Mensalidades" />
                                    <asp:ListItem Value="2" Text="Caixa" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-2">
                            <div class="form-group">
                                <label>Status ?</label>
                                <asp:DropDownList runat="server" ID="movimentoStatusDropDown" CssClass="form-control">
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
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="movimentoGridView" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                        EmptyDataText="Ainda sem movimento financeiro" DataKeyNames="movimentoId" CssClass="table table-hover table-borderless border-0 text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Tipo" DataField="movimentoTipo"/>
                            <asp:BoundField HeaderText="Origem" DataField="movimentoOrigem" />
                            <asp:BoundField HeaderText="Valor" DataField="movimentoValor" DataFormatString="R$ {0:F}"/>
                            <asp:BoundField HeaderText="Multa" DataField="movimentoMulta" DataFormatString="R$ {0:F}" />
                            <asp:BoundField HeaderText="Pago" DataField="movimentoPago" DataFormatString="R$ {0:F}"/>
                            <asp:BoundField HeaderText="Conta" DataField="contaNome" />
                            <asp:BoundField HeaderText="Favorecido" DataField="Favorecido" />
                            <asp:BoundField HeaderText="Emissão" DataField="movimentoDataEmissao" />
                            <asp:BoundField HeaderText="Pagamento" DataField="movimentoDataPagamento" />
                            <asp:BoundField HeaderText="Contrato" DataField="contratoNumero" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
