﻿using XTI_Core;

namespace XTI_SupportServiceAppApi;

public sealed class SupportAppApiFactory : AppApiFactory
{
    private readonly IServiceProvider sp;

    public SupportAppApiFactory(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public new SupportAppApi Create(IAppApiUser user) => (SupportAppApi)base.Create(user);
    public new SupportAppApi CreateForSuperUser() => (SupportAppApi)base.CreateForSuperUser();

    protected override IAppApi _Create(IAppApiUser user) => 
        new SupportAppApi(user, XtiSerializer.Serialize(new SupportServiceAppOptions()), sp);
}