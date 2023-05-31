let templateDetails = (item) => {
    return `<div class="row">
                <div class="col-1 fw-bold">Nome</div>
                <div class="col">${item.name}</div>
            </div>
            <div class="row">
                <div class="col-1 fw-bold">Categoria</div>
                <div class="col">${item.categoryName}</div>
            </div>
            <div class="row">
                <div class="col-1 fw-bold">Produttore</div>
                <div class="col">${item.producer}</div>
            </div>
            <div class="row">
                <div class="col-1 fw-bold">Grado</div>
                <div class="col">${item.grade}</div>
            </div>
            <div class="row">
                <div class="col-1 fw-bold">Punteggio</div>
                <div class="col">${item.points}</div>
            </div>
            <div class="row">
                <div class="col-1 fw-bold">Ingredienti</div>
                <div class="col">${item.ingredients}</div>
            </div>
            <div class="row">
                <div class="col-12">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>Produttore</th>
                                <th>Punteggio</th>
                            </tr>
                        </thead>
                        <tbody>
                            ${item.betterProducts.map(templateTRows).join('')}
                        </tbody>
                    </table>
                </div>
            </div>`;
}

let templateTRows = (item) => {
    return `<tr>
                <td>${item.name}</td>
                <td>${item.producer}</td>
                <td>${item.points}</td>
            </tr>`;
}

let templateToast = (msg) => {
    return `<div class="toast position absolute" role="alert" aria-live="assertive" aria-atomic="true">
      <div class="toast-header">
        <strong class="me-auto">Alert</strong>
        <small class="text-body-secondary">just now</small>
        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
      </div>
      <div class="toast-body">
        ${msg}
      </div>
    </div>`;
}

let ManageResult = (result) => {
    console.log(result);
    let html = templateDetails(result);    
    document.getElementById("ProductDetails").innerHTML = html;    
}

let Send = () => {
    let errDiv = document.getElementById("errDiv");
    errDiv.setAttribute("hidden", true);
    let barcode = document.getElementById("barcode").value;
    fetch("api/code/" + barcode)
        .then(result => result.json().then(json => ManageResult(json)))
        .catch(err => {
            errDiv.innerHTML = "Il codice inserito non esiste, si prega di riprovare";
            errDiv.removeAttribute("hidden");
        });
    let toast = templateToast("FINITO!!!");
    let toastContainer = document.getElementById("toast-area");
    toastContainer.innerHTML += toast;
    let toastItem = toastContainer.lastChild;
    let options =  { autohide: true, animation: true, delay: 1000 };
    new bootstrap.Toast(toastItem, options).show();
    toastItem.addEventListener("hidden.bs.toast", (event) => {
        toastItem.remove();
    });
}

(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const form = document.getElementById('submitForm')

    form.querySelector("input").addEventListener('blur', event => {
        form.classList.add('was-validated')
        form.checkValidity();
    }, false);


    form.addEventListener('submit', event => {
        event.preventDefault();
        event.stopPropagation();
        form.classList.add('was-validated')
        if (form.checkValidity()) {
            Send();
        }
    }, false);
})()