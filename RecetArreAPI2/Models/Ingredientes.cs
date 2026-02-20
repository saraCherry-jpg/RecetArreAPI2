using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.Models
{
    public class Ingredientes
    {
        public int Id { get; set; } //ID_Ingredientes --lo detecta automaticamente todas las ID
        [Required]
        [StringLength(100, MinimumLength = 2)]

        public string Nombre { get; set; } = default!;  //nombre del ingrediente 
        [StringLength(500)]

        public string UnidadMedida { get; set; } = String.Empty; //unidad de medida --> se manejará como un combobox 
        [StringLength(50)]
        public string? Descripcion { get; set; } //descripcion del ingrediente
        [StringLength(500)]

        public DateTime CreadoUtc { get; set; } = DateTime.UtcNow; //fecha de creacción 


        //NOTA: --el signo de ? es opcional para por si los datos que esta ingresando es seguro.
        //          Por otroa lado si quieres dejar los valores null se recomienda agregar = default! 
        //   Equivale NULL => = default!


    }
}
