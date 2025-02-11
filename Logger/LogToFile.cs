﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Logger
{
    public class LogToFile
    {
        private readonly string _path;

        public LogToFile(string path)
        {
            _path = path;
        }

        private async Task WriteToFile(string message)
        {
            try
            {
                await using var file = new StreamWriter(_path, true);
                await file.WriteLineAsync(message);
            }
            //TODO Дописать перехват исключений https://docs.microsoft.com/ru-ru/dotnet/api/system.io.streamwriter.-ctor?view=net-5.0#System_IO_StreamWriter__ctor_System_String_System_Boolean_
            catch (ObjectDisposedException)
            {
                throw new Exception("Удалено средство записи потока");
            }
            catch (InvalidOperationException)
            {
                throw new Exception(
                    "Средство записи потока в настоящее время используется предыдущей операцией записи");
            }
        }

        public async Task LogInfo(string message)
        {
            await WriteToFile($"{DateTime.Now:u} [INFO] {message}");
        }
        
        public async Task LogError(string message)
        {
            await WriteToFile($"{DateTime.Now:u} [ERROR] {message}");
        }
        
        public async Task LogWarning(string message)
        {
            await WriteToFile($"{DateTime.Now:u} [WARNING] {message}");
        }
        
        public async Task LogSuccess(string message)
        {
            await WriteToFile($"{DateTime.Now:u} [SUCCESS] {message}");
        }
        
        public async Task LogCustom(string type, string message)
        {
            await WriteToFile($"{DateTime.Now:u} [{type}] {message}");
        }

        public async Task Log(LogType type, string message)
        {
            await WriteToFile($"{DateTime.Now:u} [{type.ToString()}] {message}");
        }
    }
}