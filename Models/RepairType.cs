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
    public class RepairType : IEntityObject<RepairType>
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _name;
        [DataMember]
        private decimal _price;
        [DataMember]
        private int _duration;
        [DataMember]
        private bool _isDeleted;
        [DataMember]
        private List<Order> _orders;
        
        internal Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        

        public bool HasAssociation
        {
            get { return false;} 
        }

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public Decimal Price
        {
            get { return _price; }
            private set { _price = value; }
        }

        public int Duration
        {
            get { return _duration; }
            private set { _duration = value; }
        }

        internal virtual List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }

            set
            {
                _isDeleted = value;
            }
        }

        public RepairType()
        {
            
        }

        public RepairType(string name, decimal price, int duration)
        {
            _guid = Guid.NewGuid();
            _name = name;
            _price = price;
            _duration = duration;
            _isDeleted = false;
        }

        public class RepairTypeConfiguration : EntityTypeConfiguration<RepairType>
        {
            public RepairTypeConfiguration()
            {
                ToTable("RepairType");
                HasKey(p => p.Guid);
                Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                Property(p => p.Duration).HasColumnName("Duration").IsRequired();
                Property(p => p.Name).HasColumnName("Name").IsRequired();
                Property(p => p.Price).HasColumnName("Price").IsRequired();
                Property(p => p.IsDeleted).HasColumnName("IsDeleted").IsRequired();
                HasMany(t => t.Orders).WithRequired(t => t.RepairType).HasForeignKey(t => t.RepairTypeGuid).WillCascadeOnDelete(false);
                Ignore(p => p.HasAssociation);
                Ignore(p => p.Type);
            }
        }

        public Expression<Func<RepairType, bool>> ObjectGuid()
        {
            Expression<Func<RepairType, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<RepairType> GetAssociaton(DbSet<RepairType> dbSet)
        {
            throw new NotImplementedException();
        }

        public void DropAssociations()
        {
            _orders = null;
        }

        public ObjectType Type => ObjectType.RepairType;

        public override string ToString()
        {
            return Name + "; " + Duration + "; " + Type;
        }
    }
}
