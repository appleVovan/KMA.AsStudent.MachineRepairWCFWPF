using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacineRepairTool.DBConnector;
using MacineRepairTool.Models;

namespace Controller
{
    public static class ClientController
    {
        //Клієнт може робити наступні дії:
        //Переглянути список своїх замовлень
        //Проглянути конкретне замовлення зі списку
        //Сплатити замовлення (змінити його статус)
        //Створити нове замовлення
        //Переглянути список своїх станків
        //Додати новий станок
        //Архівувати станок
        //Відредагувати особисті дані
        public static Client CurrentClient { get; private set; }

        public static void Init(Client user)
        {
            CurrentClient = user;
        }

        public static List<Order> GetListOfOrders(int from, int count)
        {
            return EntityWrapper.GetModels<Order>(t => t.ClientGuid == CurrentClient.Guid, from, count);
        }

        public static void PayForOrder(Order order)
        {
            order.Pay();
            EntityWrapper.SaveModel(order);
        }

        public static void CreateNewOrder(Order order)
        {
            EntityWrapper.SaveModel(order);
        }

        public static List<Machine> GetListOfMachinesForOrder()
        {
            return EntityWrapper.GetModels<Machine>(t => t.ClientGuid == CurrentClient.Guid && !t.MachineType.IsDeleted && !t.IsDeleted);
        }

        public static List<RepairType> GetListOfRepairTypesForOrder()
        {
            return EntityWrapper.GetModels<RepairType>(t => !t.IsDeleted);
        }

        public static List<Machine> GetListOfMachines(int from, int count)
        {
            return EntityWrapper.GetModels<Machine>(t => t.ClientGuid == CurrentClient.Guid, from, count);
        }

        public static void CreateNewMachine(Machine machine)
        {
            EntityWrapper.SaveModel(machine);
        }

        public static void ArchiveMachine(Machine machine)
        {
            machine.IsDeleted = true;
            EntityWrapper.SaveModel(machine);
        }

        public static void SaveClient()
        {
            EntityWrapper.SaveModel(CurrentClient);
        }
    }
}
