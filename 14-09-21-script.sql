USE [DemoTest]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 14/09/2021 02:23:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[PID] [int] NULL,
	[Age] [int] NULL,
	[Salary] [int] NULL,
	[Gender] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 14/09/2021 02:23:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [Name], [PID], [Age], [Salary], [Gender], [IsActive]) VALUES (1, N'Test', 1, 22, 15000, N'Female', 1)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [PID], [Age], [Salary], [Gender], [IsActive]) VALUES (2, N'Robert ', 2, 30, 35000, N'Male', 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [PID], [Age], [Salary], [Gender], [IsActive]) VALUES (3, N'Muthu', 3, 25, 25000, N'Male', 1)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([PositionID], [Name]) VALUES (1, N'Web Developer')
INSERT [dbo].[Position] ([PositionID], [Name]) VALUES (2, N'Android Developer')
INSERT [dbo].[Position] ([PositionID], [Name]) VALUES (3, N'IOS Developer')
INSERT [dbo].[Position] ([PositionID], [Name]) VALUES (4, N'HR')
INSERT [dbo].[Position] ([PositionID], [Name]) VALUES (5, N'Tester')
SET IDENTITY_INSERT [dbo].[Position] OFF
GO
/****** Object:  StoredProcedure [dbo].[UspVueJsCRUD]    Script Date: 14/09/2021 02:23:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UspVueJsCRUD]
@EmployeeID int=null,
@Name varchar(100)=null,
@PID int=null,
@Age varchar(20)=null,
@salary varchar(100)=null,
@Task varchar(100)=null,
@Gender varchar(50)=null,
@IsActive bit=null
as
begin

if(@Task='Select')
begin
      select EmployeeID,Name,(select [Name] from Position where PositionID=PID) Position,Age, Salary,Gender,IsActive from Employee
end
else if(@Task='Insert')
begin
      if(@EmployeeID is null)
      begin
              insert into Employee(Name,PID,Age,salary,Gender,IsActive) values(@Name,@PID,@Age,@salary,@Gender,@IsActive)
      end
      else
      begin
      update Employee set Name=@Name,Age=@Age,PID=@PID,salary=@salary,Gender=Gender,IsActive=@IsActive  where EmployeeID=@EmployeeID
      end
end
else if(@task='Delete')
begin
      delete from Employee where EmployeeID=@EmployeeID
end
end
GO
