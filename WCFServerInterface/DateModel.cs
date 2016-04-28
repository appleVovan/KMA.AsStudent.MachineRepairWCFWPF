using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace MachineRepair
{
    [Serializable]
    [DataContract(IsReference = true)]
    public sealed class DateModel
    {
        [DataMember]
        private Guid _dateGuid;
        [DataMember]
        private DateTime _date;

        public Guid DateGuid
        {
            get { return _dateGuid; }
            private set
            {
                _dateGuid = value;
                if (EventModels != null)
                {
                    foreach (var doc in EventModels)
                    {
                        doc.DateGuid = _dateGuid;
                    }
                }
            }
        }

        public DateTime Date
        {
            get { return _date; }
            private set { _date = value; }
        }

        [DataMember]
        public List<EventModel> EventModels { get; set; }

        public DateModel()
        {
            EventModels = new List<EventModel>();
        }

        public DateModel(DateTime date)
        {
            _dateGuid = Guid.NewGuid();
            _date = date;
            EventModels = new List<EventModel>();
        }

        public void Print(int i)
        {
            Console.WriteLine(i + ". " + Date.ToString("D"));
        }

        public void PrintDepth()
        {
            Console.WriteLine(Date.ToString("D"));
            for (int i = 0; i < EventModels.Count; i++)
            {
                EventModels[i].Print(i);
            }
        }


        public class DateModelConfiguration : EntityTypeConfiguration<DateModel>
        {
            public DateModelConfiguration()
            {
                ToTable("DateModel");
                HasKey(p => p.DateGuid);
                Property(p => p.DateGuid).HasColumnName("DateGuid").IsRequired();
                Property(p => p.Date).HasColumnName("Date").IsRequired();
                HasMany(t => t.EventModels).WithOptional(t => t.DateModel).HasForeignKey(t => t.DateGuid).WillCascadeOnDelete(true);
            }
        }
        
    }
}
