using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Schedule
{
    public class ErrorModel
    {
        public List<string> Errors { get; set; }

        public ErrorModel()
        {
            Errors = new List<string>();
        }

    }
}