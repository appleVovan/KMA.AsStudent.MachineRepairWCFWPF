using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MacineRepairTool.Models;

namespace MachineRepair.EntityFramework
{
    internal static class EntityWrapper
    {
        public static EList<TObject> GetModels<TObject>(TObject workingObject, EntitySelectQuery selectQuery)
            where TObject : class, IEntityObject, IEntityObject<TObject>
        {

            using (var context = new ModelsDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    var query = EntitySelectQuery.GetQueryable(workingObject, selectQuery, context);
                    var eList = new EList<TObject>();
                    eList.AddRange(query.ToList());
                    return eList;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Object From Db", ex);
                }
            }
        }

        public static EList<TObject> GetModels<TObject>(TObject workingObject) where TObject : class, IEntityObject, IEntityObject<TObject>
        {
            using (var context = new ModelsDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    var eList = new EList<TObject>();
                    List<TObject> list = (workingObject.HasAssociation ? workingObject.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).ToList();
                    eList.AddRange(list);
                    return eList;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Object From Db", ex);
                }
            }
        }

        public static TObject GetModel<TObject>(TObject workingObject, EntitySelectQuery selectQuery)
            where TObject : class, IEntityObject, IEntityObject<TObject>
        {

            using (var context = new ModelsDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    return EntitySelectQuery.GetQueryable(workingObject, selectQuery, context).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Object From Db", ex);
                }
            }
        }

        public static void SaveObject<TObject>(TObject workingObject) where TObject : class, IEntityObject, IEntityObject<TObject>
        {
            try
            {
                using (var context = new ModelsDbContext())
                {
                    workingObject.DropAssociations();
                    EntityState state = EntityState.Unchanged;
                    state = context.Set<TObject>().Any(workingObject.ObjectGuid()) ? EntityState.Modified : EntityState.Added;
                    context.Entry(workingObject).State = state;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed To Save Object To Db", ex);
            }
        }
        
    }
}
