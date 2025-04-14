using HMS.API.Abstraction.Entities.Appointment;
using HMS.API.Abstraction.Interfaces.Appointment;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class AppointmentDataManager : IAppointmentDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public AppointmentDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(AppointmentDataManager));
        }

        public async Task<List<AppointmentEntity>> GetAllApointments()
        {
            _logger.Info("GetAllApointments called in DataManager");
            var appointment = await _applicationDbContext.Appointments
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    DoctorId = a.Doctor.Id,
                    PatientId = a.Patient.Id,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatusId = a.AppointmentStatusId,
                    Notes = a.Notes,
                }).ToListAsync();
            _logger.Info("GetAllApointments returned in DataManager");
            return appointment;
        }

        public async Task<AppointmentEntity> GetAppointmentById(int id)
        {
            _logger.Info($"GetAppointmentById called in DataManager [Id={id}]");
            var appointment = await _applicationDbContext.Appointments
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (appointment != null)
            {
                var appointmentEntity = new AppointmentEntity()
                {
                    Id = appointment.Id,
                    DoctorId = appointment.Doctor.Id,
                    PatientId = appointment.Patient.Id,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentStatusId = appointment.AppointmentStatusId,
                    Notes = appointment.Notes,
                };
                _logger.Info("GetAppointmentById returned in DataManager");
                return appointmentEntity;
            }
            return null;
        }

        public async Task CreateAppointment(AppointmentRequest appointment)
        {
            _logger.Info($"CreateAppointment called in DataManager [DoctorId={appointment.DoctorId}, PatientId={appointment.PatientId}]");
            var appointmentStatus = await _applicationDbContext.AppointmentStatuses
                .Where(r => r.Status == appointment.AppointmentStatus)
                .FirstOrDefaultAsync();

            if (appointmentStatus != null)
            {
                _applicationDbContext.Appointments.Add(new Appointment()
                {
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    Notes = appointment.Notes,
                    AppointmentStatus = appointmentStatus
                });
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("CreateAppointment returned in DataManager");
            }
        }

        public async Task UpdateAppointment(int id, AppointmentUpdate appointmentUpdate)
        {
            _logger.Info($"UpdateAppointment called in DataManager [Id={id}]");
            var appointment = await _applicationDbContext.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.AppointmentDate = appointmentUpdate.AppointmentDate;
                appointment.Notes = appointmentUpdate.Notes;

                var appointmentStatus = await _applicationDbContext.AppointmentStatuses
                    .Where(r => r.Status == appointmentUpdate.AppointmentStatus)
                    .FirstOrDefaultAsync();
                if (appointmentStatus != null)
                {
                    appointment.AppointmentStatus = appointmentStatus;
                }
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("UpdateAppointment returned in DataManager");
            }
        }

        public async Task DeleteAppointment(int id)
        {
            _logger.Info($"DeleteAppointment called in DataManager [Id={id}]");
            var appointment = await _applicationDbContext.Appointments.FindAsync(id);
            _applicationDbContext.Appointments.Remove(appointment);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("DeleteAppointment returned in DataManager");
        }

        public async Task<bool> IsAppointmentAvailable(int doctorId, int patientId, DateTime appointmentDate)
        {
            _logger.Info($"IsAppointmentAvailable called in DataManager [DoctorId={doctorId}, PatientId={patientId}, AppointmentDate={appointmentDate}]");
            var isAppointmentAvailable = !await _applicationDbContext.Appointments
                .Where(a => a.DoctorId == doctorId &&
                               a.PatientId == patientId &&
                               a.AppointmentDate.Date == appointmentDate.Date)
                .AnyAsync();
            _logger.Info("IsAppointmentAvailable returned in DataManager");
            return isAppointmentAvailable;
        }

        public async Task<string> GetAppointmentStatus(string appointmentStatus)
        {
            _logger.Info($"GetAppointmentStatus called in DataManager [Status={appointmentStatus}]");
            var status = await _applicationDbContext.AppointmentStatuses
                        .Where(r => r.Status == appointmentStatus)
                        .Select(r => r.Status)
                        .FirstOrDefaultAsync();

            _logger.Info("GetAppointmentStatus returned in DataManager");
            return status;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientName(string patientName)
        {
            _logger.Info($"GetAppointmentsByPatientName called in DataManager [PatientName={patientName}]");
            var appointments = await _applicationDbContext.Appointments
                .Where(a => a.Patient.FullName == patientName)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    Notes = a.Notes,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatusId = a.AppointmentStatusId,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId
                })
                .ToListAsync();
            _logger.Info("GetAppointmentsByPatientName returned in DataManager");
            return appointments;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientNameAndDate(string patientName, DateTime date)
        {
            _logger.Info($"GetAppointmentsByPatientNameAndDate called in DataManager [PatientName={patientName}, Date={date}]");
            var appointments = await _applicationDbContext.Appointments
                .Where(a => a.Patient.FullName == patientName && a.AppointmentDate.Date == date.Date)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    Notes = a.Notes,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatusId = a.AppointmentStatusId,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId
                })
                .ToListAsync();
            _logger.Info("GetAppointmentsByPatientNameAndDate returned in DataManager");
            return appointments;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorName(string doctorName)
        {
            _logger.Info($"GetAppointmentsByDoctorName called in DataManager [DoctorName={doctorName}]");
            var appointments = await _applicationDbContext.Appointments
                .Where(a => a.Doctor.FullName == doctorName)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    Notes = a.Notes,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatusId = a.AppointmentStatusId,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId
                })
                .ToListAsync();
            _logger.Info("GetAppointmentsByDoctorName returned in DataManager");
            return appointments;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorNameAndDate(string doctorName, DateTime date)
        {
            _logger.Info($"GetAppointmentsByDoctorNameAndDate called in DataManager [DoctorName={doctorName}, Date={date}]");
            var appointments = await _applicationDbContext.Appointments
                .Where(a => a.Doctor.FullName == doctorName && a.AppointmentDate.Date == date.Date)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    Notes = a.Notes,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatusId = a.AppointmentStatusId,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId
                })
                .ToListAsync();
            _logger.Info("GetAppointmentsByDoctorNameAndDate returned in DataManager");
            return appointments;
        }

        public async Task<bool> IsPatientNameAndDateExist(string patientName, DateTime date)
        {
            _logger.Info($"IsPatientNameAndDateExist called in DataManager [PatientName={patientName}, Date={date}]");
            var nameAndDate = await _applicationDbContext.Appointments
                .Where(a => a.Patient.FullName == patientName && a.AppointmentDate.Date == date.Date)
                .AnyAsync();
            _logger.Info("IsPatientNameAndDateExist returned in DataManager");
            return nameAndDate;
        }

        public async Task<bool> IsDoctorNameAndDateExist(string doctorName, DateTime date)
        {
            _logger.Info($"IsDoctorNameAndDateExist called in DataManager [DoctorName={doctorName}, Date={date}]");
            var DoctorAndDate = await _applicationDbContext.Appointments
                .Where(a => a.Doctor.FullName == doctorName && a.AppointmentDate.Date == date.Date)
                .AnyAsync();
            _logger.Info("IsDoctorNameAndDateExist returned in DataManager");
            return DoctorAndDate;
        }

        public async Task<bool> IsPatientNameExist(string patientName)
        {
            _logger.Info($"IsPatientNameExist called in DataManager [PatientName={patientName}]");
            var patientNameExist = await _applicationDbContext.Patients
                .Where(p => p.FullName == patientName)
                .AnyAsync();
            _logger.Info("IsPatientNameExist returned in DataManager");
            return patientNameExist;
        }

        public async Task<bool> IsDoctorNameExist(string doctorName)
        {
            _logger.Info($"IsDoctorNameExist called in DataManager [DoctorName={doctorName}]");
            var doctorNameExist = await _applicationDbContext.Doctors
                .Where(p => p.FullName == doctorName)
                .AnyAsync();
            _logger.Info("IsDoctorNameExist returned in DataManager");
            return doctorNameExist;
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}