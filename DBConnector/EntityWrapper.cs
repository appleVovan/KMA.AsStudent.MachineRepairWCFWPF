using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MacineRepairTool.Models;

namespace MacineRepairTool.DBConnector
{
    public static class EntityWrapper
    {
        public static void CreateDefaultDb()
        {
            var user = new User("vovan", "123", "Volodymyr Yablonskyi");
            SaveModel(user);
            var client = new Client("sashokDu", "123", "Sasha Dushkin");
            SaveModel(client);
            user = new User("shulga", "123", "Shulgan Galyna");
            SaveModel(user);
            client = new Client("dagorat", "123", "Dagor Dagorat");
            SaveModel(client);
            var clients = GetModels<Client>();
            var machineType = new MachineType("bread cutter", "Breadis Inc.", "USA");
            SaveModel(machineType);
            machineType = new MachineType("meat cutter", "Meatis Inc.", "USA");
            SaveModel(machineType);
            machineType = new MachineType("Griller 2000", "Grillis Inc.", "China");
            SaveModel(machineType);
            var machineTypes = GetModels<MachineType>();
            var repairType = new RepairType("Cleaning", new decimal(20.00), 6);
            SaveModel(repairType);
            repairType = new RepairType("Engine fix", new decimal(200.00), 24);
            SaveModel(repairType);
            repairType = new RepairType("Cooler fix", new decimal(200.00), 48);
            SaveModel(repairType);
            var repairTypes = GetModels<RepairType>();
            var machine = new Machine("11200394", 2006, machineTypes[0], clients[0]);
            SaveModel(machine);
            machine = new Machine("11200694", 2004, machineTypes[1], clients[0]);
            SaveModel(machine);
            machine = new Machine("16700694", 2014, machineTypes[2], clients[1]);
            SaveModel(machine);
            var machines = GetModels<Machine>();
            var order = new Order(repairTypes[0], machines[0], clients[0]);
            SaveModel(order);
            order = new Order(repairTypes[1], machines[1], clients[0]);
            SaveModel(order);
            order = new Order(repairTypes[2], machines[2], clients[1]);
            SaveModel(order);
        }

        public static void SaveModel<TObject>(TObject model/*, bool saveAssociations=true*/) where TObject : class, IEntityObject<TObject>
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    model.DropAssociations();
                    EntityState state = EntityState.Unchanged;
                    state = context.Set<TObject>().Any(model.ObjectGuid()) ? EntityState.Modified : EntityState.Added;
                    context.Entry(model).State = state;
                    /*if (saveAssociations)
                    {
                        
                    }*/
                    context.SaveChanges();
                    context.Entry(model).Reload();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Save Model " + typeof(TObject), ex);
                }
            }
        }

        public static void DeleteModel<TObject>(TObject model/*, bool saveAssociations=true*/) where TObject : class, IEntityObject<TObject>
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    model.DropAssociations();
                    context.Entry(model).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Save Model " + typeof(TObject), ex);
                }
            }
        }

        public static List<TObject> GetModels<TObject>() where TObject : class, IEntityObject<TObject>, new()
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    var obj = new TObject();
                    return (obj.HasAssociation?obj.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Models " + typeof(TObject), ex);
                }
            }
        }
        public static List<TObject> GetModels<TObject>(int from, int count) where TObject : class, IEntityObject<TObject>, new()
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    var obj = new TObject();
                    return (obj.HasAssociation ? obj.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).Skip(from).Take(count).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Models " + typeof(TObject), ex);
                }
            }
        }

        public static List<TObject> GetModels<TObject>(Expression<Func<TObject, bool>> linq) where TObject : class, IEntityObject<TObject>, new()
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    var obj = new TObject();
                    return (obj.HasAssociation ? obj.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).Where(linq).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Models " + typeof(TObject), ex);
                }
            }
        }
        public static List<TObject> GetModels<TObject>(Expression<Func<TObject, bool>> linq, int from, int count) where TObject : class, IEntityObject<TObject>, new()
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    var obj = new TObject();
                    return (obj.HasAssociation ? obj.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).Where(linq).Skip(from).Take(count).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Models " + typeof(TObject), ex);
                }
            }
        }

        public static TObject GetModel<TObject>(Expression<Func<TObject, bool>> linq) where TObject : class, IEntityObject<TObject>, new()
        {
            using (var context = new ModelsDbContext())
            {
                try
                {
                    var obj = new TObject();
                    return (obj.HasAssociation ? obj.GetAssociaton(context.Set<TObject>()) : context.Set<TObject>()).FirstOrDefault(linq);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed To Get Model " + typeof(TObject), ex);
                }
            }
        }
        
    }
}
