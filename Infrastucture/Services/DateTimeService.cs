using Blog.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastucture.Services
{
    public class DateTimeService:IDateService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
