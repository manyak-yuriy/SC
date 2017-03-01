using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Extensions
{
    public static class StringExtensions
    {
        public static string DeleteExtraSpaces(this string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return str;
            }

            StringBuilder result = new StringBuilder();

            int lenght = str.Length;
            for (int i = 0, spaces = 0; i < lenght; ++i)
            {
                if (str[i] == ' ')
                {
                    if (result.Length != 0 && spaces == 0)
                    {
                        result.Append(' ');
                    }

                    ++spaces;
                }    
                else if (str[i] != ' ')
                {
                    spaces = 0;
                    result.Append(str[i]);
                }
            }

            if (result[result.Length-1] == ' ')
            {
                result.Remove(result.Length-1, 1);
            }

            return result.ToString();
        }
    }
}
