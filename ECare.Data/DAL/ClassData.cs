using GenericAPI.UnitOfWork;
using ECare.Data.BAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECare.Data.DAL
{
    public class ClassData
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string SchoolSession;
        public ClassData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
            SchoolSession = PropertiesConfiguration.ActiveSession;
        }

        // GET api/school/5
        public List<Class> GetClasses()
        {
            var ClassDetails = unitOfWork.ClassRepository.Get();
            return ClassDetails;
        }

        public bool Post(Class _Class)
        {
            bool result = false;
            if (_Class != null)
            {
                unitOfWork.ClassRepository.Insert(_Class);
                unitOfWork.Save();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //update Class
        [HttpPut]
        public bool Put(int CID, Class _Class)
        {
            if (_Class == null)
            {
                return false;
            }
            if (CID != _Class.CID)
            {
                return false;
            }
            try
            {
                unitOfWork.ClassRepository.Update(_Class);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        //delete Class by id
        public Class Delete(int CID)
        {
            Class _Class = unitOfWork.ClassRepository.GetById(CID);
            if (_Class == null)
            {
                return _Class;
            }
            try
            {
                unitOfWork.ClassRepository.Delete(_Class);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return _Class;
            }
            return _Class;
        }

        public string GetClassName(string ClassID)
        {
            string ClassName = string.Empty;
            if (!string.IsNullOrEmpty(ClassID))
            {
                int ID = Convert.ToInt32(ClassID);
                ClassName = unitOfWork.ClassRepository.GetFirstOrDefault(x => x.CID == ID).Class1;
            }
            return ClassName;
        }

        public int GetClassID(string SelectedClass)
        {
            int ClassID = 0;
            ClassID = unitOfWork.ClassRepository.GetFirstOrDefault(x => x.Class1 == SelectedClass).CID;
            return ClassID;
        }
    }
}
