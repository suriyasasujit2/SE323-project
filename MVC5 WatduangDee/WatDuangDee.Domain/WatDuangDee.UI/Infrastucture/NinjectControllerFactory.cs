using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WatDuangDee.Domain.Abstract;
using WatDuangDee.Domain.Concrete;
using WatDuangDee.Domain.Entities;
namespace WatDuangDee.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext
        requestContext, Type controllerType)
        {
            return controllerType == null
           ? null
           : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // put bindings here
            ninjectKernel.Bind<IRepository>().To<EFRepository>();
          //  Mock<IRepository> mock = new Mock<IRepository>();
            //mock.Setup(m => m.RegisteredUser).Returns(new List<RegisteredUser> {
    //new RegisteredUser { Name = "Football", Surname = "25" },
    //new RegisteredUser { Name = "Surf board", Surname = "179" },
    //new RegisteredUser { Name = "Running shoes", Surname = "95"}
    //}.AsQueryable());
      //    ninjectKernel.Bind<IRepository>().ToConstant(mock.Object);
        }
    }
}
