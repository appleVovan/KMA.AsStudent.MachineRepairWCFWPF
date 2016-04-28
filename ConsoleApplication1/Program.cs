using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using MachineRepair;

namespace RecordBook
{
    class Program
    {
        //private static List<DateModel> dates;
        //private static DateModel currentDateModel;
        //private static EventModel currentEvent;
        static void Main(string[] args)
        {
            //ShowMenu();
            //ChannelFactory<IRecordBookService> factory = null;
            //EndpointAddress address = new EndpointAddress("http://localhost:8014/RecordBookService");
            //factory = new ChannelFactory<IRecordBookService>("tester", address);

            //IRecordBookService alphaserverProxy = factory.CreateChannel();
            //var a = alphaserverProxy.GetDateModels();
            //var b = new DateModel(DateTime.Today);
            //b.EventModels.Add(new EventModel(b.DateGuid, "testEvent1", "Need todo my test job"));
            //alphaserverProxy.AddDateModel(b);
            //b.EventModels.Add(new EventModel(b.DateGuid, "testEvent2", "Need todo my test job again"));
            //alphaserverProxy.SaveDateModel(b);
            //alphaserverProxy.DeleteEventModel(b.EventModels[1]);
            //alphaserverProxy.DeleteDateModel(a[0]);
            //Console.ReadKey();
            
        }

        public static ChannelFactory<IMachineRepairService> GetChanel()
        {
            ChannelFactory<IMachineRepairService> factory = null;
            EndpointAddress address = new EndpointAddress("http://localhost:8014/RecordBookService");
            factory = new ChannelFactory<IMachineRepairService>("tester", address);
            return factory;
        }
        

        //public static void ShowMenu()
        //{
        //    using (var channel = GetChanel())
        //    {
        //        var interf = channel.CreateChannel();
        //        dates = interf.GetDateModels();
        //        Console.WriteLine();
        //        for (int i = 0; i < dates.Count; i++)
        //        {
        //            dates[i].Print(i);
        //        }
        //        Console.WriteLine("To select a Date type 's', To delete a Date type 'd', To add a Date type 'a', To return to main menu type 'm'");
        //        var answer = Console.ReadKey().KeyChar;
                
        //        switch (answer)
        //        {
        //            case 's':
        //                SelectDate();
        //                break;
        //            case 'd':
        //                DeleteDate();
        //                break;
        //            case 'a':
        //                AddDate();
        //                break;
        //            default:
        //                ShowMenu();
        //                break;
        //        }

        //    }
        //}

        //public static void SelectDate()
        //{
        //    Console.WriteLine();
        //    for (int i = 0; i < dates.Count; i++)
        //    {
        //        dates[i].Print(i);
        //    }
        //    Console.WriteLine("Type the number, To return type -1");
        //    try
        //    {
        //        var number = Convert.ToInt32(Console.ReadLine());
        //        if (number == -1)
        //        {
        //            ShowMenu();
        //            return;
        //        }
        //        currentDateModel = dates[number];
        //        ShowDate();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        SelectDate();
        //    }
            
        //}

        //public static void ShowDate()
        //{
        //    currentDateModel.PrintDepth();
        //    Console.WriteLine("To Select Event Type s, To Delete event type d, To Add event type a, To return to mainMenu type m");
        //    var answer = Console.ReadKey().KeyChar;
        //    switch (answer)
        //    {
        //        case 's':
        //            ShowEvent();
        //            break;
        //        case 'd':
        //            DeleteEvent();
        //            break;
        //        case 'a':
        //            AddEvent();
        //            break;
        //        default:
        //            ShowMenu();
        //            break;
        //    }
        //}

        //public static void AddDate()
        //{
        //    try
        //    {
        //        Console.WriteLine("Type the Date in Format 'dd.MM.yyyy'");
        //        var date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new DateTimeFormatInfo());
        //        if (!dates.Exists(t => t.Date.Date == date.Date))
        //        {
        //            var dateModel = new DateModel(date);
        //            using (var channel = GetChanel())
        //            {
        //                var interf = channel.CreateChannel();
        //                interf.AddDateModel(dateModel);
        //                dates = interf.GetDateModels();
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Such Date already exists");
        //        }
        //        ShowMenu();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        AddDate();
        //    }
            


        //}

        //public static void AddEvent()
        //{
        //    try
        //    {
        //        Console.WriteLine("Type the event name");
        //        var name = Console.ReadLine();
        //        Console.WriteLine("Type the event text");
        //        var text = Console.ReadLine();
        //        if (!currentDateModel.EventModels.Exists(t => t.Name == name))
        //        {
        //            var dateModel = new EventModel(currentDateModel.DateGuid, name, text);
        //            currentDateModel.EventModels.Add(dateModel);
        //            using (var channel = GetChanel())
        //            {
        //                var interf = channel.CreateChannel();
        //                interf.SaveDateModel(currentDateModel);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Such Event already exists");
        //        }
        //        ShowDate();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        AddEvent();
        //    }



        //}

        //public static void DeleteDate()
        //{
        //    Console.WriteLine();
        //    for (int i = 0; i < dates.Count; i++)
        //    {
        //        dates[i].Print(i);
        //    }
        //    Console.WriteLine("Type the number, To return type -1");
        //    try
        //    {
        //        var number = Convert.ToInt32(Console.ReadLine());
        //        if (number == -1)
        //        {
        //            ShowMenu();
        //            return;
        //        }
        //        currentDateModel = dates[number];
        //        using (var channel = GetChanel())
        //        {
        //            var interf = channel.CreateChannel();
        //            interf.DeleteDateModel(currentDateModel);
        //            dates = interf.GetDateModels();
        //        }
        //        ShowMenu();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        DeleteDate();
        //    }
        //}

        //public static void SelectEvent()
        //{
        //    currentDateModel.PrintDepth();
        //    Console.WriteLine("Type the number, To return type -1");
        //    try
        //    {
        //        var number = Convert.ToInt32(Console.ReadLine());
        //        if (number == -1)
        //        {
        //            ShowDate();
        //            return;
        //        }
        //        currentEvent = currentDateModel.EventModels[number];
        //        ShowEvent();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        SelectEvent();
        //    }

        //}

        //public static void DeleteEvent()
        //{
        //    currentDateModel.PrintDepth();
        //    Console.WriteLine("Type the number, To return type -1");
        //    try
        //    {
        //        var number = Convert.ToInt32(Console.ReadLine());
        //        if (number == -1)
        //        {
        //            ShowDate();
        //            return;
        //        }
        //        using (var channel = GetChanel())
        //        {
        //            var interf = channel.CreateChannel();
        //            interf.DeleteEventModel(currentDateModel.EventModels[number]);
        //        }
        //        currentDateModel.EventModels.Remove(currentDateModel.EventModels[number]);
        //        ShowDate();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        DeleteEvent();
        //    }

        //}

        //public static void ShowEvent()
        //{
        //    currentEvent.Print(1);
        //    Console.WriteLine("To Select change name n, To change text t, To date d, To return to mainMenu type m");
        //    var answer = Console.ReadKey().KeyChar;
        //    switch (answer)
        //    {
        //        case 'n':
        //            Console.WriteLine("Type new name.");
        //            var text = Console.ReadLine();
        //            using (var channel = GetChanel())
        //            {
        //                currentEvent.Name = text;
        //                var interf = channel.CreateChannel();
        //                interf.SaveDateModel(currentDateModel);
        //            }
        //            ShowEvent();
        //            break;
        //        case 't':
        //            Console.WriteLine("Type new text.");
        //            var text1 = Console.ReadLine();
        //            using (var channel = GetChanel())
        //            {
        //                currentEvent.Text = text1;
        //                var interf = channel.CreateChannel();
        //                interf.SaveDateModel(currentDateModel);
        //            }
        //            ShowEvent();
        //            break;
        //        case 'd':
        //            ShowDate();
        //            break;
        //        default:
        //            ShowMenu();
        //            break;
        //    }
        //}
    }
}
