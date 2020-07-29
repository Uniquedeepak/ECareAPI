using AutoMapper;
using ECare.BAL.Model;
using ECare.Data;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public class EventHelper : IEventHelper
    {
        readonly EventData data = null;
        public EventHelper(string CS_Name)
        {
            data = new EventData(CS_Name);
        }
        public async Task<List<Model.Event>> GetEvents()
        {
            
            var EventList = data.GetEvents();
            var Event = Mapper.Map<List<Data.Event>, List<Model.Event>>(EventList);
            return Event;
        }

        public async Task<Model.Event> GetEvent(int Id)
        {
            var EventList = data.Get(Id);
            var Event = Mapper.Map<Data.Event, Model.Event>(EventList);
            return Event;
        }
        public async Task<string> InsertEvent(Model.Event _Event)
        {
            string Status = string.Empty;
            var Event = Mapper.Map<Model.Event, Data.Event>(_Event);
            var result = data.Post(Event);
            Status = result ? "Event created successfully." : "Failed to submit Event";
            return Status;
        }
        public async Task<string> UpdateEvent(int Id, Model.Event _Event)
        {
            string Status = string.Empty;
            var Event = Mapper.Map<Model.Event, Data.Event>(_Event);
            var result = data.Put(Id,Event);
            Status = result ? "Event updated successfully." : "Failed to update Event";
            return Status;
        }
        public async Task<string> DeleteEvent(int Id)
        {
            string Status = string.Empty;
            var result = data.Delete(Id);
            Status = result != null ? "Event deleted successfully." : "Failed to delete Event";
            return Status;
        }
    }
}
