using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Service
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;
        public AddressService(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public List<Address> getAll()
        {
            return _addressRepository.GetAllAddresses();
        }

        public List<string> getAllCities()
        {
            return _addressRepository.GetAllCities();
        }
    }
}
