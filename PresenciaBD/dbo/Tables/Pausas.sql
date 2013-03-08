CREATE TABLE [dbo].[Pausas] (
    [IdPausa]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [IdJornada] BIGINT   NOT NULL,
    [Inicio]    DATETIME NOT NULL,
    [Fin]       DATETIME NULL,
    CONSTRAINT [PK_Pausas] PRIMARY KEY CLUSTERED ([IdPausa] ASC),
    CONSTRAINT [FK_Pausas_Jornada] FOREIGN KEY ([IdJornada]) REFERENCES [dbo].[Jornadas] ([IdJornada])
);

