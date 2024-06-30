﻿using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IInvoiceItemRepository : IRepository<InvoiceItem>
    {
        void Update(InvoiceItem obj);
    }
}
