﻿using System.ComponentModel.DataAnnotations;

namespace ProjetoContatosMVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Digite seu login!")]
        public string Login { get; set; }
        [Required(ErrorMessage="Digite sua senha!")]
        public string Senha { get; set; }

    }
}
