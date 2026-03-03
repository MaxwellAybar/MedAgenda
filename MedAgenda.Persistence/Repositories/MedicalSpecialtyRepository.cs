using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;

namespace MedAgenda.Persistence.Repositories
{
    public class MedicalSpecialtyRepository
        : BaseRepository<MedicalSpecialty>, IMedicalSpecialtyRepository
    {
        public MedicalSpecialtyRepository(MedAgendaContext context)
            : base(context)
        {
        }
    }
}