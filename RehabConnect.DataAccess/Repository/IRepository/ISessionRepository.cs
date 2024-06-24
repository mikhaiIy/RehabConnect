﻿using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface ISessionRepository : IRepository<Session>
    {
        void Update(Schedule obj);
    }
}
