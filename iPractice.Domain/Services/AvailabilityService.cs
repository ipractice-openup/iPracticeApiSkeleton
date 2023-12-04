using iPractice.DataAccess;
using iPractice.Domain.Exceptions;
using iPractice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPractice.Domain.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Availability> CreateAsync(Availability availability)
        {
            var psychologist = await _context.Psychologists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == availability.PsychologistId);
            if (psychologist == null)
            {
                throw new PsychologistNotFoundException(availability.PsychologistId);
            }

            EnsureTimeIsValid(availability.Start);
            EnsureTimeIsValid(availability.End);
            EnsureDurationIsValid(availability.Start, availability.End);

            var result = await _context.AddAsync(new DataAccess.Models.Availability
            {
                Start = availability.Start,
                End = availability.End,
                Psychologist = psychologist
            });

            await _context.SaveChangesAsync();

            availability.Id = result.Entity.Id;

            return availability;
        }

        public async Task<IEnumerable<Availability>> GetAsync(long clientId)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .Include(x => x.Psychologists)
                .FirstOrDefaultAsync(x => x.Id == clientId);
            
            if (client == null)
            {
                throw new ClientNotFoundException(clientId);
            }

            if (client.Psychologists == null || !client.Psychologists.Any())
            {
                return Enumerable.Empty<Availability>();
            }

            var psychologystIds = client.Psychologists.Select(x => x.Id).ToList();
            var availabilities = await _context.Availabilities
                .AsNoTracking()
                .Include(x => x.Psychologist)
                .Where(x => psychologystIds.Any(id => id == x.Psychologist.Id))
                .ToListAsync();

            var result = new List<Availability>();
            foreach (var availability in availabilities)
            {
                var currentStartTime = availability.Start;
                var currentEndTime = availability.Start.AddMinutes(AppConstants.MeetingDurationInMinutes);
                while (availability.End >= currentEndTime)
                {
                    result.Add(new Availability
                    {
                        Start = currentStartTime,
                        End = currentEndTime,
                        PsychologistId = availability.Psychologist.Id
                    });

                    currentStartTime = currentStartTime.AddMinutes(AppConstants.MeetingDurationInMinutes);
                    currentEndTime = currentEndTime.AddMinutes(AppConstants.MeetingDurationInMinutes);
                }
            }

            return result;
        }

        private void EnsureTimeIsValid(DateTime time)
        {
            var message = $"Invalid time: {time}";
            if (time.Second != 0 || time.Millisecond != 0)
            {
                throw new ValidationException(message);
            }

            if (time.Minute != 0 && time.Minute != 15 && time.Minute != 30 && time.Minute != 45)
            {
                throw new ValidationException($"{message}. It should be 0, 15, 30 or 45 minutes");
            }
        }

        private void EnsureDurationIsValid(DateTime start, DateTime end)
        {
            var message = $"End date must be bigger than start date";
            if (start.AddMinutes(AppConstants.MeetingDurationInMinutes) > end)
            {
                throw new ValidationException(message);
            }
        }
    }
}
