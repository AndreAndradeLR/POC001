USE [dbBH]
GO

/****** Object:  Table [dbo].[t29_temp_vlFinanceiro]    Script Date: 04/14/2011 19:59:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t29_temp_vlFinanceiro](
	[t29_temp_vlFinanceiro] [int] IDENTITY(1,1) NOT NULL,
	[CodRelatorio] [int] NOT NULL,
	[t03_cd_projeto] [int] NULL,
	[nm_fonte] [varchar](50) NULL,
	[nm_unidade] [varchar](50) NULL,
	[PlanejadoMes] [decimal](18, 2) NULL,
	[RevisadoMes] [decimal](18, 2) NULL,
	[RealizadoMes] [decimal](18, 2) NULL,
	[EmpenhadoMes] [decimal](18, 2) NULL,
	[LiquidadoMes] [decimal](18, 2) NULL,
	[PlanejadoAcu] [decimal](18, 2) NULL,
	[RevisadoAcu] [decimal](18, 2) NULL,
	[RealizadoAcu] [decimal](18, 2) NULL,
	[EmpenhadoAcu] [decimal](18, 2) NULL,
	[LiquidadoAcu] [decimal](18, 2) NULL,
	[PlanejadoTot] [decimal](18, 2) NULL,
	[RevisadoTot] [decimal](18, 2) NULL,
	[RealizadoTot] [decimal](18, 2) NULL,
	[EmpenhadoTot] [decimal](18, 2) NULL,
	[LiquidadoTot] [decimal](18, 2) NULL,
	[dt_cadastro] [datetime] NULL,
	[dt_alterado] [datetime] NULL,
 CONSTRAINT [PK_t29_temp_vlFinanceiro] PRIMARY KEY CLUSTERED 
(
	[t29_temp_vlFinanceiro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


