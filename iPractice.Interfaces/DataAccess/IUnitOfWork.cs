using iPractice.Interfaces.Repositories;

namespace iPractice.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        IAvailabilityRepository Availability { get; }
        IPsychologistRepository Psychologist { get; }
        ITimeSlotRepository TimeSlot { get; }

        void Commit();
    }
}
