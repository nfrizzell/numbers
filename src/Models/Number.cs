namespace numbers.Models
{
    public class Number
    {
        private int _num;
        public bool isPrime { get; set; }

        public Number(int num)
        {
            _num = num;
        }
    }
}