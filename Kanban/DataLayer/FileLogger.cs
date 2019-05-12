using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Kanban.BL
{
    class FileLogger
    {
        public static void write(Dictionary<string, User> dictionary)
        {
            string path = Directory.GetCurrentDirectory() + "\\UserData.txt";

            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                string json = JsonConvert.SerializeObject(dictionary);
                File.WriteAllText(path, json);
            }
            else
            {
                WriteErrorToLog("error in Write function");
            }


        }

        //this function reads from a file
        public static Dictionary<string, User> Read()
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;//to stop duplicate the linked list
                string path = Directory.GetCurrentDirectory() + "\\UserData.txt";
                Dictionary<string, User> usersDictionary =
                             JsonConvert.DeserializeObject<Dictionary<string, User>>
                                                               (File.ReadAllText(path), settings);
                return usersDictionary;
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.ToString() + " in Read function");
            }

            return null;


        }

        public static void WriteNullObjectExceptionToLogger<T>(string positionOfTheError)
            where T : class
        {
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.RollingFile("LogFileError.txt", shared: true,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}{NewLine}") //writing the error to the same file
                     .CreateLogger();

            Type t = typeof(T);
            if (t == typeof(User))
            {
                Log.Error("the User is NULL in {0}!", positionOfTheError);
            }
            else if (t == typeof(Task))
            {
                Log.Error("the Task is NULL in {0}!", positionOfTheError);
            }
            else if (t == typeof(Column))
            {
                Log.Error("the Column is NULL in {0}!", positionOfTheError);
            }
            else if (t == typeof(Board))
            {

                Log.Error("the Board is NULL in {0}!", positionOfTheError);
            }
            else
            {
                Log.Error("the String is NULL in {0}!", positionOfTheError);
            }
            Log.CloseAndFlush();
        }

        public static void WriteInformationToLog(string info)
        {
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.RollingFile("LogFileError.txt", shared: true,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}{NewLine}") //writing the error to the same file
                     .CreateLogger();

            Log.Information(info);
            Log.CloseAndFlush();
        }

        public static void WriteErrorToLog(string info)
        {
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.RollingFile("LogFileError.txt", shared: true,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}{NewLine}") //writing the error to the same file
                     .CreateLogger();

            Log.Error(info);
            Log.CloseAndFlush();
        }
        /*public static void Main()
        {
            /*User newUser = new User("iris@g", "123", true);
            Dictionary<string, User> dictionary = new Dictionary<string, User>();
            dictionary.Add("iris@g", newUser);
            write(dictionary);
            string path = Directory.GetCurrentDirectory() + "\\kanbanBoardData.txt";
            foreach (KeyValuePair<string, Column> kvp in Read<Column>(path))
            {
                
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            
            Console.ReadKey();
        }}*/
    }
}