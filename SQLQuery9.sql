create database ComercioInterior

use ComercioInterior

create table Articulos(
id int identity(1,1),
nombre varchar(20) not null,
precioUnitario decimal(10,2)
CONSTRAINT pk_articulos PRIMARY KEY(id)
)

create table Formas_Pago(
id int identity(1,1),
nombre varchar(20) not null
CONSTRAINT pk_forma_pago PRIMARY KEY(id)
)


Create table Facturas(
id int identity(1,1),
fecha date,
pago int not null,
cliente varchar(20)
CONSTRAINT pk_factura PRIMARY KEY(id),
CONSTRAINT fk_pago FOREIGN KEY (pago) REFERENCES Formas_Pago(id)
)

create table Detalles_Factura(
id int identity(1,1),
factura int not null,
articulo int not null,
cantidad int
CONSTRAINT pk_detallefactura PRIMARY KEY(id),
CONSTRAINT fk_factura FOREIGN KEY(factura) REFERENCES Facturas(id),
CONSTRAINT fk_articulo FOREIGN KEY(articulo) REFERENCES Articulos(id)

)