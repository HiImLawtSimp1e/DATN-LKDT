using AutoMapper;
using System.Linq.Expressions;
using AutoMapper.Configuration;

using shop.Infrastructure.Model.Common.Pagination;
using AutoMapper.EquivalencyExpression;

namespace shop.Infrastructure.Utilities
{
    public static class AutoMapperUtils
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, delegate (IMemberConfigurationExpression<TSource, TDestination, object> config)
            {
                config.Ignore();
            });
            return map;
        }

        public static IMapper GetMapper<TSource, TDestination>()
        {
            return new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression cfg)
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None);
            }));
        }

        public static IMapper GetIgnoreNullMapper<TSource, TDestination>()
        {
            return new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression cfg)
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None).ForAllMembers(delegate (IMemberConfigurationExpression<TSource, TDestination, object> opts)
                {
                    opts.Condition((TSource src, TDestination dest, object srcMember) => srcMember != null);
                });
            }));
        }

        private static IMapper GetMapper<TSource, TDestination>(string idString)
        {
            return new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression cfg)
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None).ForSourceMember(idString, delegate (ISourceMemberConfigurationExpression s)
                {
                    s.DoNotValidate();
                }).ForMember(idString, delegate (IMemberConfigurationExpression<TSource, TDestination, object> s)
                {
                    s.Ignore();
                });
            }));
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source)
        {
            return GetMapper<TSource, TDestination>().Map<TDestination>(source);
        }

        public static TDestination AutoMapIgnore<TSource, TDestination>(TSource source, string idString)
        {
            return GetMapper<TSource, TDestination>(idString).Map<TDestination>(source);
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source, string idString)
        {
            return GetMapper<TSource, TDestination>(idString).Map<TDestination>(source);
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source, TDestination dest)
        {
            dest = GetMapper<TSource, TDestination>().Map(source, dest);
            return dest;
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source, TDestination dest, string idString)
        {
            dest = GetMapper<TSource, TDestination>(idString).Map(source, dest);
            return dest;
        }

        public static IList<TDestination> AutoMap<TSource, TDestination>(IList<TSource> source)
        {
            return GetMapper<TSource, TDestination>().Map<IList<TDestination>>(source);
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source)
        {
            return GetMapper<TSource, TDestination>().Map<List<TDestination>>(source);
        }

        public static IList<TDestination> AutoMap<TSource, TDestination>(IList<TSource> source, string idString)
        {
            return GetMapper<TSource, TDestination>(idString).Map<IList<TDestination>>(source);
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, string idString)
        {
            return GetMapper<TSource, TDestination>(idString).Map<List<TDestination>>(source);
        }

        public static IList<TDestination> AutoMap<TSource, TDestination>(IList<TSource> source, IList<TDestination> dest)
        {
            dest = GetMapper<TSource, TDestination>().Map(source, dest);
            return dest;
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, List<TDestination> dest)
        {
            dest = GetMapper<TSource, TDestination>().Map(source, dest);
            return dest;
        }

        public static IList<TDestination> AutoMap<TSource, TDestination>(IList<TSource> source, IList<TDestination> dest, string idString)
        {
            dest = GetMapper<TSource, TDestination>(idString).Map(source, dest);
            return dest;
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, List<TDestination> dest, string idString)
        {
            dest = GetMapper<TSource, TDestination>(idString).Map(source, dest);
            return dest;
        }

        public static Pagination<TDestination> AutoMap<TSource, TDestination>(Pagination<TSource> source)
        {
            return ((IMapperBase)new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression cfg)
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None);
                cfg.CreateMap<Pagination<TSource>, Pagination<TDestination>>(MemberList.None);
            }))).Map<Pagination<TDestination>>((object)source);
        }

        public static Pagination<TDestination> AutoMap<TSource, TDestination>(Pagination<TSource> source, IMapper mapper)
        {
            return mapper.Map<Pagination<TDestination>>(source);
        }
    }
}
