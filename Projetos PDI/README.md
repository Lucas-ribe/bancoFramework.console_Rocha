Meu primeiro projeto

SCRIPT DB

Create database BancoFramework
Go

USE BancoFramework

Create table dbo.Cliente
(
	Id int not null,
	Nome varchar(30) not null,
	CPF varchar(10) not null,
	Saldo float not null,
	constraint Pk_Pessoa primary key clustered (Id ASC)
);
