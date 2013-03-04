// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJornadaCommands.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IJornadaCommands type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acheve.Data.Services.Contracts
{
    using Acheve.Domain.Entities;

    public interface IJornadaCommands
    {
        void ActualizarJornada(Jornada jornada, string usuario);

        void CrearJornada(Jornada jornada, string usuario);

        void EliminarJornada(int idJornada);
    }
}