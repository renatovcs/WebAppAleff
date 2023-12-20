﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAleff.Application.Helpers
{

    //classe para criptografia da senha conforme exigido pelo desafio.

    public static class Cripto
    {
        public static string HashMD5(string val)
        {
            var bytes = Encoding.ASCII.GetBytes(val);

            var md5 = MD5.Create();

            var hash = md5.ComputeHash(bytes);

            var ret = string.Empty;

            for (int i = 0; i < hash.Length; i++)
            {
                ret += hash[i].ToString("x2");
            }

            return ret;

        }
    }
}
