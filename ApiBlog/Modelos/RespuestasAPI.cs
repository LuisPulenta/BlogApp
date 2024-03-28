﻿using System.Net;

namespace ApiBlog.Modelos
{
    public class RespuestasAPI
    {
        public RespuestasAPI()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsScuccess { get; set; }
        public List <string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
