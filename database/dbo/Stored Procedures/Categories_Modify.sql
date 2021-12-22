create procedure dbo.Categories_Modify
	@Id int,
	@Name varchar(64),
	@Description varchar(max)
as
begin
	update dbo.Categories
	set	
		Name = @Name
		, @Description = @Description
	where
		Id = @Id;
end;
go