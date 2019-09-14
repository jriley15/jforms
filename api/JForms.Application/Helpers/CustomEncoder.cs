using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Application.Helpers
{
    public class CustomEncoder
    {

        private static readonly string Alphabet = "UX2kQGjDq53srlNbcEwnCP6LWpy0HfMoT9VhZIRmF4zB1YOt7vu8xSiKaegJAd";

        private static readonly int Base = 62;

        public static string encode(int id)
        {
            var hash = "";

            do
            {
                hash = Alphabet[id % Base] + hash;
                id = id / Base;

            } while(id > 0);
            return hash;
        }

        public static int decode(string id)
        {

            int number = 0;

            for (int i = id.Length - 1, digit = 0; i >= 0; i--, digit++)
            {
                var num = Alphabet.IndexOf(id[i]);
                number += num * ((int)Math.Pow(Base, digit));

            }

            return number;

        }

    }
}
