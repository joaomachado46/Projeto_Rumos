//efeitos para mostrar e esconder div e section

function MudarestadoShow(el) {
    var display = document.getElementById(el).style.display;
    if (display == "none")
        document.getElementById(el).style.display = 'block';
    else
        document.getElementById(el).style.display = 'none';
}

function MudarestadoHide(el) {
    var display = document.getElementById(el).style.display;
    if (display == "block")
        document.getElementById(el).style.display = 'none';
    else
        document.getElementById(el).style.display = 'block';
}

function MudarestadoShow(el) {
    var display = document.getElementById(el).style.display;
    if (display == "none")
        document.getElementById(el).style.display = 'block';
    else
        document.getElementById(el).style.display = 'none';
}

function MudarestadoHide(el) {
    var display = document.getElementById(el).style.display;
    if (display == "block")
        document.getElementById(el).style.display = 'none';
    else
        document.getElementById(el).style.display = 'block';
}

function setTwoNumberDecimal(event) {
    this.value = parseFloat(this.value).toFixed(2);
}

//função para enviar o produto para o carrinho e retornar uma mensagem TRUE or FALSE e chamar o pop #caixa(_PopupPartialView.cshtml)
function encomendar(id) {

    var qta = document.getElementById(`${id}`);
    var res = parseInt(qta.value);

    var dados = [id, res]
    //Enviar para o servidor:
    fetch("/CarrinhoCompras/Create",
        {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",

            body: JSON.stringify(dados)  
        }
    )
        .then(resposta => resposta.json()) // Esta instrução dá erro se a resposta do server não for json
        .then(dados => apresentar(dados));
}

//função para receber a resposta json e definir depois disso a ação
function apresentar(dados) {
    if (dados.sucesso == true) {
        runEffect("blind");
    }
    else if (dados.sucesso == false) {
        alert("Produto já está no carrinho...");
    }
    else if (dados.notData == true) {
        alert("Necessário uma quantidade do produto");
    }
    else if (dados.necessarioLogin == true) {
        alert("Necessário efectuar login");
    }
}


function runEffect(obj) {
    // get effect type from
    var selectedEffect = obj;

    // Most effect types need no options passed by default
    var options = {};
    // some effects have required parameters
    if (selectedEffect === "scale") {
        options = { percent: 50 };
    } else if (selectedEffect === "size") {
        options = { to: { width: 280, height: 185 } };
    }

    //coloca um estilo a div #caixa(_PopupPartialView.cshtml)
    document.getElementById("caixa-popup").style.display = "block";
    // Run the effect
    $("#effect").show(selectedEffect, options, 500, callback);
};

//callback function to bring a hidden box back
function callback() {
    setTimeout(function () {
        //retira o estilo a div #caixa(_PopupPartialView.cshtml)
        $("#effect:visible").removeAttr("style").fadeOut();
        document.getElementById("caixa-popup").style.display = "none";
    }, 2000);
};

//serve para deixar a div escondida até ser chamada a função e aplicada o estilo para SHOW
$("#effect").hide();


