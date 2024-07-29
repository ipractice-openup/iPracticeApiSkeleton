using iPractice.DataAccess;
using iPractice.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointment = iPractice.Domain.Models.Appointment;
using Availability = iPractice.Domain.Models.Availability;

namespace iPractice.Domain.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            var client = await _context.Clients
                .Include(x => x.Psychologists)
                .FirstOrDefaultAsync(x => x.Id == appointment.ClientId);
            if (client == null)
            {
                throw new ClientNotFoundException(appointment.ClientId);
            }

            var psychologist = await _context.Psychologists
                .FirstOrDefaultAsync(x => x.Id == appointment.PsychologistId);
            if (psychologist == null)
            {
                throw new PsychologistNotFoundException(appointment.PsychologistId);
            }

            EnsureTimeIsValid(appointment.Start);
            EnsureTimeIsValid(appointment.End);
            EnsureDurationIsValid(appointment.Start, appointment.End);
            EnsurePsychologistIsConnectedToClient(psychologist.Id, client);
            await EnsureTimeIsAvailable(appointment.Start, appointment.End, appointment.PsychologistId);

            var result = await _context.AddAsync(new DataAccess.Models.Appointment
            {
                Start = appointment.Start,
                End = appointment.End,
                Client = client,
                Psychologist = psychologist,
            });

            await _context.SaveChangesAsync();

            appointment.Id = result.Entity.Id;

            return appointment;
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
            var message = $"End date must be bigger than start date, and meeting duration should be {AppConstants.MeetingDurationInMinutes} minutes";
            if (start.AddMinutes(AppConstants.MeetingDurationInMinutes) != end)
            {
                throw new ValidationException(message);
            }
        }

        private async Task EnsureTimeIsAvailable(DateTime start, DateTime end, long psychologistId)
        {
            var message = $"Psychologist {psychologistId} is not available from {start} to {end}";
            var timeIsAvailable = await _context.Availabilities.AnyAsync(x => x.Start <= start && x.End >= end && x.Psychologist.Id == psychologistId);

            if (!timeIsAvailable)
            {
                throw new ValidationException(message);
            }
        }

        private void EnsurePsychologistIsConnectedToClient(long psychologistId, DataAccess.Models.Client client)
        {
            var message = $"Psychologist {psychologistId} is not connected to the client {client.Id}";

            if (client.Psychologists == null
                || !client.Psychologists.Any(x => x.Id == psychologistId))
            {
                throw new ValidationException(message);
            }
        }
    }
}
