namespace SharedClass.Components.Data
{
    public class PCComponent
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public Action OnClickAction { get; set; }
    }
}
