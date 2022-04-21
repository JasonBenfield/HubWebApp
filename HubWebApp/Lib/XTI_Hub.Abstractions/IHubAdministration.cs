﻿using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public interface IHubAdministration
{
    Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, AppDefinitionModel[] appDefs);

    Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey);

    Task<XtiVersionModel[]> Versions(AppVersionName versionName);

    Task AddOrUpdateVersions(AppDefinitionModel appDef, XtiVersionModel[] publishedVersions);

    Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey);

    Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey);

    Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password);

    Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string domain, string password);

    Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName);

    Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName);

    Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName);

    Task Installed(int installationID);
}