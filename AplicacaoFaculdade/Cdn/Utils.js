﻿const xhttp = new XMLHttpRequest();
window.onload = () => {
    $('#datetimepicker4').datetimepicker({
        format: 'L'
    });
    
    document.getElementById('adicionarUsuarioBtn').addEventListener('click', () => {
        $('#adicionarUsuarioModal').modal('show');
    });
}

function confirmDelete(sender) {
    if (sender.getAttribute('confirmed') == 'true') { return true; }
    Swal.fire({
        type: 'warning',
        title: 'Excluir Registro',
        text: 'Deseja realmente excluir esse registro ?',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Fechar'
    }).then((result) => {
        if (result.value) {
            sender.setAttribute('confirmed', true);
            sender.click();
        }
    });
    return false;
}

function confirmAction() {
    event.preventDefault();
    Swal.fire({
        type: 'warning',
        title: 'Sair',
        text: 'Deseja sair do sistema ?',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Fechar'
    }).then(result => {
        if (result.dismiss)
            return false;
        if (result.value)
            __doPostBack('<%= logoutButton.UniqueID %>', '');
    });

}
async function getCEP(text) {
    var text = document.getElementById('pessoaCep').value;
    if (text.length == 8) {
        xhttp.open('GET', 'https://viacep.com.br/ws/' + text + '/json/');
        xhttp.setRequestHeader('Content-Type', 'application/json');
        xhttp.onload = () => {
            if (xhttp.status == 200) {
                var rawResponse = xhttp.responseText;
                var response = JSON.parse(rawResponse);

                var enderecoElem = document.getElementById('pessoaEndereco');
                var numeroRuaElem = document.getElementById('pessoaNumeroRua');
                var complementoElem = document.getElementById('pessoaComplemento');
                var cidadeElemen = document.getElementById('pessoaCidade');
                var estadoElemen = document.getElementById('pessoaEstado');

                enderecoElem.value = response.logradouro;
                numeroRuaElem.value = "";
                complementoElem.value = response.complemento;
                cidadeElemen.value = response.localidade;
                estadoElemen.value = response.uf;

                enderecoElem.disabled = false;
                numeroRuaElem.disabled = false;
                complementoElem.disabled = false;
                cidadeElemen.disabled = false;
                estadoElemen.disabled = false;

            }
        }
        xhttp.send();
    }
}