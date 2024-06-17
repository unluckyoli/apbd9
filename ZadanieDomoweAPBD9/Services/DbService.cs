using Microsoft.EntityFrameworkCore;
using ZadanieDomoweAPBD9.Data;
using ZadanieDomoweAPBD9.DTOs;
using ZadanieDomoweAPBD9.Models;

namespace ZadanieDomoweAPBD9.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;

    public DbService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> AddPrescription(AddPrescriptionRequest request)
    {
        var doctor = await _context.Doctors.FindAsync(request.DoctorId);
        if (doctor == null) return false;

        var patient = await _context.Patients.FindAsync(request.PatientId);
        if (patient == null) return false;

        if (request.Medicaments.Count > 10) return false;
        foreach (var med in request.Medicaments)
        {
            if (!await _context.Medicaments.AnyAsync(m => m.IdMedicament == med.IdMedicament))
                return false;
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = request.DoctorId,
            IdPatient = request.PatientId,
            PrescriptionsMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PatientDetailsDto> GetPatientDetails(int patientId)
    {
        var patient = await _context.Patients
            .Where(p => p.IdPatient == patientId)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionsMedicaments)
                .ThenInclude(pm => pm.Medicament)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (patient == null) return null;

        var detailsDto = new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(pr => new PrescriptionDetailsDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new DoctorDto
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName,
                    LastName = pr.Doctor.LastName,
                    Email = pr.Doctor.Email
                },
                Medicaments = pr.PrescriptionsMedicaments.Select(pm => new MedicamentDto
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type
                }).ToList()
            }).ToList()
        };

        return detailsDto;
    }
}
