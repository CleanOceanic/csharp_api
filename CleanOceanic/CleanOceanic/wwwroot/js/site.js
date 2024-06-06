function togglePassword() {
    var pswdField = document.getElementById("pswd");
    var showPasswordText = document.querySelector(".password-show");
    if (pswdField.type === "password") {
        pswdField.type = "text";
        showPasswordText.textContent = "Ocultar Senha";
    } else {
        pswdField.type = "password";
        showPasswordText.textContent = "Mostrar Senha";
    }
}