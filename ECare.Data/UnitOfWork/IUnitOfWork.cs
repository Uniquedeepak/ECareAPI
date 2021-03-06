﻿using GenericAPI.Repository;
using ECare.Data;

namespace GenericAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<AdmissionForm> AdmissionFormRepository { get; }
        IGenericRepository<Fine> FineRepository { get; }
        IGenericRepository<Class> ClassRepository { get; }
        IGenericRepository<tbl_homework> HomeworkRepository { get; }
        IGenericRepository<EmployeeEntry> EmployeeRepository { get; }
        IGenericRepository<TransportCharge> TransportRepository { get; }
        IGenericRepository<TeacherAss> TeacherRepository { get; }
        IGenericRepository<School> SchoolRepository { get; }
        IGenericRepository<Session> SessionRepository { get; }
        IGenericRepository<InventoryCategory> InventoryCategoryRepository { get; }
        IGenericRepository<InventoryItem> InventoryItemRepository { get; }
        IGenericRepository<InventoryIssue> InventoryIssueRepository { get; }
        IGenericRepository<StudentFeeDetail> StudentFeeDetailRepository { get; }
        IGenericRepository<Month> MonthRepository { get; }
        IGenericRepository<NewFeeHeading> FeeHeadingRepository { get; }
        IGenericRepository<House> HouseRepository { get; }
        IGenericRepository<Hobby> HobbyRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<Leave> LeaveRepository { get; }
        IGenericRepository<LiveClass> LiveClassRepository { get; }
        IGenericRepository<Album> AlbumRepository { get; }
        IGenericRepository<Photo> PhotoRepository { get; }
        IGenericRepository<Feedback> FeedbackRepository { get; }
        IGenericRepository<Event> EventRepository { get; }

        void Save();
    }
}