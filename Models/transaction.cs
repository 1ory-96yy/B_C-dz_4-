namespace ConsoleApp1.Models
{
    public class transaction
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public double amount { get; set; }
        public DateTime timestamp { get; set; }

        public transaction(string from, string to, double amount)
        {
            this.from = from;
            this.to = to;
            this.amount = amount;
            this.timestamp = DateTime.Now;
            this.id = Guid.NewGuid().ToString();
        }

        public string ToRowString()
                    {
            return $"{id}\t{from}\t{to}\t{amount}\t{timestamp}";
        }
        public override string ToString()
        {
            return $"Transaction from {from} to {to} of amount {amount} at {timestamp}";
        }


    }
}
