
CREATE DATABASE GestionEnvios;
GO

USE GestionEnvios;
GO


CREATE TABLE Producto (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL
);



CREATE TABLE Envio (
    id INT IDENTITY(1,1) ,
    fecha DATE NOT NULL,
    dniCliente INT	 NOT NULL,
    direccion NVARCHAR(200) NOT NULL,
    palabraSecreta NVARCHAR(50) NOT NULL,
    estado NVARCHAR(20) NOT NULL
	CONSTRAINT PK_Envio PRIMARY KEY(id)

);


CREATE TABLE DetalleEnvio (
    id INT IDENTITY(1,1) ,
    idEnvio INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    comentario NVARCHAR(200) NULL,
	CONSTRAINT PK_DetalleEnvio PRIMARY KEY(id),
    CONSTRAINT FK_DetalleEnvio_Envio FOREIGN KEY (idEnvio)
        REFERENCES Envio(id),
    CONSTRAINT FK_DetalleEnvio_Producto FOREIGN KEY (idProducto)
        REFERENCES Producto(id)
);


INSERT INTO Producto (nombre, precio) VALUES
('Laptop Lenovo', 850000.00),
('Mouse Logitech', 15000.00),
('Teclado Mecánico', 35000.00),
('Monitor Samsung 24"', 120000.00),
('Impresora HP', 95000.00);


INSERT INTO Envio (fecha, dniCliente, direccion, palabraSecreta, estado) VALUES
('2025-09-26', '40888999', 'Av. Siempre Viva 742, Córdoba', 'clave123', 'Pendiente'),
('2025-09-27', '39777111', 'Calle Falsa 123, Buenos Aires', 'secretoX', 'En camino'),
('2025-09-28', '42666777', 'San Martín 555, Rosario', 'entregarYA', 'Entregado');

INSERT INTO DetalleEnvio (idEnvio, idProducto, cantidad, comentario) VALUES
(1, 1, 1, 'Entregar con factura A'),
(1, 2, 2, 'Cliente pidió embalaje especial'),
(2, 3, 1, NULL),
(2, 4, 1, 'Revisar stock antes de enviar'),
(3, 5, 1, 'Cliente solicitó instalación incluida');
