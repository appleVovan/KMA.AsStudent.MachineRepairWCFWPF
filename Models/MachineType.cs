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
    public class MachineType:IEntityObject<MachineType>
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _name;
        [DataMember]
        private string _tradeMark;
        [DataMember]
        private string _country;
        [DataMember]
        private bool _isDeleted;
        [DataMember]
        private List<Machine> _machines;
        

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

        public string TradeMark
        {
            get { return _tradeMark; }
            private set { _tradeMark = value; }
        }

        public string Country
        {
            get { return _country; }
            private set { _country = value; }
        }

        internal virtual List<Machine> Machines
        {
            get { return _machines; }
            set { _machines = value; }
        }

        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set { _isDeleted = value; }
        }

        public MachineType(string name, string tradeMark, string country)
        {
            _guid = Guid.NewGuid();
            _name = name;
            _tradeMark = tradeMark;
            _country = country;
            _isDeleted = false;
        }

        public MachineType()
        {
            
        }

        public class MachineTypeConfiguration : EntityTypeConfiguration<MachineType>
        {
            public MachineTypeConfiguration()
            {
                ToTable("MachineType");
                HasKey(p => p.Guid);
                Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                Property(p => p.Country).HasColumnName("Country").IsRequired();
                Property(p => p.Name).HasColumnName("Name").IsRequired();
                Property(p => p.TradeMark).HasColumnName("TradeMark").IsRequired();
                Property(p => p.IsDeleted).HasColumnName("IsDeleted").IsRequired();
                HasMany(t => t.Machines).WithRequired(t => t.MachineType).HasForeignKey(t => t.MachineTypeGuid).WillCascadeOnDelete(false);
                Ignore(p => p.HasAssociation);
                Ignore(p => p.Type);
            }
        }

        public Expression<Func<MachineType, bool>> ObjectGuid()
        {
            Expression<Func<MachineType, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<MachineType> GetAssociaton(DbSet<MachineType> dbSet)
        {
            throw new NotImplementedException();
        }

        public void DropAssociations()
        {
            _machines = null;
        }

        public ObjectType Type { get {return ObjectType.MachineType;} }

        public override string ToString()
        {
            return Name + "; " + TradeMark + "; " + Country;
        }
    }
}
