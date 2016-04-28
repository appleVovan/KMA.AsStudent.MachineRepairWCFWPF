using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MacineRepairTool.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Machine:IEntityObject<Machine>
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _serialNumber;
        [DataMember]
        private int _year;
        [DataMember]
        private int _timesRepaired;
        [DataMember]
        private bool _isDeleted;
        [DataMember]
        private Guid _machineTypeGuid;
        [DataMember]
        private MachineType _machineType;
        [DataMember]
        private List<Order> _orders;
        [DataMember]
        private Guid _clientGuid;
        [DataMember]
        private Client _client;

        internal Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        

        public bool HasAssociation
        {
            get { return true; }
        }
        


        public string SerialNumber
        {
            get { return _serialNumber; }
            private set { _serialNumber = value; }
        }

        public int Year
        {
            get { return _year; }
            private set { _year = value; }
        }

        public int TimesRepaired
        {
            get { return _timesRepaired; }
            set { _timesRepaired = value; }
        }

        public Guid MachineTypeGuid
        {
            get { return _machineTypeGuid; }
            private set { _machineTypeGuid = value; }
        }

        public virtual MachineType MachineType
        {
            get { return _machineType; }
            internal set { _machineType = value; }
        }

        internal virtual List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public Guid ClientGuid
        {
            get { return _clientGuid; }
            private set { _clientGuid = value; }
        }

        internal virtual Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public Machine()
        {
            
        }

        public Machine(string serialNumber, int year, MachineType machineType, Client client)
        {
            _guid = Guid.NewGuid();
            _client = client;
            _clientGuid = client.Guid;
            _machineType = machineType;
            _machineTypeGuid = machineType.Guid;
            _serialNumber = serialNumber;
            _timesRepaired = 0;
            _year = year;
            _isDeleted = false;
        }

        public class MachineConfiguration : EntityTypeConfiguration<Machine>
        {
            public MachineConfiguration()
            {
                ToTable("Machine");
                HasKey(p => p.Guid);
                Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                Property(p => p.ClientGuid).HasColumnName("ClientGuid").IsRequired();
                Property(p => p.MachineTypeGuid).HasColumnName("MachineTypeGuid").IsRequired();
                Property(p => p.SerialNumber).HasColumnName("SerialNumber").IsRequired();
                Property(p => p.TimesRepaired).HasColumnName("TimesRepaired").IsRequired();
                Property(p => p.Year).HasColumnName("Year").IsRequired();
                Property(p => p.IsDeleted).HasColumnName("IsDeleted").IsRequired();
                HasMany(t => t.Orders).WithRequired(t => t.Machine).HasForeignKey(t => t.MachineGuid).WillCascadeOnDelete(false);
                Ignore(p => p.HasAssociation);
                Ignore(p => p.Type);

            }
        }

        public Expression<Func<Machine, bool>> ObjectGuid()
        {
            Expression<Func<Machine, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<Machine> GetAssociaton(DbSet<Machine> dbSet)
        {
            return dbSet.Include(t => t.MachineType);
        }

        public void DropAssociations()
        {
            _orders = null;
            _client = null;
            _machineType = null;
        }

        public ObjectType Type => ObjectType.Machine;

        public override string ToString()
        {
            return SerialNumber + "; " + Year + "; " + MachineType.ToString();
        }
    }
}
