"# Parapharmacie"

Prepare sql

use Parapharmacydb;

create table role (
Id int primary key Identity(1,1),
Name varchar(100)
);

create table "User" (
Id int primary key Identity(1,1),
Name varchar(100),
Email varchar(255),
Password varchar(255),
RoleId int ,
foreign key (RoleId) references role(Id)
);
create table Category(
Id int primary key Identity(1,1),
Name varchar(255)
);

create table Product (
Id int primary key Identity(1,1),
Name varchar(255),
Description varchar(1000),
Price float,
Qte int,
CategoryId int,
foreign key (CategoryId) references Category(Id)
);
