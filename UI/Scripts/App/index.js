/// <reference path="../jquery-1.9.1.js" />

var my = my || {};

$(document).ready(function () {

    $('.eliminar').on('click', function (e) {

        var input = $(e.target);
        var usuario = input.data('usuario');
        var fecha = input.data('fecha');

        var shouldContinue = confirm('Va a eliminar permanentemente los datos de la jornada ' + fecha + ' para el usuario ' + usuario + '\r\n\r\n¿Desea continuar?');

        if (shouldContinue) {
            var json = JSON.stringify({ usuario: usuario, fecha: fecha });

            my.data.delete(json, function (data) {
                if (data.hasError) {
                    alert(data.errors);
                } else {
                    $('#btnBuscar').click();
                }
            });
        }

        e.preventDefault();
        e.stopPropagation();
    });

});