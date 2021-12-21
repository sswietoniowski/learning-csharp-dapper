create procedure dbo.Categories_Add
	@Name varchar(64),
	@Description varchar(max)
as
begin
	insert into dbo.Categories (Name, Description)
	values (@Name, @Description);

	select SCOPE_IDENTITY();
end;
go