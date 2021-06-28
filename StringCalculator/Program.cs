using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Add("//@\n1@2\n77\n1@2\n6\n1@2\n78\n1@2\n5\n1@2\n63"));       // 244
            Console.WriteLine(Add("//;\n1;2\n3"));                                          // 6
            Console.WriteLine(Add("-1\n\n-2"));                                             // Negatives not allowed: -1,-2, \n-1
            Console.WriteLine(Add("1\n\n2"));                                               //3
            Console.WriteLine(Add("1,2,3,4,5,6"));                                          // 21
        }

        static int Add(string numbers)
        {
            try
            {
                if (numbers.Length > 0)
                {
                    string delimeter = Delimeter(numbers);
                    numbers = Sanitize(numbers);
                    return Sum(numbers, delimeter);
                }
                return 0;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }

        static string Sanitize(string command)
        {
            int _indexLine = command.IndexOf("\n");
            bool isCommand = command.IndexOf("//") > -1 ? true : false;

            if (isCommand)
            {
                command = command.Substring(_indexLine);
            }

            return command;
        }

        static string Delimeter(string command)
        {
            string delimeter = ",";
            int _indexLine = command.IndexOf("\n");
            bool isCommand = command.IndexOf("//") > -1 ? true : false;

            if (isCommand)
            {
                delimeter = command.Substring("//".Length, _indexLine - "//".Length);
            }

            return delimeter;
        }

        static int Sum(string digits, string delimeter = ",")
        {
            digits = digits.Replace("\n", delimeter);
            string[] split = digits.Split(delimeter);
            string[] negatives = new string[digits.Split(delimeter).Length];
            int i = 0;
            int sum = 0;

            foreach(string x in split)
            {
                int _n = 0;
                
                try
                {
                    _n = Int32.Parse(x);
                } catch (Exception e)
                {

                }

                if (_n > -1)
                {
                    sum += x.Length > 0 ? Int32.Parse(x) : 0;
                }
                else
                {
                    negatives[i] = x;
                    i++;
                }
            }

            if(i > 0)
            {
                string errors = string.Join(",", negatives);
                throw new Exception($"Negatives not allowed: {errors}");
            }

            return sum;
        }
    }
}
