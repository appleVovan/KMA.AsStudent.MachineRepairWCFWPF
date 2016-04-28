using System.ServiceModel;
using MacineRepairTool.Models;

namespace MachineRepair
{
    public class DatabaseHelper
    {
        public static ChannelFactory<IMachineRepairService> GetChanel()
        {
            ChannelFactory<IMachineRepairService> factory;
            EndpointAddress address = new EndpointAddress("http://localhost:8016/MachineRepairService");
            factory = new ChannelFactory<IMachineRepairService>("tester", address);
            return factory;
        }

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

        public static EList<TObject> GetModels<TObject>()
            where TObject : class, IEntityObject, IEntityObject<TObject>, new()
        {
            using (ChannelFactory<IMachineRepairService> alphaserverFactory = GetChanel())
            {
                IMachineRepairService alphaserverProxy = alphaserverFactory.CreateChannel();
                return alphaserverProxy.GetModels(new TObject()) as EList<TObject>;
            }
        }

        public static EList<TObject> GetModels<TObject>(EntitySelectQuery selectQuery)
            where TObject : class, IEntityObject, IEntityObject<TObject>, new()
        {
            using (ChannelFactory<IMachineRepairService> alphaserverFactory = GetChanel())
            {
                IMachineRepairService alphaserverProxy = alphaserverFactory.CreateChannel();
                var obj = new TObject();
                return alphaserverProxy.GetModelsWithQuery(obj, selectQuery) as EList<TObject>;
            }
        }

        public static TObject GetModel<TObject>(EntitySelectQuery selectQuery)
            where TObject : class, IEntityObject, IEntityObject<TObject>, new()
        {
            using (ChannelFactory<IMachineRepairService> alphaserverFactory = GetChanel())
            {
                IMachineRepairService alphaserverProxy = alphaserverFactory.CreateChannel();
                return alphaserverProxy.GetModel(new TObject(), selectQuery) as TObject;
            }
        }

        public static void SaveModel<TObject>(TObject obj)
            where TObject : class, IEntityObject, IEntityObject<TObject>, new()
        {
            using (ChannelFactory<IMachineRepairService> alphaserverFactory = GetChanel())
            {
                IMachineRepairService alphaserverProxy = alphaserverFactory.CreateChannel();
                alphaserverProxy.SaveModel(obj);
            }
        }
    }
}
