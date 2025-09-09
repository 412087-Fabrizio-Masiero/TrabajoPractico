create database ComercioInterior
go
use ComercioInterior
go
create table Articulos(
id int identity(1,1),
nombre varchar(20) not null,
precioUnitario decimal(10,2),
esta_activa int
CONSTRAINT pk_articulos PRIMARY KEY(id)
)

create table Formas_Pago(
id int identity(1,1),
nombre varchar(20) not null,
esta_activa int
CONSTRAINT pk_forma_pago PRIMARY KEY(id)
)


Create table Facturas(
id int identity(1,1),
fecha date,
pago int not null,
cliente varchar(20),
esta_activa int
CONSTRAINT pk_factura PRIMARY KEY(id),
CONSTRAINT fk_pago FOREIGN KEY (pago) REFERENCES Formas_Pago(id)
)

create table Detalles_Factura(
id int identity(1,1),
NroFactura int not null,
NroArticulo int not null,
Cantidad int,
esta_activa int
CONSTRAINT pk_detallefactura PRIMARY KEY(id),
CONSTRAINT fk_factura FOREIGN KEY(NroFactura) REFERENCES Facturas(id),
CONSTRAINT fk_articulo FOREIGN KEY(NroArticulo) REFERENCES Articulos(id)
)

insert into Articulos (nombre, precioUnitario, esta_activa) values
('Lapicera', 120.50, 1),
('Cuaderno', 450.00, 1),
('Goma', 75.00, 1),
('Regla', 150.00, 1),
('Cartuchera', 850.00, 1);

insert into Formas_Pago (nombre, esta_activa) values
('Efectivo', 1),
('Tarjeta Crédito', 1),
('Tarjeta Débito', 1),
('Transferencia', 1);
	

insert into Facturas (fecha, pago, cliente, esta_activa) values
('2025-09-01', 1, 'Juan Pérez', 1),
('2025-09-02', 2, 'María Gómez', 1),
('2025-09-03', 3, 'Carlos Díaz', 1);


insert into Detalles_Factura (NroFactura, NroArticulo, Cantidad, esta_activa) values
(1, 1, 2, 1), 
(1, 2, 1, 1), 
(1, 3, 3, 1);


insert into Detalles_Factura (NroFactura, NroArticulo, Cantidad, esta_activa) values
(2, 2, 2, 1), 
(2, 5, 1, 1);


insert into Detalles_Factura (NroFactura, NroArticulo, Cantidad, esta_activa) values
(3, 4, 2, 1), 
(3, 1, 1, 1); 
