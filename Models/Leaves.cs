namespace hr_app.api.Models
{
    public class Leaves
    {
        public int Leave_ID { get; set; }
        public int Number_of_days { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int Employee_ID { get; set; }
    }
}
