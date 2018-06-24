CREATE TABLE [dbo].[Genre](
	[Id] [smallint] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)