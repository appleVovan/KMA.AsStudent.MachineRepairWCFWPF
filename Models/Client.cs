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
    public class Client : User, IEntityObject<Client>
    {
        [DataMember]
        private List<Order> _orders;
        [DataMember]
        private List<Machine> _machines;

        internal virtual List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        internal virtual List<Machine> Machines
        {
            get { return _machines; }
            set { _machines = value; }
        }

        public class ClientConfiguration : EntityTypeConfiguration<Client>
        {
            public ClientConfiguration()
            {
                //ToTable("User");
                //HasKey(p => p.Guid);
                //Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                //Property(p => p.IsEnabled).HasColumnName("Enabled").IsRequired();
                //Property(p => p.Login).HasColumnName("Login").IsRequired();
                //Property(p => p.Name).HasColumnName("Name").IsRequired();
                //Property(p => p.Password).HasColumnName("Password").IsRequired();
                //Property(p => p.UserType).HasColumnName("UserType").IsRequired();
                ////Ignore(p => p.ObjectGuid);
                HasMany(t => t.Orders).WithRequired(t => t.Client).HasForeignKey(t => t.ClientGuid).WillCascadeOnDelete(false);
                HasMany(t => t.Machines).WithRequired(t => t.Client).HasForeignKey(t => t.ClientGuid).WillCascadeOnDelete(false);

            }
        }

        //public new Guid ObjectGuid => Guid;
        public new Expression<Func<Client, bool>> ObjectGuid()
        {
            Expression<Func<Client, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<Client> GetAssociaton(DbSet<Client> dbSet)
        {
            throw new NotImplementedException();
        }

        public Client(string login, string password, string name) : base(login, password, name)
        {
            UserType = UserType.Client;
        }

        public Client()
        {
        }

        public new void DropAssociations()
        {
            _orders = null;
            _machines = null;
        }

        public new ObjectType Type => ObjectType.Client;
    }
}
