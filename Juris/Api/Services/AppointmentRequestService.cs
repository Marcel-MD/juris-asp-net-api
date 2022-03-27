using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Api.Services;

public class AppointmentRequestService : IAppointmentRequestService
{
    private readonly IMailService _mailService;
    private readonly IGenericRepository<AppointmentRequest> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AppointmentRequestService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.AppointmentRequestRepository;
        _userManager = userManager;
        _mailService = mailService;
    }

    public async Task<IEnumerable<AppointmentRequest>> GetAllRequests(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        return await _repository.GetAll(a => a.UserId == userId);
    }

    public async Task CreateRequest(AppointmentRequest request, long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        request.UserId = userId;
        await _repository.Insert(request);
        await _unitOfWork.Save();
        try
        {
            await _mailService.SendAsync(
                user.Email,
                "New Appointment Request",
                $"<strong>{request.FirstName} {request.LastName}</strong> wants to make an appointment with you. Check the details of the request on <strong>Juris</strong>.");
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw new HttpResponseException(HttpStatusCode.FailedDependency,
                "Could not send notification email to user");
        }
    }

    public async Task DeleteRequest(long requestId, long userId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Request with id={requestId} not found");

        if (request.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to delete request id={requestId}");

        await _repository.Delete(requestId);
        await _unitOfWork.Save();
    }

    public async Task UpdateRequestStatus(string status, long requestId, long userId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Request with id={requestId} not found");

        if (request.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to modify request id={requestId}");

        request.Status = status;
        _repository.Update(request);
        await _unitOfWork.Save();
    }
}