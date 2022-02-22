using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.ViewModels;
using WMDAApi.Utils;
using System.Linq;
using FluentValidation;

namespace WMDAApi.Services
{
    public class PatientService : ServiceBase, IPatientService
    {
        private readonly IValidator<Patient> _validator;
        public PatientService(IAppDbContext context, IValidator<Patient> validator) : base(context)
        {
            _validator = validator;
        }
        public async Task<ResponseModel> CreatePatientAsync(Patient patientModel)
        {
            ResponseModel model = new ResponseModel();
            
            var validated = _validator.Validate(patientModel);
            if (!validated.IsValid)
            {
                var validationErrors = validated.Errors.Select(t => new ErrorData("", t.ErrorMessage)).ToList();
                throw new CustomException(validationErrors);
            }
            
            ((AppDbContext)_dbContext).Add<Patient>(patientModel);
            model.Messsage = "Patient Inserted Successfully.";

            await ((AppDbContext)_dbContext).SaveChangesAsync();
            model.IsSuccess = true;
            
            return model;
        }
    }
}
