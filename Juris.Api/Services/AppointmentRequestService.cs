using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Api.IServices;
using Juris.Common.Dtos.AppointmentRequest;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Domain.Identity;
using Juris.Resource;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Api.Services;

public class AppointmentRequestService : IAppointmentRequestService
{
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<AppointmentRequest> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AppointmentRequestService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMailService mailService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.AppointmentRequestRepository;
        _userManager = userManager;
        _mailService = mailService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentRequestDto>> GetAllRequests(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, userId));

        var requests = await _repository.GetAll(a => a.UserId == userId);
        return _mapper.Map<IEnumerable<AppointmentRequestDto>>(requests);
    }

    public async Task<AppointmentRequestDto> CreateRequest(CreateAppointmentRequestDto requestDto, long userId)
    {
        var request = _mapper.Map<AppointmentRequest>(requestDto);
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, userId));

        request.UserId = userId;
        await _repository.Insert(request);
        await _unitOfWork.Save();
        try
        {
            await _mailService.SendAsync(
                user.Email,
                GlobalResource.NewRequestSubject,
                string.Format(GlobalResource.NewRequestEmail, request.FirstName, request.LastName));
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw new HttpResponseException(HttpStatusCode.FailedDependency,
                GlobalResource.CantSendEmail);
        }

        return _mapper.Map<AppointmentRequestDto>(request);
    }

    public async Task DeleteRequest(long requestId, long userId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.RequestNotFound, requestId));

        if (request.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedRequestChange, requestId));

        await _repository.Delete(requestId);
        await _unitOfWork.Save();
    }

    public async Task UpdateRequestStatus(string status, long requestId, long userId)
    {
        var request = await _repository.GetById(requestId);
        if (request == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.RequestNotFound, requestId));

        if (request.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedRequestChange, requestId));

        request.Status = status;
        _repository.Update(request);
        await _unitOfWork.Save();
    }
}