using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace MachineRepair
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class EventModel
    {
        [DataMember]
        private Guid _eventGuid;

        [DataMember]
        private Guid _dateGuid;

        [DataMember]
        private string _name;

        [DataMember]
        private string _text;

        public Guid EventGuid
        {
            get { return _eventGuid; }
            private set
            {
                _eventGuid = value;
            }
        }

        public Guid DateGuid
        {
            get { return _dateGuid; }
            internal set { _dateGuid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        [DataMember]
        public virtual DateModel DateModel { get; set; }
        public EventModel()
        {
        }

        public EventModel(Guid dateGuid, string name, string text)
        {
            _eventGuid = Guid.NewGuid();
            _dateGuid = dateGuid;
            _name = name;
            _text = text;
        }

        public void Print(int i)
        {
            Console.WriteLine(i + ". " + _name + "   " + _text);
        }


        public class EventModelConfiguration : EntityTypeConfiguration<EventModel>
        {
            public EventModelConfiguration()
            {
                ToTable("Event");
                HasKey(p => p.EventGuid);
                Property(p => p.EventGuid).HasColumnName("Guid").IsRequired();
                Property(p => p.DateGuid).HasColumnName("DateGuid").IsOptional();
                Property(p => p.Name).HasColumnName("Name").IsRequired();
                Property(p => p.Text).HasColumnName("Text").IsRequired();
            }
        }
    }
}
