CREATE TABLE [dbo].[Jornadas] (
    [IdJornada] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Usuario]   NVARCHAR (15) NOT NULL,
    [Fecha]     DATE          NOT NULL,
    [Entrada]   DATETIME      NOT NULL,
    [Salida]    DATETIME      NULL,
    CONSTRAINT [PK_Jornada_1] PRIMARY KEY CLUSTERED ([IdJornada] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Jornadas]
    ON [dbo].[Jornadas]([Usuario] ASC, [Fecha] ASC);

