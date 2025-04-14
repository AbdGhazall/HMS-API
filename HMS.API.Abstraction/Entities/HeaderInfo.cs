using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Abstraction.Entities
{
    public class HeaderInfo
    {
        [Required]
        [FromHeader]
        public string MachineName { get; set; } = string.Empty;

        [Required]
        [FromHeader]
        public string MachineIP { get; set; } = string.Empty;
    }
}