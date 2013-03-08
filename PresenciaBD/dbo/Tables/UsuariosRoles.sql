CREATE TABLE [dbo].[UsuariosRoles] (
    [Usuario] NVARCHAR (10) NOT NULL,
    [Rol]     NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Usuario] ASC),
    CONSTRAINT [FK_UsuariosRoles_Roles] FOREIGN KEY ([Rol]) REFERENCES [dbo].[Roles] ([Name]),
    CONSTRAINT [FK_UsuariosRoles_Usuarios] FOREIGN KEY ([Usuario]) REFERENCES [dbo].[Usuarios] ([Usuario])
);

