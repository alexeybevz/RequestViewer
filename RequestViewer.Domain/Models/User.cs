﻿using System;

namespace RequestViewer.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public string Email { get; set; }
    }
}
