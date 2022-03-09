using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Constants;
using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Juris.Api.Services;

public class AppointmentRequestService : IAppointmentRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<AppointmentRequest> _repository;
    private readonly UserManager<User> _userManager;

    public AppointmentRequestService(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.AppointmentRequestRepository;
        _userManager = userManager;
    }
    
    public async Task<IList<AppointmentRequest>> GetAllRequests(long userId)
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
    }

    public async Task DeleteRequest(long requestId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Request with id={requestId} not found");
        
        await _repository.Delete(requestId);
        await _unitOfWork.Save();
    }

    public async Task UpdateRequestStatus(string status, long requestId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Request with id={requestId} not found");

        request.Status = status;
        _repository.Update(request);
        await _unitOfWork.Save();
    }
}