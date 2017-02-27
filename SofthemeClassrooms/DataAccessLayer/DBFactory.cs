using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public static class DBFactory
    {
        public static IDatabaseRepositories GetDbRepositories()
        {
            return  new DatajBaseRepositories();
        }
    }
}
