using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace AplicacaoFaculdade {
    public class Crypter {

        public static string Hash(string str) {

            byte[] hash;
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256CryptoServiceProvider crypt = new SHA256CryptoServiceProvider()) {
                hash = crypt.ComputeHash(Encoding.Unicode.GetBytes(str));
            }

            foreach ( byte b in hash ) {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }

    }
}