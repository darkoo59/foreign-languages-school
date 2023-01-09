using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using ForeignLanguagesSchool.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForeignLanguagesSchool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal AddressService addressService;
        internal SchoolService schoolService;
        internal UserService userService;
        internal ClassService classService;
        internal User loggedUser;

        public App()
        {
            loggedUser = null;
            AddressRepository addressRepository = new AddressRepository();
            addressService = new AddressService(addressRepository);

            SchoolRepository schoolRepository = new SchoolRepository();
            schoolService = new SchoolService(schoolRepository);

            UserRepository userRepository = new UserRepository();
            userService = new UserService(userRepository, schoolService);

            ClassRepository classRepository = new ClassRepository();
            classService = new ClassService(classRepository);
        }
    }
}
