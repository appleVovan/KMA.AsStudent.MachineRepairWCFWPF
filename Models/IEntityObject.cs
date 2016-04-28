using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MacineRepairTool.Models
{
    public interface IEntityObject<TObject> : IEntityObject where TObject : class, IEntityObject<TObject>
    {
        Expression<Func<TObject, bool>> ObjectGuid();
        bool HasAssociation { get; }
        IQueryable<TObject> GetAssociaton(DbSet<TObject> dbSet);
        void DropAssociations();
    }

    public interface IEntityObject
    {
        ObjectType Type { get; }
    }

    public enum ObjectType
    {
        User = 0,
        Client = 1,
        Machine = 2,
        MachineType = 3,
        Order = 4,
        RepairType = 5
    }
}
