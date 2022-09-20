using System;
using System.Threading.Tasks;
using Microsoft.OData.SampleService.Models.TripPin;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ListPeople().Wait();
            //Consulta1().Wait();
            //Consulta2().Wait();
            Consulta3().Wait();
        }

        static async Task ListPeople()
        {
            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var people = context.People.Expand(_ => _.Trips).Execute();
            var gentes = from gente in people select gente;
            foreach (var _person in gentes)
            {
                var viajes = _person.Trips;
                Console.WriteLine("{0} {1}", _person.FirstName, _person.LastName);
                foreach (var viaje in viajes)
                {
                    Console.WriteLine($"{viaje.Name} con la descripción de {viaje.Description}");
                }
            }
            Console.ReadLine();

        }

        //Sacar todos los viajeros con todos sus datos. ordenados por nombre.
        static async Task Consulta1()
        {
            Console.WriteLine("CONSULTA 1");
            Console.WriteLine("----------\n");
            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));

            var personas = context.People.Expand(persona => persona.Trips).Execute();
            personas = personas.OrderBy(p => p.FirstName, StringComparer.OrdinalIgnoreCase);
            foreach (var _person in personas)
            {
                if (_person.Trips.Count != 0)
                {
                    Console.WriteLine("Viajero: " + _person.FirstName + " " + _person.LastName);
                    Console.Write("\tEMAILS: ");
                    foreach (var email in _person.Emails)
                    {
                        Console.Write(email + ", ");
                    }
                    Console.WriteLine();
                    if (_person.AddressInfo.Count != 0)
                    {
                        Console.Write("\tDIRECCION: ");
                        Console.Write(_person.AddressInfo[0].Address + " in " + _person.AddressInfo[0].City.Name);
                        Console.WriteLine();
                    }
                    Console.Write("\tGENERO: ");
                    Console.Write(_person.Gender.Value);
                    Console.WriteLine();


                }
                //Console.WriteLine("{0} {1}", _person.FirstName, _person.LastName);
                //foreach (var viaje in viajes)
                //{
                //    Console.WriteLine($"{viaje.Name} con la descripción de {viaje.Description}");
                //}
            }
        }
        //Sacar los 2 ultimos viajes de un viajero en concreto
        static async Task Consulta2()
        {
            Console.WriteLine("CONSULTA 2");
            Console.WriteLine("----------\n");

            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));

            var gente = context.People.Expand(_ => _.Trips).Execute().ToList();
            if (gente.Count() == 0)
            {
                Console.WriteLine("No hay pasajeros");
            }
            Random rnd = new Random();
            int randIndex = rnd.Next(gente.Count());
            var persona = gente.ElementAt(randIndex);
            var viajes = persona.Trips.OrderBy(v => v.StartsAt);
            if (viajes.Count() >= 2)
            {
                foreach (var viaj in viajes.Take(2))
                {
                    Console.WriteLine(viaj.StartsAt.ToString() + ", " + viaj.Name.ToString());
                    Console.WriteLine(viaj.Description);
                    Console.WriteLine();
                }
            }
            else if (viajes.Count() == 1)
            {
                Console.WriteLine(viajes.FirstOrDefault().StartsAt.ToString() + ", " + viajes.FirstOrDefault().Name.ToString());
                Console.WriteLine(viajes.FirstOrDefault().Description);
            }
            else
            {
                Console.WriteLine("No hay viajes de este pasajero");
            }
            Console.WriteLine();
        }
        //Sacar las lineas aereas y casa linea aerea en los aeropuertos en los que trabaja o a tenido vuelos.
        static async Task Consulta3()
        {
            Console.WriteLine("CONSULTA 3");
            Console.WriteLine("----------\n");

            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));

            /*var vuelos = context.People.Expand(x => x.Trips).Execute();
            foreach (var vuelo in vuelos)
            {
                var viaje = vuelo.Trips;
                var v = viaje.ElementAt(0);
                var plant = v.PlanItems.ElementAt(0);
                var f = plant.
            }*/

            var fotos = context.Photos.Execute();
            var foto = fotos.ElementAt(0);
            foto.
            
        }

        //Sacar todos los aeropuertos y todas las lineas que operan desde el.
        //Sacar todos los aeropuertos, las lineas con las que trabaja y los vuelos que ha tenido.
        //Determinar para cada aeropuerto cuantos vuelos han salido

    }
}
