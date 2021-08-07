using numbers.Models;
using System.Collections.Generic;
using System.Linq;

namespace numbers.Services
{
    public static class NumberService
    {
        private static List<Number> numbers;

        static NumberService()
        {
            numbers = new List<Number>();
        }

        public static Number Get(int num)
        {
            return new Number(num);
        }

        public static void Add(int num)
        {
            Number newNum = new Number(num);
            numbers.Add(newNum);
        }
    }
}