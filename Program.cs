using ComercioInterior.Domain;
using ComercioInterior.Services;

ArticuloService ar =  new ArticuloService();
FacturaService facturaService = new FacturaService();


List<Articulos> lp = ar.GetAll();

// Manejamos la respuesta
if (lp.Count > 0)
    foreach (Articulos p in lp)
        Console.WriteLine(p);
else
    Console.WriteLine("No hay productos...");


List<Facturas> lfp = facturaService.GetAll();

// Manejamos la respuesta
if (lfp.Count > 0)
    foreach (Facturas p in lfp)
        Console.WriteLine(p);
else
    Console.WriteLine("No hay productos...");
