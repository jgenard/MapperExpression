﻿using System;

namespace MapperCore.Exception
{
    public class NoFoundMapperException : System.Exception
    {

        public NoFoundMapperException(Type source, Type dest)
            : base("Le mapping pour les types '" + source.Name + "' et '" + dest.Name + "' ne sont pas configurés")
        {

        }        
    }
}