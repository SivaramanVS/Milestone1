using System;
using System.Collections.Generic;
using System.Linq;
using BusinessService.Data.DBModel;

namespace BusinessService.Domain.Services
{
    public class SchoolManagementServiceFake : ISchoolManagementService
    {
        private readonly List<SchoolItem> _schoolCart;

        public SchoolManagementServiceFake()
        {
            _schoolCart = new List<SchoolItem>()
            {
                new SchoolItem() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Sivaraman", Principal="Uma Sridhar", Fees = 1.00M },
                new SchoolItem() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Viswanath", Principal="Rajagopal", Fees = 4.00M },
                new SchoolItem() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Vignesh", Principal ="Rakesh", Fees = 3.00M }
            };
        }

        public IEnumerable<SchoolItem> GetAllItems()
        {
            return _schoolCart;
        }

        public SchoolItem Add(SchoolItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            _schoolCart.Add(newItem);
            return newItem;
        }

        public SchoolItem GetById(Guid id)
        {
            return _schoolCart.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var existing = _schoolCart.First(a => a.Id == id);
            _schoolCart.Remove(existing);
        }
    }
}
