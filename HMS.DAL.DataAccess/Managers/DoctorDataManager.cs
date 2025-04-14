using HMS.API.Abstraction.Entities.Doctor;
using HMS.API.Abstraction.Interfaces.Doctor;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class DoctorDataManager : IDoctorDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public DoctorDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(DoctorDataManager));
        }

        public async Task<List<DoctorEntity>> GetAllDoctors()
        {
            _logger.Info("GetAllDoctors called in DataManager");
            var doctors = await _applicationDbContext.Doctors
                .AsNoTracking()
                .Include(d => d.User)
                .Select(d => new DoctorEntity
                {
                    Id = d.Id,
                    SpecialtyId = d.SpecialtyId,
                    Phone = d.Phone,
                    UserId = d.UserId,
                    FullName = d.FullName,
                    Email = d.User.Email
                }).ToListAsync();
            _logger.Info("GetAllDoctors returned in DataManager");
            return doctors;
        }

        public async Task<DoctorEntity> GetDoctorById(int id)
        {
            _logger.Info($"GetDoctorById called in DataManager [Id={id}]");
            var doctor = await _applicationDbContext.Doctors
                .AsNoTracking()
                .Include(d => d.User)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (doctor != null)

            {
                var doctorEntity = new DoctorEntity()
                {
                    Id = doctor.Id,
                    SpecialtyId = doctor.SpecialtyId,
                    Phone = doctor.Phone,
                    UserId = doctor.UserId,
                    FullName = doctor.FullName,
                    Email = doctor.User.Email
                };
                _logger.Info("GetDoctorById returned in DataManager");
                return doctorEntity;
            }
            return null;
        }

        public async Task CreateDoctor(DoctorRequest doctor)
        {
            _logger.Info($"CreateDoctor called in DataManager [Id={doctor.FullName}]");
            var specialty = await _applicationDbContext.Specialty
                .AsNoTracking()
                .Where(r => r.DoctorSpecialty == doctor.Specialty)
                .FirstOrDefaultAsync();

            if (specialty != null)
            {
                _applicationDbContext.Doctors.Add(new Doctor()
                {
                    FullName = doctor.FullName,
                    Specialty = specialty,
                    Phone = doctor.Phone,
                    UserId = doctor.UserId,
                });
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("CreateDoctor returned in DataManager");
            }
        }

        public async Task UpdateDoctor(int id, DoctorUpdate updatedDoctor)
        {
            _logger.Info($"UpdateDoctor called in DataManager [Id={id}]");
            var doctor = await _applicationDbContext.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.FullName = updatedDoctor.FullName;
                doctor.Phone = updatedDoctor.Phone;
                var specialty = await _applicationDbContext.Specialty
                    .AsNoTracking()
                    .Where(r => r.DoctorSpecialty == updatedDoctor.Specialty)
                    .FirstOrDefaultAsync();
                if (specialty != null)
                {
                    doctor.Specialty = specialty;
                }

                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("UpdateDoctor returned in DataManager");
            }
        }

        public async Task DeleteDoctor(int id)
        {
            _logger.Info($"DeleteDoctor called in DataManager [Id={id}]");
            var doctor = await _applicationDbContext.Doctors.FindAsync(id);
            _applicationDbContext.Doctors.Remove(doctor);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("DeleteDoctor returned in DataManager");
        }

        public async Task<DoctorEntity> GetDoctorByPhone(string phone)
        {
            _logger.Info($"GetDoctorByPhone called in DataManager [Phone={phone}]");
            var doctor = await _applicationDbContext.Doctors
                .Include(x => x.User)
                .Where(x => x.Phone == phone)
                .FirstOrDefaultAsync();
            if (doctor != null)
            {
                var doctorEntity = new DoctorEntity()
                {
                    Id = doctor.Id,
                    SpecialtyId = doctor.SpecialtyId,
                    Phone = doctor.Phone,
                    UserId = doctor.UserId,
                    FullName = doctor.FullName,
                    Email = doctor.User.Email
                };
                _logger.Info("GetDoctorByPhone returned in DataManager");
                return doctorEntity;
            }
            return null;
        }

        public async Task<string> GetDoctorSpecialty(string doctorSpecialty)
        {
            _logger.Info($"GetDoctorSpecialty called in DataManager [Specialty={doctorSpecialty}]");
            var Specialty = await _applicationDbContext.Specialty
                        .Where(r => r.DoctorSpecialty == doctorSpecialty)
                        .Select(r => r.DoctorSpecialty)
                        .FirstOrDefaultAsync();
            _logger.Info("GetDoctorSpecialty returned in DataManager");
            return Specialty;
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}