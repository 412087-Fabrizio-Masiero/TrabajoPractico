using ComercioInterior.Domain;
using ComercioInterior.Services;

ArticuloService ar =  new ArticuloService();
FacturaService facturaService = new FacturaService();


List<Articulos> lp = ar.GetAll();


if (lp.Count > 0)
    foreach (Articulos p in lp)
        Console.WriteLine(p);
else
    Console.WriteLine("No hay productos...");


List<Facturas> lfp = facturaService.GetAll();


if (lfp.Count > 0)
    foreach (Facturas p in lfp)
        Console.WriteLine(p);
else
    Console.WriteLine("No hay productos...");


//Facturas facturas = new Facturas()
//{
//    Codigo = 0,
//    Fecha = DateTime.Now,
//    Cliente = "Fabrizio",
//    Pago = 1
//};

//var articulo1 = ar.GetById(1);
//var articulo2 = ar.GetById(2);

//if (articulo1 != null)
//{
//    facturas.detalleFacturas.Add(new DetalleFactura()
//    {
//        NroArticulo = articulo1,
//        Cantidad = 3
//    });
//}

//if (articulo2 != null)
//{
//    facturas.detalleFacturas.Add(new DetalleFactura()
//    {
//        NroArticulo = articulo2,
//        Cantidad = 2
//    });
//}

//bool resultado = facturaService.Save(facturas);

//if (resultado)
//    Console.WriteLine("Factura creada exitosamente.");
//else
//    Console.WriteLine("Error al crear la factura.");