using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities{

    public class Prestamo{
        public int id { get; set; }
        public int idbombero { get; set; }
        public string estado { get; set; }
        public string fechaprestamo { get; set; }
        public string fechadevolucion { get; set; }
        public string observaciones { get; set; }

        [NotMapped]
        public virtual Bombero? bombero { get; set; }

        
        public Prestamo()
        {
            
        }
        public Prestamo(int id, int idbombero,string estado,string fechaprestamo,string fechadevolucion, string observaciones)
        {
            this.id = id;
            this.idbombero = idbombero;
            this.estado = estado;
            this.fechaprestamo = fechaprestamo;
            this.fechadevolucion = fechadevolucion;
            this.observaciones = observaciones;
        }
    }
}
