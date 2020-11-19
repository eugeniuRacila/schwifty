namespace Customer.Models
{
    public class Package<T>
    {
        // change to ENUMS
        private string _type;
        private T _payload;

        public Package(string type, T payload)
        {
            _type = type;
            _payload = payload;
        }
    }
}