function ocultarBoton() {
    document.getElementById('btnIniciarSesion').style.display = 'none';
    setTimeout(function() {
        document.getElementById('btnIniciarSesion').style.display = 'block';
        document.getElementById('mensajeError').innerText = ''; // Oculta el mensaje de error
    }, 30000); // 30 segundos
}

function ocultarMensajeError() {
    document.getElementById('mensajeError').innerText = '';
}