
ALTER DATABASE [Fabrica] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO

DROP DATABASE [Fabrica]
GO

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


create table Product(
	Id int identity primary key,
	Description nvarchar (50)not null,
	Value money not null
)

create table [Order](
	Id int identity primary key,
	UserId int not null,
	ProductId int not null,
	Unitvalue money not null,
	Amount float not null,
	Subtotal  money not null,
	Iva float not null,
	Total  money not null
)

ALTER TABLE [Order] ADD FOREIGN KEY (UserId) REFERENCES [user](id);
ALTER TABLE [Order] ADD FOREIGN KEY (ProductId) REFERENCES Product(id);


go
insert into [user] values ('Admin','$2a$11$i5TKETTflHmXTThKCV8MOeCaJAJ0EhkCvo47XxmssxqYWqY5mWZGS','Roberto','contrerasroberto621@gmail.com')

select * from [User]
select * from Product
select * from [Order]