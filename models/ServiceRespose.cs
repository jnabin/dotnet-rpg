using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.models
{
    public class ServiceRespose<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
    }
}