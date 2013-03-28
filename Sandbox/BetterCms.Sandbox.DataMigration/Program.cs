﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using BetterCms.Configuration;
using BetterCms.Core.DataAccess.DataContext.Migrations;
using BetterCms.Core.Environment.Assemblies;
using BetterCms.Core.Modules;

using BetterCms.Module.Blog;
using BetterCms.Module.MediaManager;
using BetterCms.Module.Pages;
using BetterCms.Module.Root;
using BetterCms.Module.Templates;
using BetterCms.Module.Users;

using Common.Logging;

namespace BetterCms.Sandbox.DataMigration
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static List<ModuleDescriptor> descriptors;

        static Program()
        {
             ICmsConfiguration configuration = new CmsConfigurationSection();
             descriptors = 
                    (new ModuleDescriptor[]
                    {
                        new BlogModuleDescriptor(configuration),
                        new TemplatesModuleDescriptor(configuration),
                        new MediaManagerModuleDescriptor(configuration),
                        new PagesModuleDescriptor(configuration),
                        new RootModuleDescriptor(configuration),
                        new UsersModuleDescriptor(configuration)
                    })
                    .ToList();
        }

        private static void Migrate(bool up)
        {            
            DefaultMigrationRunner runner = new DefaultMigrationRunner(new DefaultAssemblyLoader(), new CmsConfigurationSection());
            runner.Migrate(descriptors, up);
        }

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0 || args[0] != "auto")
                {
                    Console.WriteLine("-- PRESS ANY KEY TO START DATABASE MIGRATIONS --");
                    Console.ReadKey();
                }

                Console.WriteLine("-- Migrate DOWN --");
                
                 Migrate(false);

                Console.WriteLine("-- Migrate  UP --");

                Migrate(true);
                
                Console.WriteLine("-- DONE --");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Console.ReadKey();
            }
        }
    }
}
