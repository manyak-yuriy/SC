using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DBRepository
    {
        private SofthemeClassroomsDBcontext db = new SofthemeClassroomsDBcontext();

        public IEnumerable<AspNetUsers> Users
        {
            get
            {
                return db.AspNetUsers;
            }
        }

        public IEnumerable<AspNetUserClaims> Claims
        {
            get
            {
                return db.AspNetUserClaims;
            }
        }

        public IEnumerable<>
    }
}
