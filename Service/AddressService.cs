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
        private readonly List<Address> _allAddresses;
        public AddressService(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            _allAddresses = _addressRepository.GetAllAddresses();
        }

        public void Create(Address address)
        {
            _addressRepository.Create(address);
        }

        public void Update(Address address)
        {
            _addressRepository.Update(address);
        }
        public List<Address> getAll()
        {
            return _addressRepository.GetAllAddresses();
        }

        public List<string> getAllCities()
        {
            return _addressRepository.GetAllCities();
        }

        public int GenerateId()
        {
            int id = 1;
            foreach(Address address in _allAddresses)
            {
                if (address.Id >= id)
                    id = address.Id + 1;
            }
            return id;
        }
    }
}
