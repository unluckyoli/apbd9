using ZadanieDomoweAPBD9.DTOs;

namespace ZadanieDomoweAPBD9.Services;

public interface IDbService
{
        Task<bool> AddPrescription(AddPrescriptionRequest request);
        Task<PatientDetailsDto> GetPatientDetails(int patientId);
    

}
