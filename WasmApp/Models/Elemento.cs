using System.ComponentModel.DataAnnotations.Schema;

namespace Entities{

    public class Elemento{
        public int id { get; set; }
        public int idCategoria { get; set; }
        public string estado { get; set; }
        public string nombre { get; set; }
        public int vidaUtil { get; set; }
        public string descripcion { get; set; }


        public virtual Categoria? categoria { get; set; }

        
        public Elemento()
        {
            
        }
        public Elemento(int id, int idCategoria,string nombre,string descripcion,string estado, int vidaUtil)
        {
            this.id = id;
            this.idCategoria = idCategoria;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.estado = estado;
            this.vidaUtil = vidaUtil;
        }
    }
}