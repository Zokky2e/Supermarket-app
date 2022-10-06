create table Employees(
	EmployeeID int NOT NULL,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
    Address varchar(255) NULL,
    OIB char(10) NOT NULL,
	Primary key (EmployeeID),
	Unique(OIB)) ;

	create index oibIndex on Employees(OIB)
	
	create table Products(
	ProductID int NOT NULL,
    Name varchar(255) NOT NULL,
	Price decimal(6,2) NOT NULL,
    Mark varchar(255) NULL,
	Primary key(ProductID));

	create table EmployeeProduct(
		SupermarketID int NOT NULL,
		EmployeeID int  NOT NULL,
		ProductID int  NOT NULL,
		Primary Key(SupermarketID), 
		Foreign key(EmployeeID) references Employees(EmployeeID),
		Foreign key(ProductID) references Products(ProductID));


	insert into Employees values(01,'Pero','Peric','Novska 32','1234567890');
	insert into Employees values(02,'Mario','Maric','Novska 32','7392829202');

	insert into Products values(01,'Apple',3.99, '');
	insert into Products values(02,'Pear',2.99,'Belgrade');
	insert into Products values(03,'Bread',6.99,'');
	insert into Products values(04,'Juice',2.99,'kplus');

	insert into EmployeeProduct values(01, 01, 01)
	insert into EmployeeProduct values(02, 01, 03)
	insert into EmployeeProduct values(03, 02, 04)
	insert into EmployeeProduct values(04, 02, 02)
	insert into EmployeeProduct values(05, 02, 01)

	select * from Employees where FirstName Like 'P%';
	select * from Products where Price > 3.00;
	
	update Products 
	set Mark = 'not marked'
	where Mark = '';

	delete Products where ProductID = 6;



	select * from "EmployeeProduct"

		select ep.SupermarketID as SupermarketID, e.FirstName + ' ' + e.Lastname as EmployeeName, p.Name as ProductName
		from EmployeeProduct as ep, Employees as e, Products as p 
		where ep.EmployeeID = e.EmployeeID and p.ProductID = ep.ProductID and p.Price > 3.00
		order by p.Name

	select * from Employees;
