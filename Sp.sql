
--Facturas
CREATE PROCEDURE sp_ObtenerFactura
AS
BEGIN
    	SELECT * FROM Facturas
END;

CREATE PROCEDURE sp_ObtenerFactura_Por_Id

	@id int
AS
BEGIN
	SELECT * FROM Facturas WHERE id = @id;
END


CREATE PROCEDURE sp_Guardar_Factura

@id int ,
@fecha datetime,
@pago int ,
@cliente varchar(20)
AS
BEGIN 

	BEGIN
		insert into Facturas(fecha, pago, cliente) 
		values (@fecha,@pago, @cliente)	
	END
END


CREATE PROCEDURE sp_Modificar_Factura
@id int ,
@fecha datetime,
@pago int,
@cliente varchar(20)
AS
BEGIN 

	BEGIN
		update Facturas 
			set pago= @pago, cliente= @cliente
			where id=@id
	END
END

CREATE PROCEDURE sp_Eliminar_Factura
@id int 
AS
BEGIN 
	
	BEGIN
		update Facturas
			set esta_activa= 0
			where id=@id
	END
END


--Articulos
CREATE PROCEDURE sp_ObtenerArticulos
AS
BEGIN
    	SELECT * FROM Articulos
END;

CREATE PROCEDURE sp_ObtenerArticulos_Por_Id

	@id int
AS
BEGIN
	SELECT * FROM Articulos WHERE id = @id;
END


CREATE PROCEDURE sp_Guardar_Articulo

@id int ,
@nombre varchar(20),
@precioUnitario decimal(10,2)
AS
BEGIN 

	BEGIN
		insert into Articulos(nombre, precioUnitario, esta_activa) 
		values (@nombre,@precioUnitario, 1)	
	END
END


CREATE PROCEDURE sp_Modificar_Articulo
@id int ,
@nombre varchar(20),
@precioUnitario decimal(10,2)
AS
BEGIN 

	BEGIN
		update Articulos 
			set nombre= @nombre, precioUnitario= @precioUnitario 
			where id=@id
	END
END

CREATE PROCEDURE sp_Eliminar_Articulo
@id int 
AS
BEGIN 
	
	BEGIN
		update Articulos 
			set esta_activa= 0
			where id=@id
	END
END
--Detalle
CREATE PROCEDURE sp_Guardar_Detalle
    @nroFactura INT,
    @nroArticulo INT,
    @cantidad INT
AS
BEGIN
    INSERT INTO Detalles_Factura (NroFactura, NroArticulo, Cantidad,esta_activa)
    VALUES (@nroFactura, @nroArticulo, @cantidad, 1);
END

CREATE PROCEDURE sp_ObtenerPagoPorId
    @idFactura  INT
AS
BEGIN
    SELECT 
        id,
        nombre,
        esta_activa
    FROM Formas_Pago
    WHERE id = @idFactura
END
GO

CREATE PROCEDURE sp_Modificar_DetalleFactura
    @id INT,
    @facturaId INT,
    @articuloId INT,
    @cantidad INT
AS
BEGIN
    UPDATE Detalles_Factura
    SET NroArticulo = @articuloId,
        Cantidad = @cantidad
    WHERE id = @id AND NroFactura = @facturaId;
END