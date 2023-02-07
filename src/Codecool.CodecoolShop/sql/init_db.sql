CREATE TABLE Product (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	name VARCHAR(50),
	description NVARCHAR(MAX),
	players VARCHAR(10),
	currency VARCHAR(3),
	default_price DECIMAL,
	product_category_id INT,
	supplier_id INT,
	image VARCHAR(50)
	);

CREATE TABLE ProductCategory (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	name VARCHAR(50),
	description NVARCHAR(MAX)
	);

CREATE TABLE Supplier (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	name VARCHAR(50),
	description NVARCHAR(MAX)
	);

CREATE TABLE Item (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	cart_id INT,
	product_id INT,
	quantity INT,
	item_total DECIMAL
	);

CREATE TABLE Cart (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	user_id INT
	);

CREATE TABLE Users (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	name varchar(20),
	email varchar(30),
	password varchar(50)
);

CREATE TABLE PersonalDetail (
	id INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
	user_id int,
	first_name varchar(15),
	last_name varchar(15),
	country varchar(30),
	zipcode int,
	adress varchar(50),
);

ALTER TABLE Product ADD FOREIGN KEY (product_category_id) REFERENCES ProductCategory(id);
ALTER TABLE Product ADD FOREIGN KEY (supplier_id) REFERENCES Supplier(id);
ALTER TABLE Item ADD FOREIGN KEY (product_id) REFERENCES Product(id);
ALTER TABLE Item ADD FOREIGN KEY (cart_id) REFERENCES Cart(id);
ALTER TABLE Cart ADD FOREIGN KEY (user_id) REFERENCES Users(id);
ALTER TABLE PersonalDetail ADD FOREIGN KEY (user_id) REFERENCES Users(id);