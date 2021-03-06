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
                c.CreateMap<BAL.Model.Notification, Data.Notification>();
                c.CreateMap<BAL.Model.Leave, Data.Leave>();
                c.CreateMap<Data.Leave, BAL.Model.Leave>();
                c.CreateMap<BAL.Model.LiveClass, Data.LiveClass>();
                c.CreateMap<Data.LiveClass, BAL.Model.LiveClass>();
                c.CreateMap<Data.StAttendance, BAL.Model.StuAttendance>();
                c.CreateMap<BAL.Model.StuAttendance, Data.StAttendance>();
                c.CreateMap<Data.Feedback, BAL.Model.Feedback>();
                c.CreateMap<BAL.Model.Feedback, Data.Feedback>();
                c.CreateMap<Data.Event, BAL.Model.Event>();
                c.CreateMap<BAL.Model.Event, Data.Event>();
            });
        }

    }
}