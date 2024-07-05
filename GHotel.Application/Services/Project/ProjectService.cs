using GHotel.Application.Exceptions;
using GHotel.Application.Models;
using GHotel.Application.Repositories;
using GHotel.Application.Utilities;
using GHotel.Domain.Entities;
using GHotel.Domain.Enums;
using Mapster;

namespace GHotel.Application.Services.Project;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IImageUtility _imageUtility;
    private readonly IUserRepository _userRepository;
    private readonly ILockUtility _lockUtility;

    public ProjectService(IProjectRepository projectRepository, IImageUtility imageUtility, IUserRepository userRepository, ILockUtility lockUtility)
    {
        _projectRepository = projectRepository;
        _imageUtility = imageUtility;
        _userRepository = userRepository;
        _lockUtility = lockUtility;
    }

    public async Task<ProjectResponseModel> Create(ProjectRequestModel request, CancellationToken cancellationToken)
    {
        var exists = await _projectRepository.Exists(request.Name,cancellationToken).ConfigureAwait(false);
        if (exists)
            throw new Exception("Project already exists");

        var project = request.Adapt<GHotel.Domain.Entities.Project>();
        var result = await _projectRepository.Create(project,cancellationToken).ConfigureAwait(false);
        return result.Adapt<ProjectResponseModel>();
    }

    public async Task<ProjectResponseModel> GetByName(string name,CancellationToken cancellationToken)
    {
        var targetProject = await _projectRepository.GetByName(name, cancellationToken).ConfigureAwait(false);
        if (targetProject == null)
            throw new NotFoundException("Project not found");
        return targetProject.Adapt<ProjectResponseModel>();
    }

    public async Task<ProjectResponseModel> UploadImages(string currentUserEmail,string projectName, List<ImageRequestModel> images, CancellationToken cancellationToken)
    {
        var targetProject = await _projectRepository.GetByName(projectName, cancellationToken).ConfigureAwait(false);
        if (targetProject == null)
            throw new NotFoundException("Project not found");
        if (targetProject.Business!.User!.Email != currentUserEmail)
            throw new Exception("You are not authorized to upload images for this project");
        if (targetProject.Images is null)
            targetProject.Images = new List<MyImage>();

        foreach (var image in images)
            targetProject.Images!.Add(new MyImage { Url = await _imageUtility.SaveImageToFile(image, cancellationToken).ConfigureAwait(false) });
        await _projectRepository.Update(targetProject,cancellationToken).ConfigureAwait(false);
        return targetProject.Adapt<ProjectResponseModel>();
    }

    public async Task Invest(InvestRequestModel request, CancellationToken cancellationToken)
    {
        //Implement Serialized transaction isolation level for individual projects
        await _lockUtility.Lock(request.ProjectName).ConfigureAwait(false);
        try
        {
            var targetUser = await _userRepository.GetByEmail(request.InvestorEmail, cancellationToken).ConfigureAwait(false);
            if (targetUser == null)
                throw new NotFoundException("User not found");
            var targetProject = await _projectRepository.GetByName(request.ProjectName, cancellationToken).ConfigureAwait(false);
            if (targetProject == null)
                throw new NotFoundException("Project not found");

            if(request.Amount > targetProject.RequiredBudget - targetProject.CurrentBudget)
                throw new Exception("Amount exceeds the required budget");

            var percentage = (double)(request.Amount / targetProject.RequiredBudget) * 100;

            var transaction = new Transaction()
            {
                UserPersonalNumber = targetUser.PersonalNumber,
                ProjectName = request.ProjectName,
                Amount = request.Amount,
                Currency = Currency.GEL
            };

            if (targetProject.Transactions is null)
                targetProject.Transactions = new();
            targetProject.Transactions.Add(transaction);

            var share = new GHotel.Domain.Entities.Share()
            {
                ProjectName = request.ProjectName,
                SharePercentage = percentage,
                UserPersonalNumber = targetUser.PersonalNumber
            };

            if (targetProject.Shares is null)
                targetProject.Shares = new();
            targetProject.Shares.Add(share);

            targetProject.CurrentBudget += request.Amount;
            await _projectRepository.Update(targetProject, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            throw;
        }
        finally
        {
            _lockUtility.Release(request.ProjectName);
        }

    }

}
