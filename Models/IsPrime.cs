namespace numbers.Models
{
    public class IsPrime 
    {
        private int _num;
        public bool prime { get; set; }

        public IsPrime(int num)
        {
            _num = num;
        }
    }
}