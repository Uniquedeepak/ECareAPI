﻿using AutoMapper;
using ECare.BAL.Model;
using ECare.Data;

namespace ECare.API.Models
{
    public class AutoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<AdmissionForm, Student>();
                c.CreateMap<StudentFeeDetail, StudentFee>();
                c.CreateMap<Data.School, BAL.Model.School>();
                c.CreateMap<Data.Notification, BAL.Model.Notification>();
                c.CreateMap<Data.tbl_homework, Homework>();
            });
        }

    }
}