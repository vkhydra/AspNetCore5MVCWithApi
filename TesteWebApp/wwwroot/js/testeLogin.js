async function logar() {

    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var user = document.getElementById("usuario").value;
    var password = document.getElementById("password").value;

    var raw = JSON.stringify({
        "username": user,
        "password": password
    });

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };
    fetch("https://localhost:44389/api/account", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    fetch("https://localhost:44389/api/account", requestOptions)
        .then(function (response) {
            if (response.ok) {
                
            }
            else {
                alert("Usuario e, ou senha incorretos.");
            }
        }).catch(function (error) {
            alert("Houve um problema com a autenticação, tente novamente mais tarde. \r\nErro:" + error.message);
        })



};