namespace NewRestaurant.Dto.order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public int? WaiterId { get; set; }
        public DateTime? Created { get; set; }
    }
}
