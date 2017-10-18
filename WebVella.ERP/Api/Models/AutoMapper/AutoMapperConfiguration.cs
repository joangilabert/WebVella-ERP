﻿#region <--- DIRECTIVES --->

using System;
using AutoMapper;
using WebVella.ERP.Api.Models.AutoMapper.Profiles;
using WebVella.ERP.Api.Models.AutoMapper.Resolvers;
using AutoMapper.Configuration;

#endregion


namespace WebVella.ERP.Api.Models.AutoMapper
{
    public class AutoMapperConfiguration
    {
        private static object lockObj = new object();
        private static bool alreadyConfigured = false;

        public static void Configure(MapperConfigurationExpression cfg)
        {
            if (alreadyConfigured)
                return;

            lock (lockObj)
            {
                if (alreadyConfigured)
                    return;

                alreadyConfigured = true;

                cfg.CreateMap<Guid, string>().ConvertUsing<GuidToStringConverter>();
                cfg.CreateMap<DateTimeOffset, DateTime>().ConvertUsing<DateTimeTypeConverter>();
                cfg.AddProfile(new EntityRelationProfile());
                cfg.AddProfile(new EntityProfile());
                cfg.AddProfile(new RecordPermissionsProfile());
                cfg.AddProfile(new FieldPermissionsProfile());
                cfg.AddProfile(new FieldProfile());
                cfg.AddProfile(new RecordsListProfile());
                cfg.AddProfile(new RecordViewProfile());
                cfg.AddProfile(new RecordTreeProfile());
                cfg.AddProfile(new EntityRelationOptionsProfile());
                cfg.AddProfile(new JobProfile());
                cfg.AddProfile(new UserFileProfile());
                //Mapper.AddProfile(new RecordViewFieldProfile(service));

                cfg.CreateMap<EntityRecord, ErpUser>().ConvertUsing(new ErpUserConverter());
                cfg.CreateMap<ErpUser, EntityRecord>().ConvertUsing(new ErpUserConverterOposite());
                cfg.CreateMap<EntityRecord, ErpRole>().ConvertUsing(new ErpRoleConverter());
            }
        }
    }
}