// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaDbRowPausaDbRowRelator.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaDbRowPausaDbRowRelator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco
{
    using System;
    using System.Collections.Generic;

    using Acheve.Data.Services.PetaPoco.Models;

    internal class JornadaDbRowPausaDbRowRelator
    {
        private JornadaDbRow current;

        public JornadaDbRow MapIt(JornadaDbRow jornada, PausaDbRow pausa)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (jornada == null)
            {
                return this.current;
            }

            // Is this the same as the current one we're processing
            if (this.current != null && this.current.IdJornada == jornada.IdJornada)
            {
                // Yes, just add this to the collection
                this.current.Pausas.Add(pausa);

                // Return null to indicate we're not done with this yet
                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet

            // Save the current author
            var prev = this.current;

            // Setup the new current author
            this.current = jornada;
            this.current.Pausas = new List<PausaDbRow>();

            if (pausa.Inicio != DateTime.MinValue)
            {
                this.current.Pausas.Add(pausa);
            }

            // Return the now populated previous author (or null if first time through)
            return prev;
        }
    }
}