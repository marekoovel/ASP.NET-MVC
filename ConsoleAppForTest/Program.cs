using DAL;
using DAL.Helpers;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;


namespace ConsoleAppForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDbContext>().To<DatabaseContext>();
            kernel.Bind<EFRepositoryFactories>().To<EFRepositoryFactories>();
            kernel.Bind<IEFRepositoryProvider>().To<EFRepositoryProvider>();
            kernel.Bind<IUOW>().To<UOW>();


            //using (var resultRepo = kernel.Get<UOW>())
            //{
            //    var genre = new Genre{
            //        GenreId = 3,
            //        Name = "Electro",
            //        Description = "Dance music with beat",
            //    };

            //    resultRepo.Genres.Add(genre);
            //    resultRepo.Commit();

            //}


            //using (var resultRepo = kernel.Get<UOW>())
            //{
            //    var results = resultRepo.Genres.All;

            //    foreach (var result in results)
            //    {
            //        Console.WriteLine("ID" + result.);
            //        Console.WriteLine("ID" + result.Name);
            //        Console.WriteLine("ID" + result.Description);
            //    }


            //}

            Console.WriteLine("================ DONE ==============");


        //     using (var resultRepo = kernel.Get<IGBT880_DAL.Repositories.ResultsRepository>())
        //     {
        //         var results = resultRepo.All.Take(10).ToList();

        //         foreach (var result in results)
        //         {
        //             Console.WriteLine("Bar code" + result.IGBT_barcode);
        //         }


        //     }

        //     Console.WriteLine("================ DONE ==============");
        //     Console.ReadLine();
        }
        
    }
}
