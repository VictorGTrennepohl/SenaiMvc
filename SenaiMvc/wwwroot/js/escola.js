var escola = function () {

    return {
        carregarCidadesPorUF: function (uf) {
            if (!uf) return;
            var select = document.getElementById("Endereco_Cidade");
            select.innerHTML = '<option value="">Carregando...</option>';

            fetch('/Escola/ObterCidadesPorUF?uf=${uf}')
                .then(response => response.json())
                .then(data => {
                    select.innerHTML = '<option value="">-- Selecionea cidade --</option>';

                    data.forEach(function (cidade) {
                        var option = document.CreateElement("option");
                        option.value = cidade.id;
                        option.text = cidade.nome;
                        select.appenChild(option);
                    });
                })
                .catch(err => {
                    console.error("Erro ao carregar cidades:", err);
                });
        }
    }
}();