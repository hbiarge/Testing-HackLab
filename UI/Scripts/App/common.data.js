/// <reference path="../jquery-1.9.1.js" />

var my = my || {};
my.data = my.data || {};

my.data.ajaxPostJson = function (url, data, success, error) {
    $.ajax({
        url: url,
        cache: false,
        type: 'POST',
        data: data,
        contentType: 'application/json; charset=utf-8',
        success: success,
        error: function (jqXHR, textStatus, errorThrown) {
            if (error) {
                error(jqXHR, textStatus, errorThrown);
            } else {
                var message = 'Se ha producido un error en la llamada al servidor.\r\n\r\n' + jqXHR.status + ': ' + jqXHR.statusText +
                    '\r\nUrl: ' + url;
                alert(message);
            }
        },
    });
};

my.data.ajaxGetJson = function (url, data, success, error) {
    $.ajax({
        url: url,
        cache: false,
        data: data,
        contentType: 'application/json; charset=utf-8',
        success: success,
        error: function (jqXHR, textStatus, errorThrown) {
            if (error) {
                error(jqXHR, textStatus, errorThrown);
            } else {
                var message = 'Se ha producido un error en la llamada al servidor.\r\n\r\n' + jqXHR.status + ': ' + jqXHR.statusText +
                    '\r\nUrl: ' + url;
                alert(message);
            }
        },
    });
};