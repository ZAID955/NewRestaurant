namespace NewRestaurant.Dto.Item
{
    public class UpdateItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public string? Catagory { get; set; }
    }
}
