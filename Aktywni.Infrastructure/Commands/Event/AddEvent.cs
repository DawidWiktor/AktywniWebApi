using System;

namespace Aktywni.Infrastructure.Commands.Event
{
    public class AddEvent
    {
        public string Name {get; set;}
        public int ObjectFID {get;set;}
        public DateTime Date {get;set;}
        public int WhoCreatedID {get;set;} 
        public string Description {get;set;}
    }
}