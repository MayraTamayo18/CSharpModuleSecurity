namespace Entity.Model.Security
{
    public class View
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        //nombre de la relacion
        public int ModuloId { get; set; }
        //referencia de la relacion
        public Modulo Module { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdateAt { get; set; }
        public DateTime ? DeletedAt { get; set; }
        public bool State { get; set; }
    }
}
