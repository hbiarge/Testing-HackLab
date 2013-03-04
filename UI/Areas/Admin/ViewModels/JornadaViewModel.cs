// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Acheve.Domain.Entities;

    public class JornadaViewModel
    {
        public JornadaViewModel()
        {
            this.Pausas = new List<PausaViewModel>();
        }

        public string Usuario { get; set; }

        public int Id { get; set; }

        public string Dia { get; set; }

        public string Inicio { get; set; }

        public string Fin { get; set; }

        public IEnumerable<PausaViewModel> Pausas { get; set; }

        public static JornadaViewModel FormJornada(Jornada jornada, string usuario)
        {
            var vm = new JornadaViewModel
            {
                Usuario = usuario,
                Id = jornada.Id,
                Dia = jornada.Dia.ToShortDateString(),
                Inicio = jornada.Inicio.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture),
                Fin = jornada.Fin.HasValue ? jornada.Fin.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture) : string.Empty,
                Pausas = jornada.Pausas.Select(p => new PausaViewModel
                {
                    Id = p.Id,
                    Inicio = p.Inicio.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture),
                    Fin = p.Fin.HasValue ? p.Fin.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture) : string.Empty,
                })
            };

            return vm;
        }
    }
}