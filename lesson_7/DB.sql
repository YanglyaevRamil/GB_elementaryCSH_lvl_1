USE [EmployeeBook]
GO

/****** Object:  Table [dbo].[DepartmentTable]    Script Date: 28.10.2021 0:31:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepartmentTable](
	[IdDepartment] [nvarchar](3) NOT NULL,
	[NameDepartment] [nvarchar](20) NULL,
	[Profit] [nvarchar](25) NULL,
 CONSTRAINT [PK_DepartmentTable] PRIMARY KEY CLUSTERED 
(
	[IdDepartment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [EmployeeBook]
GO

/****** Object:  Table [dbo].[EmployeeTable]    Script Date: 28.10.2021 0:31:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeTable](
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[SecondName] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL,
	[Salary] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[IdDepartment] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_EmployeeTable] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeTable]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTable_DepartmentTable] FOREIGN KEY([IdDepartment])
REFERENCES [dbo].[DepartmentTable] ([IdDepartment])
GO

ALTER TABLE [dbo].[EmployeeTable] CHECK CONSTRAINT [FK_EmployeeTable_DepartmentTable]
GO

