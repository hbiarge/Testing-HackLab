// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJornadaService.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IJornadaService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Services.Contracts
{
    public interface IJornadaService
    {
        void IniciarJornada(string usuario);

        void TerminarJornada(string usuario);

        void IniciarPausa(string usuario);

        void TerminarPausa(string usuario);
    }
}
