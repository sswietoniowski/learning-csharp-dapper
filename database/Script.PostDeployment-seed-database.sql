set identity_insert dbo.Categories on;

insert into dbo.Categories (Id, Name, Description)
values 
	(1, 'AGD', 'Różne takie z kategorii AGD'),
	(2, 'RTV', null),
	(3, 'Spożywcze', 'Wszelkiej maści produkty spożywcze');

set identity_insert dbo.Categories off;

set identity_insert dbo.Products on;

insert into dbo.Products (Id, Name, Price, Description, CategoryId)
values
	(1, 'Pralka', 1000.00, 'Ujdzie chociaż bez rewelacji', 1),
	(2, 'Lodówka', 999.00, null, 1),
	(3, 'Ekspres', 2000.00, 'Kawa o poranku to podstawa', 1),
	(4, 'Telewizor', 4000.00, 'Duuuuży telewizor to konieczność ;-)', 2),
	(5, 'Radio', 100, 'Ktoś jeszcze z tego korzysta?', 2),
	(6, 'Jabłko', 2.99, 'An apple a day keeps doctors away...', 3),
	(7, 'Chleb', 4.99, 'Czemu tak drogo?', 3);

set identity_insert dbo.Products off;