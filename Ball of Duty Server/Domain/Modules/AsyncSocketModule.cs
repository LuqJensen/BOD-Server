﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AsyncSocket;

namespace Ball_of_Duty_Server.Domain.Modules
{
    public class AsyncSocketModule : MarshalByRefObject
    {
        private AppDomain _appDomain;
        private const string ASSEMBLY_NAME = "AsyncSocketImpl";
        private const string FULLY_QUALIFIED_TYPE_NAME = ASSEMBLY_NAME + ".AsyncSocketFactory";
        private static string _appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string _dllPath = Path.Combine(_appDomainPath, $@"ComponentImpl\{ASSEMBLY_NAME}.dll");

        public IAsyncSocketFactory AsyncSocketFactory { get; private set; }

        public AsyncSocketModule()
        {
            Reload();
        }

        /// <summary>
        /// The loaded Assembly will be unloaded together with the AppDomain, when a new Assembly is loaded.
        /// Requires all used types to be Serializable.
        /// </summary>
        public void Reload()
        {
            // Requires all types to be Serializable, System.Net.Sockets.Socket is not!

            if (_appDomain != null)
            {
                AppDomain.Unload(_appDomain);
            }

            byte[] bytes = File.ReadAllBytes(_dllPath);

            _appDomain = AppDomain.CreateDomain(ASSEMBLY_NAME);
            AsmLoader asm = new AsmLoader()
            {
                Data = bytes
            };
            _appDomain.DoCallBack(asm.LoadAsm);

            AsyncSocketFactory = (IAsyncSocketFactory)asm.Assembly.CreateInstance(FULLY_QUALIFIED_TYPE_NAME);
        }

        private class AsmLoader : MarshalByRefObject
        {
            public byte[] Data { private get; set; }
            public Assembly Assembly { get; private set; }

            public void LoadAsm()
            {
                Assembly = Assembly.Load(Data);
            }
        }
    }
}