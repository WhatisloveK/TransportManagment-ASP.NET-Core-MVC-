
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagment_DAL.Data;
using TransportManagment.Models;
using TransportManagment_DAL.Models;

namespace TransportManagment.Services
{
    public class CargoService
    {
        TrnspMngmntContext _dataBase;
        UserManager<Company> _userManager;

        public CargoService(TrnspMngmntContext dataBase, UserManager<Company> userManager)
        {
            _dataBase = dataBase;
            _userManager = userManager;
        }
        public CargoDTO GetDetailsAsync(int? id)
        {
            var cargo = _dataBase.Cargoes
               .Include(x => x.Company)
               .Include(c => c.TruckType)
               .FirstOrDefault(m => m.ID == id);
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDTO>();
                cfg.CreateMap<Company, CompanyDTO>().ForMember(d => d.Cargoes, a => a.Ignore());
                cfg.CreateMap<TruckType, TruckTypeDTO>();
            });
            var cargoDTO = mapperCfg.CreateMapper().Map<Cargo, CargoDTO>(cargo);

            return cargoDTO;
        }

        public IEnumerable<CargoDTO> GetCargoDTOs()
        {
            var cargos  = _dataBase.Cargoes.Include(c => c.Company).Include(c => c.TruckType);
            var mappingCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDTO>();
                cfg.CreateMap<TruckType, TruckTypeDTO>();
                cfg.CreateMap<Company, CompanyDTO>().ForMember(d => d.Cargoes, a => a.Ignore());
            });
            var cargosDto = mappingCfg.CreateMapper().Map<IEnumerable<Cargo>, IEnumerable<CargoDTO>>(cargos);
            return cargosDto;
        }


    }
}
