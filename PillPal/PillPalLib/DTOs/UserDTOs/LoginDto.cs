using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.DTOs.UserDTOs
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = "";
    }
}
