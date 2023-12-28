using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Services.ForDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Controller
{
    public class RegionControllerTest 
    {

        private readonly IRegion_Service _regions;
        public RegionControllerTest()
        {
            _regions = A.Fake<IRegion_Service>();
        }


    }
}
