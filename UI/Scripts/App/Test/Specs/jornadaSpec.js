/// <reference path="../Jasmine/jasmine.js" />
/// <reference path="../../jornada.js" />
/// <reference path="../../../knockout-2.2.1.js" />
/// <reference path="../../../jquery-ui-timepicker-addon.js" />

describe("Una Pausa", function () {

    it("se incializa correctamente desde el original serializado", function () {

        var vm = { id: 1, inicio: '01/01/2013 08:25', fin: '01/01/2013 08:50' };
        var pausa = new my.Pausa(vm);

        expect(vm.id).toBe(pausa.id);
        expect(vm.inicio).toBe(pausa.inicio());
        expect(vm.fin).toBe(pausa.fin());
    });

});

describe("Una JornadaModel", function () {

    describe("se inicializa", function () {

        it("correctamente sin pausas", function () {

            var vm = {
                usuario: 'Prueba',
                id: 1,
                dia: '01/01/2013',
                inicio: '01/01/2013 08:25',
                fin: '01/01/2013 16:32',
                pausas: []
            };
            var jornadaModel = new my.JornadaModel(vm);

            expect(vm.usuario).toBe(jornadaModel.usuario);
            expect(vm.id).toBe(jornadaModel.id);
            expect(vm.dia).toBe(jornadaModel.dia);
            expect(vm.inicio).toBe(jornadaModel.inicio());
            expect(vm.fin).toBe(jornadaModel.fin());
            expect(jornadaModel.pausas().length).toBe(0);
        });

        it("correctamente con pausas", function () {

            var vm = {
                usuario: 'Prueba',
                id: 1,
                dia: '01/01/2013',
                inicio: '01/01/2013 08:00',
                fin: '01/01/2013 16:00',
                pausas: [
                    { id: 1, inicio: '01/01/2013 08:25', fin: '01/01/2013 08:50' },
                    { id: 2, inicio: '01/01/2013 09:30', fin: '01/01/2013 10:50' }
                ]
            };
            var jornadaModel = new my.JornadaModel(vm);

            expect(vm.usuario).toBe(jornadaModel.usuario);
            expect(vm.id).toBe(jornadaModel.id);
            expect(vm.dia).toBe(jornadaModel.dia);
            expect(vm.inicio).toBe(jornadaModel.inicio());
            expect(vm.fin).toBe(jornadaModel.fin());
            expect(jornadaModel.pausas().length).toBe(2);
        });

    });

    describe("sin pausas", function () {

        var jornadaModel;

        beforeEach(function () {

            var vm = {
                usuario: 'Prueba',
                id: 1,
                dia: '01/01/2013',
                inicio: '01/01/2013 08:00',
                fin: '01/01/2013 16:00',
                pausas: []
            };

            jornadaModel = new my.JornadaModel(vm);

        });

        it("puede anadir pausa", function () {

            spyOn(my, 'attachDatetimepicker');

            jornadaModel.addPausa();

            expect(jornadaModel.pausas().length).toBe(1);
        });

        it("puede eliminar una pausa añadida", function () {

            spyOn(my, 'attachDatetimepicker');

            jornadaModel.addPausa();

            expect(jornadaModel.pausas().length).toBe(1);

            var pausa = jornadaModel.pausas()[0];
            jornadaModel.removePausa(pausa);

            expect(jornadaModel.pausas().length).toBe(0);

        });

    });
});