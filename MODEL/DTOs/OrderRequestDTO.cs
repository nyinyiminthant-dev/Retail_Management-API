namespace BAL.IServices
{
    public class OrderRequestDTO
    {
       
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        
        public DateTime OrderDate { get; set; }
        
    }
}