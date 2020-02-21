using Splat;
using StudentTest.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTest
{
    public class Startup
    {
        public Startup()
        {
            RegisterServices();
        }
        void RegisterServices()
        {
            Locator.CurrentMutable.RegisterConstant(new StudentService(), typeof(IStudentService));
            Locator.CurrentMutable.RegisterConstant(new EquipmentService(), typeof(IEquipmentService));
        }
    }
}
