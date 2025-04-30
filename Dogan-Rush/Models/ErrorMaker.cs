using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dogan_Rush.Models
{
    public static class ErrorMaker
    {

        public static void GenerateError(VISACard visa)
        {
            Random rnd = new Random();
            int errorPos = rnd.Next(0, 9);

            switch (errorPos)
            {
                case 0:
                    //cambiare birthdate, necessario PersonData
                    break;
                case 1:
                    //cambiare emissionDate, il tempo è di 5 anni.
                    break;
                case 2:
                    //cambiare expirationDate, il tempo è di 5 anni
                    break;
                case 3:
                    //cambiare Nome, cambiarlo totalmente o un solo char
                    break;
                case 4:
                    //cambiare PassportCode, cambiarlo di un solo char
                    break;
                case 5:
                    //cambiare lo stato emettitore
                    break;
                case 6:
                    //cambiare il sesso segnato
                    break;
                default:
                    //cambiare il cognome, totalmente o un solo char.
                    break;

            }
        }
    }
}