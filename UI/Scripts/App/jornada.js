/// <reference path="../jquery-1.9.1.js" />
/// <reference path="../knockout-2.2.1.js" />
/// <reference path="../jquery-ui-timepicker-addon.js" />

var my = my || {};

my.attachDatetimepicker = function () {
    $('.datetime:not(.hasDatepicker)').datetimepicker({
        timeText: 'Hora',
        hourText: 'Horas',
        minuteText: 'Minutos',
        currentText: 'Ahora',
        closeText: 'Hecho',
    });
};

my.Pausa = function (original) {
    var self = this;
    self.id = original.id;
    self.inicio = ko.observable(original.inicio);
    self.fin = ko.observable(original.fin);
};

my.JornadaModel = function (original) {
    var self = this;
    self.usuario = original.usuario;
    self.id = original.id;
    self.dia = original.dia;
    self.inicio = ko.observable(original.inicio ? original.inicio : '');
    self.fin = ko.observable(original.fin ? original.fin : '');
    self.pausas = ko.observableArray(ko.utils.arrayMap(original.pausas, function (pausa) {
        return new my.Pausa(pausa);
    }));
    self.errores = ko.observable('');
    self.tieneErrores = ko.observable(false);
    self.addPausa = function () {
        self.pausas.push(new my.Pausa({
            id: -1,
            inicio: self.dia,
            fin: self.dia
        }));
        my.attachDatetimepicker();
    };
    self.removePausa = function (pausa) {
        self.pausas.remove(pausa);
    };
    self.saveJornada = function () {
        var json = JSON.stringify(ko.toJS(this));
        my.data.save(json, function (data) {
            if (!data.hasError) {
                window.location = data.location;
            } else {
                self.tieneErrores(true);
                self.errores(data.errors);
            }
        });
    };
};

$(document).ready(function () {

    ko.applyBindings(new my.JornadaModel(my.data.viewModel));

    $('.datetime').datetimepicker({
        timeText: 'Hora',
        hourText: 'Horas',
        minuteText: 'Minutos',
        currentText: 'Ahora',
        closeText: 'Hecho',
    });

});