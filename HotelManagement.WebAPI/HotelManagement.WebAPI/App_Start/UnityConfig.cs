using System;
using HotelManagement.WebAPI.Data;
using HotelManagement.WebAPI.Repositories;
using Unity;
using Unity.Lifetime;

namespace HotelManagement.WebAPI
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            // DbContext
            container.RegisterType<HotelDbContext>(new HierarchicalLifetimeManager());

            // Repositories
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IHotelRepository, HotelRepository>();
            container.RegisterType<IRoomTypeRepository, RoomTypeRepository>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IReservationRepository, ReservationRepository>();
            container.RegisterType<IServiceRepository, ServiceRepository>();
            container.RegisterType<IReservationServiceRepository, ReservationServiceRepository>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
        }
    }
}