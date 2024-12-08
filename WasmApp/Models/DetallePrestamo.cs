using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

    public class DetallePrestamo{
        
        public int idPrestamo { get; set; }
        public int idElemento { get; set; }
        
        public virtual Elemento? elemento {get; set;}

        public virtual Prestamo? prestamo {get; set;}
        

        
        public DetallePrestamo()
        {
            
        }

        public DetallePrestamo(int idElemento, int idPrestamo)
        {
            this.idElemento=idElemento;
            this.idPrestamo=idPrestamo;
        }
        
        
    }
