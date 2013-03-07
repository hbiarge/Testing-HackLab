// --------------------------------------------------------------public ------------------------------------------------------
// <copyright file="DatabaseHelper.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DatabaseHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.IntegrationTests
{
    using System;

    using global::PetaPoco;

    public class DatabaseHelper
    {
        public const string Database = "Presencia";
        public const string Usuario = "Prueba";
        public const int NumeroDiasCreados = 3;

        public static void RestartTestDatabase()
        {
            var database = new Database("Presencia");

            using (var transaction = database.GetTransaction())
            {
                database.Execute("DELETE FROM Pausas;");
                database.Execute("DELETE FROM Jornadas;");

                // Añadir jornadas terminadas anteriores al día actual
                var dia = DateTime.Today.AddDays(-NumeroDiasCreados);

                for (var i = 0; i < NumeroDiasCreados; i++)
                {
                    var idJornada = database.ExecuteScalar<int>(
                        "INSERT INTO Jornadas ([Usuario],[Fecha],[Entrada],[Salida]) VALUES (@0, @1, @2, @3);\nSELECT SCOPE_IDENTITY() AS NewID;",
                        Usuario,
                        dia,
                        dia.AddHours(8),
                        dia.AddHours(16));

                    for (var j = 0; j < 4; j++)
                    {
                        var horaInicioPausa = 9 + j;
                        database.Execute(
                        "INSERT INTO Pausas ([IdJornada],[Inicio],[Fin]) VALUES (@0, @1, @2)",
                        idJornada,
                        dia.AddHours(horaInicioPausa),
                        dia.AddMinutes((horaInicioPausa * 60) + 30));
                    }

                    dia = dia.AddDays(1);
                }

                transaction.Complete();
            }
        }
    }
}
