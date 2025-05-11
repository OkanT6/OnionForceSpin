using AutoMapper;
using AutoMapper.Internal;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMapper = AutoMapper.IMapper;


namespace OnionForceSpin.Mapper.AutoMapper
{
    public class Mapper : Application.Interfaces.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = new();
        private IMapper MapperContainer;

        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        // 🔄 Eklenen kısım: Mevcut bir nesne üzerinde mapleme
        public void Map<TSource, TDestination>(TSource source, TDestination destination, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);
            MapperContainer.Map(source, destination); // Var olan nesne üzerinde değişiklik
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(5, ignore);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            Config<TDestination, IList<object>>(5, ignore);
            return MapperContainer.Map<IList<TDestination>>(source);
        }

        protected void Config<TDestionation, TSource>(int depth = 5, string? ignore = null)
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestionation));

            // TypePair zaten var ise, tekrar eklemiyoruz
            if (typePairs.Any(a => a.DestinationType == typePair.DestinationType && a.SourceType == typePair.SourceType) && ignore is null)
                return;

            typePairs.Add(typePair);

            // MapperConfiguration'ı sadece bir kez oluşturuyoruz
            if (MapperContainer == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var item in typePairs)
                    {
                        var mapConfig = cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth);

                        if (ignore is not null)
                            mapConfig.ForMember(ignore, x => x.Ignore());

                        mapConfig.ReverseMap();
                    }
                });

                MapperContainer = config.CreateMapper();
            }
        }


    }
}
