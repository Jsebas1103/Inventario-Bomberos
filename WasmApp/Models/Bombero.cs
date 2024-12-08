namespace Entities{

    public class Bombero{
        public int id { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string acceso { get; set; }
        

        public Bombero(int id,string nombre, int edad, string direccion, string telefono, string correo, string contraseña, string acceso )
        {
            this.id=id;
            this.nombre=nombre;
            this.edad=edad;
            this.direccion=direccion;
            this.telefono=telefono;
            this.correo=correo;
            this.contraseña=contraseña;
            this.acceso=acceso;
        }

        public Bombero(){

        }

        public override string ToString()
        {
            return $"id: {id},  nombre: {nombre}, edad: {edad}, direccion: {direccion}, telefono: {telefono}, correo {correo}, contrasena: {contraseña}, acceso: {acceso}";
        }


    }
}

