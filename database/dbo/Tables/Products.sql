create table dbo.Products
(
	Id int not null identity(1,1) constraint PK_Products primary key,
	Name varchar(128) not null,
	Price money not null,
	Description varchar(max) null,
	Thumbnail varbinary(max) null,
	CategoryId int not null constraint FK_Products_Categories foreign key references Categories (Id) on update cascade on delete no action
);
