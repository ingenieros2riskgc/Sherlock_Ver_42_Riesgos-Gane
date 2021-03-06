USE [Test_Sherlock_LinQ]
GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizacionIndicadorSegmento]    Script Date: 20/10/2016 14:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Actualizacion Sarlaft Indicador Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizacionIndicadorSegmento]
	-- Parametros de entrada
	@IdIndicador numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Indicador nvarchar(1000),
	@MensajeSenalAlerta nvarchar(1000),
	@NombreAtributo1 nvarchar(200),
	@NombreRango1 nvarchar(200),
	@NombreAtributo2 nvarchar(200),
	@NombreRango2 nvarchar(200),
	@ValorInferior1 numeric(18,2),
	@ValorSuperior1 numeric(18,2),
	@ValorInferior2 numeric(18,2),
	@ValorSuperior2 numeric(18,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.Indicador SET Codigo = @Codigo, Nombre = @Nombre, 
	Indicador = @Indicador, MensajeSenalAlerta = @MensajeSenalAlerta, 
	NombreAtributo1 = @NombreAtributo1, NombreRango1 = @NombreRango1, 
	NombreAtributo2 = @NombreAtributo2, NombreRango2 = @NombreRango2, 
	ValorInferior1 = @ValorInferior1, ValorSuperior1 = @ValorSuperior1, 
	ValorInferior2 = @ValorInferior2, ValorSuperior2 = @ValorSuperior2
	WHERE (IdIndicador = @IdIndicador)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizarAtributoSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Atributo Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizarAtributoSegmento]
	-- Parametros de entrada
	@IdAtributo numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.AtributoSegmentacion SET Codigo = @Codigo, Nombre = @Nombre,
	 Descripcion = @Descripcion 
	WHERE (IdAtributo = @IdAtributo)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizarFactoRiesgo]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Factor de Riesgo
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizarFactoRiesgo]
	-- Parametros de entrada
	@IdFactorRiesgo numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Indicador nvarchar(80),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.FactorRiesgo SET Codigo = @Codigo, Nombre = @Nombre, 
	Indicador = @Indicador, Descripcion = @Descripcion
	WHERE (IdFactorRiesgo = @IdFactorRiesgo)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizarPerfilSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Perfil Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizarPerfilSegmento]
	-- Parametros de entrada
	@IdPerfil numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.PerfilSegmento SET Codigo = @Codigo, Nombre = @Nombre,
	 Descripcion = @Descripcion
	WHERE (IdPerfil = @IdPerfil)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizarSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizarSegmento]
	-- Parametros de entrada
	@IdSegmento numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.Segmento SET Codigo = @Codigo, Nombre = @Nombre, 
	Descripcion = @Descripcion 
	WHERE (IdSegmento = @IdSegmento)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTActualizarTipoSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Tipo Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTActualizarTipoSegmento]
	-- Parametros de entrada
	@IdTipoSegmento numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Parametrizacion.TipoSegmento SET Codigo = @Codigo, Nombre = @Nombre, 
	Descripcion = @Descripcion 
	WHERE (IdTipoSegmento = @IdTipoSegmento)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionAtributoSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Atributo Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionAtributoSegmento]
	-- Parametros de entrada
	@IdTipoSegmento numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.AtributoSegmentacion (IdTipoSegmento, Codigo, Nombre, Descripcion) 
	VALUES 
	(@IdTipoSegmento, @Codigo, @Nombre, @Descripcion)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionFactoRiesgo]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizar Sarlaft Factor de Riesgo
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionFactoRiesgo]
	-- Parametros de entrada
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Indicador nvarchar(80),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.FactorRiesgo (Codigo, Nombre, Indicador, Descripcion) 
	VALUES (@Codigo, @Nombre, @Indicador, @Descripcion)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionIndicadorSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Indicador Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionIndicadorSegmento]
	-- Parametros de entrada
	@IdPerfil numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Indicador nvarchar(1000),
	@MensajeSenalAlerta nvarchar(1000),
	@NombreAtributo1 nvarchar(200),
	@NombreRango1 nvarchar(200),
	@NombreAtributo2 nvarchar(200),
	@NombreRango2 nvarchar(200),
	@ValorInferior1 numeric(18,2),
	@ValorSuperior1 numeric(18,2),
	@ValorInferior2 numeric(18,2),
	@ValorSuperior2 numeric(18,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.Indicador (IdPerfil, Codigo, Nombre, Indicador, MensajeSenalAlerta, NombreAtributo1,
	 NombreRango1, NombreAtributo2, NombreRango2, ValorInferior1, ValorSuperior1, ValorInferior2, ValorSuperior2) 
	VALUES 
	(@IdPerfil, @Codigo, @Nombre, @Indicador, @MensajeSenalAlerta,
	 @NombreAtributo1, @NombreRango1, @NombreAtributo2, @NombreRango2, 
	 @ValorInferior1, @ValorSuperior1, @ValorInferior2, @ValorSuperior2)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionPerfilSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Perfil Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionPerfilSegmento]
	-- Parametros de entrada
	@IdAtributo numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.PerfilSegmento (IdAtributo, Codigo, Nombre, Descripcion) 
	VALUES 
	(@IdAtributo, @Codigo, @Nombre, @Descripcion)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionSegmento]
	-- Parametros de entrada
	@IdFactorRiesgo numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.Segmento (IdFactorRiesgo, Codigo, Nombre, Descripcion) 
	VALUES 
	(@IdFactorRiesgo, @Codigo, @Nombre, @Descripcion)
END

GO
/****** Object:  StoredProcedure [Parametrizacion].[spSARLAFTInsercionTipoSegmento]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Tipo Segmento
-- =============================================
CREATE PROCEDURE [Parametrizacion].[spSARLAFTInsercionTipoSegmento]
	-- Parametros de entrada
	@IdSegmento numeric(18),
	@Codigo nvarchar(10),
	@Nombre nvarchar(40),
	@Descripcion nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Parametrizacion.TipoSegmento (IdSegmento, Codigo, Nombre, Descripcion) 
	VALUES 
	(@IdSegmento, @Codigo, @Nombre, @Descripcion)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTActualizarConfiguracionEstructuraArchivo]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizacion Sarlaft Configuracion Estructura Archivo
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTActualizarConfiguracionEstructuraArchivo] 
	-- Parametros de entrada
	@NombreCampo varchar(250),
	@Longitud numeric(18),
	@Parametriza bit,
	@IdTipoParametro numeric(18),
	@IdTipoDato numeric(18),
	@Posicion numeric(18),
	@IdCampo numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [Perfiles].[tblEstructuraCampos]
	SET [NombreCampo] = @NombreCampo, [Longitud] = @Longitud, [Parametriza] = @Parametriza, 
	[IdTipoParametro] = @IdTipoParametro, [IdTipoDato] = @IdTipoDato, [Posicion]= @Posicion
	WHERE [IdCampo] = @IdCampo

END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTActualizarParametrizacion]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizacion Sarlaft Parametrizacion
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTActualizarParametrizacion] 
	-- Parametros de entrada
	@IdParametros numeric(18),
	@IdTipoParametro numeric(18),
	@NombreParametro varchar(250),
	@CalificacionParametro varchar(250),
	@CodigoParametro varchar(250),
	@EsFormula bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [Perfiles].[tblParametrizacion] SET [IdTipoParametro] = @IdTipoParametro,
	[NombreParametro] = @NombreParametro, [CalificacionParametro] = @CalificacionParametro, 
	[CodigoParametro] = @CalificacionParametro, [EsFormula] = @EsFormula
	WHERE [IdParametros] = @IdParametros
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTActualizarPerfiles]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizacion Sarlaft Perfiles
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTActualizarPerfiles] 
	-- Parametros de entrada
	@Nombre varchar(250),
	@ValorMinimo numeric(18),
	@ValorMaximo numeric(18),
	@IdPerfil numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [Perfiles].[tblPerfil]
	SET [Nombre] = @Nombre, [ValorMinimo] = @ValorMinimo, [ValorMaximo] = @ValorMaximo 
	WHERE IdPerfil = @IdPerfil
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTActualizarTipoParametro]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Actualizacion Sarlaft Tipo parametro
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTActualizarTipoParametro] 
	-- Parametros de entrada
	@IdTipoParametro numeric(18),
	@NombreVariable varchar(250),
	@Calificacion numeric(18),
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [Perfiles].[tblTipoParametros] SET [NombreParametro] = @NombreVariable, 
	[Calificacion] = @Calificacion, [Activo] = @Activo 
	WHERE [IdTipoParametro] = @IdTipoParametro
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionConfiguracionEstructuraArchivo]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Configuracion Estructura Archivo
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionConfiguracionEstructuraArchivo]
	-- Parametros de entrada
	@NombreCampo varchar(250),
	@Longitud numeric(18),
	@Parametriza bit,
	@IdTipoParametro numeric(18),
	@IdTipoDato numeric(18),
	@Posicion numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [Perfiles].[tblEstructuraCampos] ([NombreCampo], [Longitud], [Parametriza], [IdTipoParametro],
	 [IdTipoDato], [Posicion])
	 VALUES 
	 (@NombreCampo, @Longitud, @Parametriza, @IdTipoParametro, @IdTipoDato, @Posicion)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionFactoSeñal]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Factor Señal
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionFactoSeñal]
	-- Parametros de entrada
	@IdFactorRiesgo numeric(18),
	@IdSenal numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT [Perfiles].[tblFactorSenal] ([IdFactorRiesgo] ,[IdSenal]) 
	VALUES 
	(@IdFactorRiesgo, @IdSenal)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionParametrizacion]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Parametrizacion
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionParametrizacion] 
	-- Parametros de entrada
	@IdTipoParametro numeric(18),
	@NombreParametro varchar(250),
	@CalificacionParametro varchar(250),
	@CodigoParametro varchar(250),
	@EsFormula bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [Perfiles].[tblParametrizacion]
	([IdTipoParametro], [NombreParametro], [CalificacionParametro], [CodigoParametro], [EsFormula]) 
	VALUES 
	(@IdTipoParametro, @NombreParametro, @CalificacionParametro, @CodigoParametro, @EsFormula)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionPerfil]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Perfiles
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionPerfil]
	-- Parametros de entrada
	@Nombre varchar(250),
	@ValorMinimo numeric(18),
	@ValorMaximo numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [Perfiles].[tblPerfil] ([Nombre], [ValorMinimo], [ValorMaximo]) 
	VALUES 
	(@Nombre, @ValorMinimo, @ValorMaximo)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionSeñalAlertaVariable]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Señal Alerta Variable
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionSeñalAlertaVariable]
	-- Parametros de entrada
	@IdSenal numeric(18),
	@IdOperando numeric(18),
	@Valor varchar(50),
	@Posicion numeric(18),
	@EsGlobal bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT [Perfiles].[tblSenalVariable] ([IdSenal],[IdOperando],[Valor],[Posicion],[EsGlobal])
	VALUES 
	(@IdSenal, @IdOperando, @Valor, @Posicion, @EsGlobal)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionSeñalCargueInfoArchivo]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Señal CargueInfo Archivo
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionSeñalCargueInfoArchivo]
	-- Parametros de entrada
	@IdArchivo numeric(18),
	@ValorCampoArchivo varchar(max),
	@Posicion numeric(18),
	@NumeroLinea numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [Perfiles].[tblInfoCargueArchivo]([IdArchivo], [ValorCampoArchivo], [Posicion], [NumeroLinea])
	VALUES 
	(@IdArchivo,@ValorCampoArchivo,@Posicion, @NumeroLinea)
END

GO
/****** Object:  StoredProcedure [Perfiles].[spSARLAFTInsercionTipoParametro]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 19-10-2016
-- Description:	Insercion Sarlaft Tipo parametro
-- =============================================
CREATE PROCEDURE [Perfiles].[spSARLAFTInsercionTipoParametro] 
	-- Parametros de entrada
	@NombreVariable varchar(250),
	@Calificacion numeric(18),
	@Activo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT [Perfiles].[tblTipoParametros] ([NombreParametro], [Calificacion], [Activo]) 
	VALUES 
	(@NombreVariable, @Calificacion, @Activo)
END

GO
/****** Object:  StoredProcedure [Proceso].[spSARLAFTActualizacionRegistroOperacionUsuario]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Actualizacion Sarlaft Registro Operacion Usuario
-- =============================================
CREATE PROCEDURE [Proceso].[spSARLAFTActualizacionRegistroOperacionUsuario]
	-- Parametros de entrada
	@IdRegistroOperacion numeric(18),
	@IdUsuarioAsignado numeric(18)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Proceso.RegistroOperacion SET IdEstadoOperacion = 2, IdUsuario = @IdUsuarioAsignado 
	WHERE (IdRegistroOperacion = @IdRegistroOperacion)
END

GO
/****** Object:  StoredProcedure [Proceso].[spSARLAFTInsercionRegistroOperacion]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Registro Operacion
-- =============================================
CREATE PROCEDURE [Proceso].[spSARLAFTInsercionRegistroOperacion]
	-- Parametros de entrada
	@IdRegistroOperacion numeric(18),
	@FechaPosibleSolucion nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Proceso.RegistroOperacion SET FechaPosibleSolucion = CONVERT(datetime, @FechaPosibleSolucion,120) 
	WHERE (IdRegistroOperacion = @IdRegistroOperacion)
END

GO
/****** Object:  StoredProcedure [Proceso].[spSARLAFTInsercionRegistroOperacionComentario]    Script Date: 20/10/2016 14:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JhonRestrepo
-- Create date: 20-10-2016
-- Description:	Insercion Sarlaft Registro Operacion Comentario
-- =============================================
CREATE PROCEDURE [Proceso].[spSARLAFTInsercionRegistroOperacionComentario]
	-- Parametros de entrada
	@IdRegistroOperacion numeric(18),
	@NombreUsuario nvarchar(400),
	@Comentario nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Proceso.ComentarioRegistroOperacion (IdRegistroOperacion, NombreUsuario, FechaRegistro, Comentario) 
	VALUES (@IdRegistroOperacion, @NombreUsuario, GETDATE(), @Comentario)
END

GO
