using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MacineRepairTool.Models
{
    public enum OrderStatus
    {
        Requested,
        Canceled,
        Confirmed,
        Complete,
        Paid
    }
    [Serializable]
    [DataContract(IsReference = true)]
    public class Order : IEntityObject<Order>
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private DateTime _startDate;
        [DataMember]
        private OrderStatus _status;
        [DataMember]
        private Guid _repairTypeGuid;
        [DataMember]
        private RepairType _repairType;
        [DataMember]
        private Guid _machineGuid;
        [DataMember]
        private Machine _machine;
        [DataMember]
        private Guid _clientGuid;
        [DataMember]
        private Client _client;

        private Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        
        public bool HasAssociation
        {
            get { return true; }
        }
        

        public DateTime StartDate
        {
            get { return _startDate; }
            private set { _startDate = value; }
        }

        public OrderStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        internal Guid RepairTypeGuid
        {
            get { return _repairTypeGuid; }
            set { _repairTypeGuid = value; }
        }

        public virtual RepairType RepairType
        {
            get { return _repairType; }
            internal set { _repairType = value; }
        }

        internal Guid MachineGuid
        {
            get { return _machineGuid; }
            set { _machineGuid = value; }
        }

        public virtual Machine Machine
        {
            get { return _machine; }
            private set { _machine = value; }
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

        public Order()
        {
        }

        public Order(RepairType repairType, Machine machine, Client client)
        {
            _guid = Guid.NewGuid();
            _client = client;
            _clientGuid = client.Guid;
            _machine = machine;
            _machineGuid = machine.Guid;
            _repairType = repairType;
            _repairTypeGuid = repairType.Guid;
            _startDate = DateTime.Now;
            _status = OrderStatus.Requested;
        }

        public class OrderConfiguration : EntityTypeConfiguration<Order>
        {
            public OrderConfiguration()
            {
                ToTable("Order");
                HasKey(p => p.Guid);
                Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                Property(p => p.ClientGuid).HasColumnName("ClientGuid").IsRequired();
                Property(p => p.MachineGuid).HasColumnName("MachineGuid").IsRequired();
                Property(p => p.RepairTypeGuid).HasColumnName("RepairTypeGuid").IsRequired();
                Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
                Property(p => p.Status).HasColumnName("Status").IsRequired();
                Ignore(p => p.HasAssociation);
                Ignore(p => p.Type);
            }
        }
        public Expression<Func<Order, bool>> ObjectGuid()
        {
            Expression<Func<Order, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<Order> GetAssociaton(DbSet<Order> dbSet)
        {
            return dbSet.Include(t => t.RepairType).Include(t=>t.Machine).Include(t=>t.Machine.MachineType);
        }

        public void DropAssociations()
        {
            _client = null;
            _machine = null;
            _repairType = null;
        }

        public void Pay()
        {
            Status = OrderStatus.Paid;
        }

        public ObjectType Type => ObjectType.Order;
    }
}
