﻿using Lexicon.Frontend.Services;
using Microsoft.JSInterop;

namespace Lexicon.Frontend.ServicesImp;

public class UnitOfWork : IUnitOfWork
{

    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public UnitOfWork(HttpClient httpClient, IJSRuntime jsRuntime)
    {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
    }

    public ISessionStorageService SessionStorageService => new SessionStorageService(_jsRuntime);
    public IUserService UserService => new UserService(_httpClient, SessionStorageService);
    public IModuleService ModuleService => new ModuleService(_httpClient, SessionStorageService);
    public IActivityService ActivityService => new ActivityService(_httpClient, SessionStorageService);
    public ICourseService CourseService => new CourseService(_httpClient, SessionStorageService);
    public IDocumentService DocumentService => new DocumentService(_httpClient);
    public IAuthService AuthService => new AuthService(_httpClient, SessionStorageService);

}