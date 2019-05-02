create table [dbo].[Country](
Id int primary key identity,
Name nvarchar(50) not null,
Capital nvarchar(50) not null,
Population int not null,
)

create table [dbo].[City](
Id int primary key identity,
Name nvarchar(50) not null,
CountryId int not null,
Population int not null,
)

create table [dbo].[Street](
Id int primary key identity,
Name nvarchar(50) not null,
CityId int not null,
)