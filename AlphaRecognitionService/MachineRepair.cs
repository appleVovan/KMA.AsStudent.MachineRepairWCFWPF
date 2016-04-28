using System;
using System.Collections.Generic;
using MachineRepair.EntityFramework;
using MacineRepairTool.Models;

namespace MachineRepair
{
    public class MachineRepair : IMachineRepairService
    {
        public void SaveModel(IEntityObject model)
        {
            switch (model.Type)
            {
                case ObjectType.User:
                    EntityWrapper.SaveObject(model as User);
                    break;
                case ObjectType.Client:
                    EntityWrapper.SaveObject(model as Client);
                    break;
                case ObjectType.Machine:
                    EntityWrapper.SaveObject(model as Machine);
                    break;
                case ObjectType.MachineType:
                    EntityWrapper.SaveObject(model as MachineType);
                    break;
                case ObjectType.Order:
                    EntityWrapper.SaveObject(model as Order);
                    break;
                case ObjectType.RepairType:
                    EntityWrapper.SaveObject(model as RepairType);
                    break;
                default:
                    break;
            }
        }

        public IEntityObject GetModels(IEntityObject model)
        {
            switch (model.Type)
            {
                case ObjectType.User:
                    return EntityWrapper.GetModels(model as User);
                case ObjectType.Client:
                    return EntityWrapper.GetModels(model as Client);
                case ObjectType.Machine:
                    return EntityWrapper.GetModels(model as Machine);
                case ObjectType.MachineType:
                    return EntityWrapper.GetModels(model as MachineType);
                case ObjectType.Order:
                    return EntityWrapper.GetModels(model as Order);
                case ObjectType.RepairType:
                    return EntityWrapper.GetModels(model as RepairType);
                default:
                    return null;
            }
        }

        public IEntityObject GetModelsWithQuery(IEntityObject model, EntitySelectQuery selectQuery)
        {
            switch (model.Type)
            {
                case ObjectType.User:
                    return EntityWrapper.GetModels(model as User, selectQuery);
                case ObjectType.Client:
                    return EntityWrapper.GetModels(model as Client, selectQuery);
                case ObjectType.Machine:
                    return EntityWrapper.GetModels(model as Machine, selectQuery);
                case ObjectType.MachineType:
                    return EntityWrapper.GetModels(model as MachineType, selectQuery);
                case ObjectType.Order:
                    return EntityWrapper.GetModels(model as Order, selectQuery);
                case ObjectType.RepairType:
                    return EntityWrapper.GetModels(model as RepairType, selectQuery);
                default:
                    return null;
            }
        }

        public IEntityObject GetModel(IEntityObject model, EntitySelectQuery selectQuery)
        {
            switch (model.Type)
            {
                case ObjectType.User:
                    return EntityWrapper.GetModel(model as User, selectQuery);
                case ObjectType.Client:
                    return EntityWrapper.GetModel(model as Client, selectQuery);
                case ObjectType.Machine:
                    return EntityWrapper.GetModel(model as Machine, selectQuery);
                case ObjectType.MachineType:
                    return EntityWrapper.GetModel(model as MachineType, selectQuery);
                case ObjectType.Order:
                    return EntityWrapper.GetModel(model as Order, selectQuery);
                case ObjectType.RepairType:
                    return EntityWrapper.GetModel(model as RepairType, selectQuery);
                default:
                    return null;
            }
        }
    }
}
