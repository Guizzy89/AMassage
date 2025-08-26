public class Booking
{
    public int Id { get; set; }
    public DateTime SlotDate { get; set; }
    public bool IsAvailable { get; set; }
    public string ClientName { get; set; }
    public string PhoneNumber { get; set; }
    public string Comment { get; set; }
}