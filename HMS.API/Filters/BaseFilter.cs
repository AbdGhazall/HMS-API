using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Exceptions;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.API.Filters
{
    public class BaseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ILog _logger = LogManager.GetLogger(typeof(BaseFilter));
            if (context.Exception is not null)
            {
                _logger.Error($"error occurding, Error message :{context.Exception.Message} ", context.Exception);
                if (context.Exception is UserException)
                {
                    var userException = context.Exception as UserException;
                    var errorMessage = userException.Message;
                    var errorCode = userException.ErrorCode;
                    var httpStatusCode = userException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
                else if (context.Exception is PatientException)
                {
                    var patientException = context.Exception as PatientException;
                    var errorMessage = patientException.Message;
                    var errorCode = patientException.ErrorCode;
                    var httpStatusCode = patientException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
                else if (context.Exception is AppointmentException)
                {
                    var appointmentException = context.Exception as AppointmentException;
                    var errorMessage = appointmentException.Message;
                    var errorCode = appointmentException.ErrorCode;
                    var httpStatusCode = appointmentException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
                else if (context.Exception is MedicalRecordException)
                {
                    var medicalRecordException = context.Exception as MedicalRecordException;
                    var errorMessage = medicalRecordException.Message;
                    var errorCode = medicalRecordException.ErrorCode;
                    var httpStatusCode = medicalRecordException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
                else if (context.Exception is DoctorException)
                {
                    var doctorException = context.Exception as DoctorException;
                    var errorMessage = doctorException.Message;
                    var errorCode = doctorException.ErrorCode;
                    var httpStatusCode = doctorException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
                else if (context.Exception is BillingException)
                {
                    var billingException = context.Exception as BillingException;
                    var errorMessage = billingException.Message;
                    var errorCode = billingException.ErrorCode;
                    var httpStatusCode = billingException.HttpStatusCode;
                    var objectResult = new ObjectResult(new BaseResponseError
                    {
                        ErrorCode = errorCode,
                        ErrorMessage = errorMessage,
                        MessageTime = DateTime.Now,
                    });
                    objectResult.StatusCode = httpStatusCode;
                    context.Result = objectResult;
                    context.ExceptionHandled = true;
                }
            }
        }

        //if you add else here anything you write correctly in the api endpoint will be caught here because that mean there's no exception!

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}