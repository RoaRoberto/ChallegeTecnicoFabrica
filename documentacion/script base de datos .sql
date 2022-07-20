use master
drop database fabrica
go
create database Fabrica
go
use Fabrica
go

create table [User](
Id int identity primary key,
Login nvarchar (255)not null,
Password nvarchar (255)not null,
Name nvarchar (255)not null,
Email nvarchar (255)not null
)


select * from [User]