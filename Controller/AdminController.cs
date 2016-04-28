using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacineRepairTool.DBConnector;
using MacineRepairTool.Models;

namespace Controller
{
    public static class AdminController
    {
        //Адмін може робити наступні дії:
        //Переглянути список типів ремонтів
        //Додати новий тип ремонту
        //Зробити тип ремонту архівним
        //Переглянути список типів машин
        //Додати новий тип машини
        //Зробити тип машини архівним
        //Переглянути список клієнтів
        //Проглянути конкретного клієнта
        //Створити нового клієнта
        //Архівувати клієнта
        //Переглянути список користувачів
        //Проглянути конкретного користувача
        //Створити нового користувача
        //Архівувати користувача
        //Переглянути замовлення конкретного користувача
        //Переглянути всі замовлення
        //Переглянути конкретне замовлення
        //Змінити статус замовлення
        //Відмінити замовлення

        public static User CurrentUser { get; private set; }

        public static void Init(User user)
        {
            CurrentUser = user;
        }

        public static List<RepairType> GetListOfRepairTypes(int from, int count)
        {
            return EntityWrapper.GetModels<RepairType>(from, count);
        }

        public static void AddNewRepairType(RepairType repairType)
        {
            EntityWrapper.SaveModel(repairType);
        }

        public static void ArchiveRepairType(RepairType repairType)
        {
            repairType.IsDeleted = true;
            EntityWrapper.SaveModel(repairType);
        }

        public static List<MachineType> GetListOfMachineTypes(int from, int count)
        {
            return EntityWrapper.GetModels<MachineType>(from, count);
        }

        public static void AddNewMachineType(MachineType machineType)
        {
            EntityWrapper.SaveModel(machineType);
        }

        public static void ArchiveMachineType(MachineType machineType)
        {
            machineType.IsDeleted = true;
            EntityWrapper.SaveModel(machineType);
        }

        public static List<Client> GetListOfClients(int from, int count)
        {
            return EntityWrapper.GetModels<Client>(from, count);
        }

        public static void AddNewClient(Client client)
        {
            EntityWrapper.SaveModel(client);
        }

        public static void ArchiveClient(Client client)
        {
            client.IsDeleted = true;
            EntityWrapper.SaveModel(client);
        }

        public static List<User> GetListOfUsers(int from, int count)
        {
            return EntityWrapper.GetModels<User>(t=>t.UserType==UserType.Partner, from, count);
        }

        public static void AddNewUser(User user)
        {
            EntityWrapper.SaveModel(user);
        }

        public static void ArchiveUser(User user)
        {
            user.IsDeleted = true;
            EntityWrapper.SaveModel(user);
        }

        public static List<Order> GetListOfOrders(int from, int count)
        {
            return EntityWrapper.GetModels<Order>(from, count);
        }

        public static List<Order> GetListOfOrders(Client client, int from, int count)
        {
            return EntityWrapper.GetModels<Order>(t => t.ClientGuid == client.Guid, from, count);
        }

        public static void ChangeOrderStatus(Order order)
        {
            switch (order.Status)
            {
                case OrderStatus.Requested:
                    order.Status = OrderStatus.Confirmed;
                    break;
                case OrderStatus.Confirmed:
                    order.Status = OrderStatus.Complete;
                    order.Machine.TimesRepaired++;
                    EntityWrapper.SaveModel(order.Machine);
                    break;
                case OrderStatus.Complete:
                    order.Status = OrderStatus.Paid;
                    break;
                default:
                    break;
            }
            EntityWrapper.SaveModel(order);
        }

        public static void CancellOrder(Order order)
        {
            order.Status=OrderStatus.Canceled;
            EntityWrapper.SaveModel(order);
        }
    }
}
