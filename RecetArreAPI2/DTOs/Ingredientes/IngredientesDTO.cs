namespace RecetArreAPI2.DTOs.Ingredientes
{
    public class IngredientesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = String.Empty;
        public string? Descripcion { get; set; }
        public DateTime CreadoUtc { get; set; }
    }

    //METODO CRUD EN INGREDIENTES
    public class IngredientesCreacionDto //ADD - INSERT 
    {
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = String.Empty;
        public string? Descripcion { get; set; }
    }

    public class IngredientesModificacionDto //UPDATE
    {
        public string Nombre { get; set; } = default!;
        public string UnidadMedida { get; set; } = String.Empty;
        public string? Descripcion { get; set; }
    }


}
