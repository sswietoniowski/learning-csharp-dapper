create procedure dbo.Categories_GetAll
as
begin
	select 
		Id,
		Name,
		Description
	from
		dbo.Categories
end;
go