using System;
using System.Collections.Generic;
using BusinessService.Data.DBModel;

namespace BusinessService.Domain.Services
{
    public interface ISchoolManagementService
    {
        IEnumerable<SchoolItem> GetAllItems();
        SchoolItem Add(SchoolItem newItem);
        SchoolItem GetById(Guid id);
        void Remove(Guid id);
    }
}
