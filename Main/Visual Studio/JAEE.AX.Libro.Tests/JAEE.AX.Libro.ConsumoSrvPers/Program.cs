using System;

using JAEE.AX.Libro.WSRef.JAEEServiciosPersonalizados;

namespace JAEE.AX.Libro.ConsumoSrvPers
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // El contexto indica a AX algunos aspectos de la conexión
                CallContext context = new CallContext();
                context.Company = "USMF";

                RegistrarAlbaran contrato = new RegistrarAlbaran();
                contrato.Pedido = "000020";
                contrato.Albaran = "PRUEBA-20";
                contrato.Fecha = DateTime.Now;
                
                // Llamada al servicio
                JAEERegistrarAlbaranServiceClient client = new JAEERegistrarAlbaranServiceClient();
                contrato = client.registrar(context, contrato);

                Console.WriteLine("Respuesta: {0}", contrato.Albaran_RecId);
            }
            catch
            {
                throw;
            }
        }
    }
}
