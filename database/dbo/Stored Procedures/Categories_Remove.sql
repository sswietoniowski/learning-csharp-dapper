create procedure dbo.Categories_Remove
	@Id int
as
begin
	delete from dbo.Categories
	where Id = @Id;
end;
go