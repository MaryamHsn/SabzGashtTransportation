using AutoMapper;
namespace Sabz.ServiceLayer.Mapper
{
    public static class BaseMapper<T, TD>
        where T : class
        where TD : class
    {
        public static TD ToDto(T entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TD>());
            var mapper = config.CreateMapper();
            return mapper.Map<TD>(entity);
        }

        public static T ToEntity(TD dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TD, T>());
            var mapper = config.CreateMapper();
            return mapper.Map<T>(dto);
        }

        public static TD Map(T entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TD>());
            var mapper = config.CreateMapper();
            return mapper.Map<TD>(entity);
        }

        public static T Map(TD dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TD, T>());
            var mapper = config.CreateMapper();
            return mapper.Map<T>(dto);
        }
    }
}
