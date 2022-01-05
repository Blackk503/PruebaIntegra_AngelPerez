use master
go
create database TestIntegra
go
use TestIntegra
go

create table empleado
(
	id int primary key identity(1,1),
	apellido varchar(75) not null,
	nombre varchar(75) not null,
	telefono varchar(15) not null unique,
	email varchar(100) not null unique,
	foto nvarchar(150) not null,
	fecha_contratacion date not null
)
