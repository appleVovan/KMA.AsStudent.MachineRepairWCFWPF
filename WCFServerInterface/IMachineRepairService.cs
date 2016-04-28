using System.Collections.Generic;
using System.ServiceModel;
using MacineRepairTool.Models;

namespace MachineRepair
{

    [ServiceContract]
    public interface IMachineRepairService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Machine))]
        [ServiceKnownType(typeof(MachineType))]
        [ServiceKnownType(typeof(Order))]
        [ServiceKnownType(typeof(RepairType))]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Filter))]
        void SaveModel(IEntityObject model);
        [OperationContract]
        [ServiceKnownType(typeof(EList<Client>))]
        [ServiceKnownType(typeof(EList<Machine>))]
        [ServiceKnownType(typeof(EList<MachineType>))]
        [ServiceKnownType(typeof(EList<Order>))]
        [ServiceKnownType(typeof(EList<RepairType>))]
        [ServiceKnownType(typeof(EList<User>))]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Machine))]
        [ServiceKnownType(typeof(MachineType))]
        [ServiceKnownType(typeof(Order))]
        [ServiceKnownType(typeof(RepairType))]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Filter))]
        IEntityObject GetModels(IEntityObject model);
        [OperationContract]
        [ServiceKnownType(typeof(EList<Client>))]
        [ServiceKnownType(typeof(EList<Machine>))]
        [ServiceKnownType(typeof(EList<MachineType>))]
        [ServiceKnownType(typeof(EList<Order>))]
        [ServiceKnownType(typeof(EList<RepairType>))]
        [ServiceKnownType(typeof(EList<User>))]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Machine))]
        [ServiceKnownType(typeof(MachineType))]
        [ServiceKnownType(typeof(Order))]
        [ServiceKnownType(typeof(RepairType))]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Filter))]
        IEntityObject GetModelsWithQuery(IEntityObject model, EntitySelectQuery selectQuery);
        [OperationContract]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Machine))]
        [ServiceKnownType(typeof(MachineType))]
        [ServiceKnownType(typeof(Order))]
        [ServiceKnownType(typeof(RepairType))]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Filter))]
        IEntityObject GetModel(IEntityObject model, EntitySelectQuery selectQuery);
    }    
}

