using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagment_DAL.Models;
namespace TransportManagment_DAL.Data
{
    public class DbInitializer
    {
        public static void Initialize(TrnspMngmntContext context)
        {

            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Cargoes.Any())
            {
                return;   // DB has been seeded
            }

            var trucktypes = new TruckType[]
            {
            new TruckType{TruckTypeID=10,TypeName="Tilt"},
            new TruckType{TruckTypeID=11,TypeName="Refrigirator"},
            new TruckType{TruckTypeID=12,TypeName="Metal Trailer"},
            new TruckType{TruckTypeID=13,TypeName="Insulated Truck"},
            new TruckType{TruckTypeID=14,TypeName="Car transporter"}
            };
            foreach (TruckType c in trucktypes)
            {
                context.TruckTypes.Add(c);
            }
            context.SaveChanges();

            //var companies = new Company[]
            //{
            //    new Company{Name="WhatisloveCorp",Email="kuplykvlad@gmail.com",Phone="+380963083105"},
            //    new Company{Name="VicitalCorp",Email="viki@gmail.com",Phone="+380502052146"},
            //    new Company{Name="ViktoriaCorp",Email="viktoria@gmail.com",Phone="+380963073808"}
            //};
            //foreach (Company c in companies)
            //{
            //    context.Companies.Add(c);
            //}
            //context.SaveChanges();

            //var cargos = new Cargo[]
            //{
            //new Cargo{StartOfShipping=DateTime.Parse("2019-05-02"),EndOfShipping=DateTime.Parse("2019-06-02"),Departure="Vinnitsia",
            //    Destination ="Kyiv",Info="Details",Weight=10000,Volume=123,
            //    CompanyID =companies.Single(i=>i.Name=="WhatisloveCorp").Id,TruckTypeID=10},
            //new Cargo{StartOfShipping=DateTime.Parse("2019-05-03"),EndOfShipping=DateTime.Parse("2019-06-03"),Departure="Zaporizzia",
            //    Destination ="Kyiv",Info="Milk",Weight=1000,Volume=100,
            //    CompanyID =companies.Single(i=>i.Name=="WhatisloveCorp").Id,TruckTypeID=11},
            //new Cargo{StartOfShipping=DateTime.Parse("2019-05-04"),EndOfShipping=DateTime.Parse("2019-06-04"),Departure="Vinnitsia",
            //    Destination ="Lviv",Info="Choklate",Weight=15000,Volume=90,
            //    CompanyID =companies.Single(i=>i.Name=="ViktoriaCorp").Id,TruckTypeID=11},
            //new Cargo{StartOfShipping=DateTime.Parse("2019-07-02"),EndOfShipping=DateTime.Parse("2019-08-02"),Departure="Kherson",
            //    Destination ="Odessa",Info="Souseges",Weight=9000,Volume=20,
            //    CompanyID =companies.Single(i=>i.Name=="VicitalCorp").Id,
            //    TruckTypeID =12},
            //new Cargo{StartOfShipping=DateTime.Parse("2019-08-02"),EndOfShipping=DateTime.Parse("2019-08-02"),Departure="Zhytomyr",
            //    Destination ="Simpheropol",Info="Armor",Weight=70000,Volume=50,
            //    CompanyID =companies.Single(i=>i.Name=="ViktoriaCorp").Id,TruckTypeID=14}
            //};
            //foreach (Cargo s in cargos)
            //{
            //    context.Cargoes.Add(s);
            //}
            //context.SaveChanges();

            //var transports = new Transport[]
            //{
            //new Transport{StartOfShipping=DateTime.Parse("2019-05-02"),EndOfShipping=DateTime.Parse("2019-06-02"),Departure="Zaporizzia",Destination="Vinnitsia",MaxWeight=10000,MaxVolume=123,CompanyID=companies.Single(i=>i.Name=="ViktoriaCorp").Id,TruckTypeID=10,Type=Models.Type.Truck},
            //new Transport{StartOfShipping=DateTime.Parse("2019-05-03"),EndOfShipping=DateTime.Parse("2019-06-03"),Departure="Zaporizzia",Destination="Kyiv",MaxWeight=1000,MaxVolume=100,CompanyID=companies.Single(i=>i.Name=="WhatisloveCorp").Id,TruckTypeID=11,Type=Models.Type.Semitrailer},
            //new Transport{StartOfShipping=DateTime.Parse("2019-05-04"),EndOfShipping=DateTime.Parse("2019-05-07"),Departure="Vinnitsia",Destination="Lviv",MaxWeight=15000,MaxVolume=90,CompanyID=companies.Single(i=>i.Name=="VicitalCorp").Id,TruckTypeID=11,Type=Models.Type.Truck},
            //new Transport{StartOfShipping=DateTime.Parse("2019-07-05"),EndOfShipping=DateTime.Parse("2019-07-06"),Departure="Mykolaiv",Destination="Odessa",MaxWeight=9000,MaxVolume=20,CompanyID=companies.Single(i=>i.Name=="VicitalCorp").Id,TruckTypeID=12,Type=Models.Type.DoubleRoadTrain},
            //new Transport{StartOfShipping=DateTime.Parse("2019-08-04"),EndOfShipping=DateTime.Parse("2019-09-05"),Departure="Zhytomyr",Destination="Simpheropol",MaxWeight=70000,MaxVolume=50,CompanyID=companies.Single(i=>i.Name=="ViktoriaCorp").Id,TruckTypeID=14,Type=Models.Type.DoubleRoadTrain}
            //};
            //foreach (Transport s in transports)
            //{
            //    context.Transports.Add(s);
            //}
            //context.SaveChanges();



        }
    }
}
