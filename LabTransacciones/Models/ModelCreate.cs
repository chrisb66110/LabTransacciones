﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabTransacciones.Models
{
    public class ModelCreate
    {
        public string SiglaExam { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string NombreP { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Descripcion { get; set; }
    }
}