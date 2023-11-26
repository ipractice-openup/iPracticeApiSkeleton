using System;
using System.Collections.Generic;
using System.Linq;
using iPractice.Application.Contract.Dtos;

namespace iPractice.DataAccess
{
    public class SeedData
    {
        private const int NoClients = 50;
        private const int NoPsychologists = 20;
        
        private readonly ApplicationDbContext _context;
        
        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var psychologists = CreatePsychologists();
            _context.Psychologists.AddRange(psychologists);
            
            var clients = CreateClients(psychologists);
            _context.Clients.AddRange(clients);
            
            _context.SaveChanges();
        }

        private static List<ClientDto> CreateClients(List<PsychologistDto> psychologists)
        {
            var random = new Random();
            
            List<ClientDto> clients = new List<ClientDto>();
            for (int i = 0; i < NoClients; i++)
            {
                clients.Add(new ClientDto()
                {
                    Name = $"Client {i + 1}",
                    Psychologists = new List<PsychologistDto>(new[]
                    {
                        psychologists.Skip(random.Next(NoPsychologists)).First(),
                        psychologists.Skip(random.Next(NoPsychologists)).First()
                    })
                });
            }

            return clients;
        }

        private static List<PsychologistDto> CreatePsychologists()
        {
            List<PsychologistDto> psychologists = new List<PsychologistDto>();
            for (int i = 0; i < NoPsychologists; i++)
            {
                psychologists.Add(new PsychologistDto
                {
                    Name = $"Psychologist {i + 1}"
                });
            }

            return psychologists;
        }
    }
}