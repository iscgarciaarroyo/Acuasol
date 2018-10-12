using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acuasol.Backend.Helpers
{
    using Dominio;
    using System;
    using System.Threading.Tasks;
    using Models;
    
    public class DBHelper
    {
        public async static Task<Respuesta> SaveChanges(LocalDataContext db)
        {
            try
            {
                await db.SaveChangesAsync();
                return new Respuesta { Exito = true, };
            }
            catch (Exception ex)
            {
                var Respuesta = new Respuesta { Exito = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                {
                    Respuesta.Mensaje = "There is a record with the same value";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    Respuesta.Mensaje = "The record can't be delete because it has related records";
                }
                else
                {
                    Respuesta.Mensaje = ex.Message;
                }

                return Respuesta;
            }
        }
    }
}