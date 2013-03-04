// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaEditViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaEditViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Acheve.Domain.Entities;

    public class JornadaEditViewModel : IValidatableObject
    {
        public JornadaEditViewModel()
        {
            this.Pausas = new List<PausaEditViewModel>();
        }

        [Required]
        public string Usuario { get; set; }

        public int Id { get; set; }

        public DateTime Dia { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime? Fin { get; set; }

        public List<PausaEditViewModel> Pausas { get; set; }

        public Jornada ToJornada()
        {
            var pausas = from pvm in this.Pausas
                         select pvm.Fin.HasValue
                                    ? new Pausa(pvm.Inicio, pvm.Fin.Value) { Id = pvm.Id }
                                    : new Pausa(pvm.Inicio) { Id = pvm.Id };

            var jornada = this.Fin.HasValue ?
                              new Jornada(this.Inicio, this.Fin.Value, pausas.ToArray()) { Id = this.Id } :
                              new Jornada(this.Inicio, pausas.ToArray()) { Id = this.Id };

            return jornada;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Inicio.Date != this.Dia)
            {
                yield return new ValidationResult("La fecha de inicio debe coincidir con el día de la jornada: " + this.Dia.ToShortDateString());
            }

            if (this.Fin.HasValue && this.Fin.Value.Subtract(this.Inicio) > TimeSpan.FromHours(24))
            {
                yield return new ValidationResult("La jornada no puede tener una duración de más de 24 horas");
            }

            for (var i = 0; i < this.Pausas.Count; i++)
            {
                var pausa = this.Pausas[i];

                if (pausa.Inicio < this.Inicio)
                {
                    yield return new ValidationResult("Las pausas deben empezar después de la fecha de inicio de la jornada", new[] { "Pausa nº " + (i + 1) });
                }

                if (this.Fin.HasValue && pausa.Fin.HasValue && pausa.Fin.Value > this.Fin.Value)
                {
                    yield return new ValidationResult("Las pausas no pueden finalizar después de la fecha de fin de la jornada", new[] { "Pausa nº " + (i + 1) });
                }
            }
        }
    }
}