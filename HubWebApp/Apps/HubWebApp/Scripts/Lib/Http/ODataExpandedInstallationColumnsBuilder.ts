// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedInstallationColumnViewsBuilder {
	readonly InstallationID = new ODataColumnViewBuilder();
	readonly IsCurrent = new ODataColumnViewBuilder();
	readonly InstallationStatus = new ODataColumnViewBuilder();
	readonly TimeInstallationAdded = new ODataColumnViewBuilder();
	readonly QualifiedMachineName = new ODataColumnViewBuilder();
	readonly Domain = new ODataColumnViewBuilder();
	readonly AppID = new ODataColumnViewBuilder();
	readonly AppKey = new ODataColumnViewBuilder();
	readonly AppName = new ODataColumnViewBuilder();
	readonly AppType = new ODataColumnViewBuilder();
	readonly VersionName = new ODataColumnViewBuilder();
	readonly VersionRelease = new ODataColumnViewBuilder();
	readonly VersionKey = new ODataColumnViewBuilder();
	readonly VersionStatus = new ODataColumnViewBuilder();
	readonly VersionType = new ODataColumnViewBuilder();
	readonly LastRequestTime = new ODataColumnViewBuilder();
	readonly LastRequestDaysAgo = new ODataColumnViewBuilder();
	readonly RequestCount = new ODataColumnViewBuilder();
}

export class ODataExpandedInstallationColumnsBuilder {
	constructor(views: ODataExpandedInstallationColumnViewsBuilder) {
		this.InstallationID = new ODataColumnBuilder('InstallationID', new SourceType('Int32'), views.InstallationID);
		this.InstallationID.setDisplayText('Installation ID');
		this.IsCurrent = new ODataColumnBuilder('IsCurrent', new SourceType('Boolean'), views.IsCurrent);
		this.IsCurrent.setDisplayText('Is Current');
		this.InstallationStatus = new ODataColumnBuilder('InstallationStatus', new SourceType('String'), views.InstallationStatus);
		this.InstallationStatus.setDisplayText('Installation Status');
		this.TimeInstallationAdded = new ODataColumnBuilder('TimeInstallationAdded', new SourceType('DateTimeOffset'), views.TimeInstallationAdded);
		this.TimeInstallationAdded.setDisplayText('Time Installation Added');
		this.QualifiedMachineName = new ODataColumnBuilder('QualifiedMachineName', new SourceType('String'), views.QualifiedMachineName);
		this.QualifiedMachineName.setDisplayText('Qualified Machine Name');
		this.Domain = new ODataColumnBuilder('Domain', new SourceType('String'), views.Domain);
		this.AppID = new ODataColumnBuilder('AppID', new SourceType('Int32'), views.AppID);
		this.AppID.setDisplayText('App ID');
		this.AppKey = new ODataColumnBuilder('AppKey', new SourceType('String'), views.AppKey);
		this.AppKey.setDisplayText('App Key');
		this.AppName = new ODataColumnBuilder('AppName', new SourceType('String'), views.AppName);
		this.AppName.setDisplayText('App Name');
		this.AppType = new ODataColumnBuilder('AppType', new SourceType('String'), views.AppType);
		this.AppType.setDisplayText('App Type');
		this.VersionName = new ODataColumnBuilder('VersionName', new SourceType('String'), views.VersionName);
		this.VersionName.setDisplayText('Version Name');
		this.VersionRelease = new ODataColumnBuilder('VersionRelease', new SourceType('String'), views.VersionRelease);
		this.VersionRelease.setDisplayText('Version Release');
		this.VersionKey = new ODataColumnBuilder('VersionKey', new SourceType('String'), views.VersionKey);
		this.VersionKey.setDisplayText('Version Key');
		this.VersionStatus = new ODataColumnBuilder('VersionStatus', new SourceType('String'), views.VersionStatus);
		this.VersionStatus.setDisplayText('Version Status');
		this.VersionType = new ODataColumnBuilder('VersionType', new SourceType('String'), views.VersionType);
		this.VersionType.setDisplayText('Version Type');
		this.LastRequestTime = new ODataColumnBuilder('LastRequestTime', new SourceType('Nullable`1'), views.LastRequestTime);
		this.LastRequestTime.setDisplayText('Last Request Time');
		this.LastRequestDaysAgo = new ODataColumnBuilder('LastRequestDaysAgo', new SourceType('Nullable`1'), views.LastRequestDaysAgo);
		this.LastRequestDaysAgo.setDisplayText('Last Request Days Ago');
		this.RequestCount = new ODataColumnBuilder('RequestCount', new SourceType('Int32'), views.RequestCount);
		this.RequestCount.setDisplayText('Request Count');
	}
	readonly InstallationID: ODataColumnBuilder;
	readonly IsCurrent: ODataColumnBuilder;
	readonly InstallationStatus: ODataColumnBuilder;
	readonly TimeInstallationAdded: ODataColumnBuilder;
	readonly QualifiedMachineName: ODataColumnBuilder;
	readonly Domain: ODataColumnBuilder;
	readonly AppID: ODataColumnBuilder;
	readonly AppKey: ODataColumnBuilder;
	readonly AppName: ODataColumnBuilder;
	readonly AppType: ODataColumnBuilder;
	readonly VersionName: ODataColumnBuilder;
	readonly VersionRelease: ODataColumnBuilder;
	readonly VersionKey: ODataColumnBuilder;
	readonly VersionStatus: ODataColumnBuilder;
	readonly VersionType: ODataColumnBuilder;
	readonly LastRequestTime: ODataColumnBuilder;
	readonly LastRequestDaysAgo: ODataColumnBuilder;
	readonly RequestCount: ODataColumnBuilder;
	
	build() {
		return {
			InstallationID: this.InstallationID.build(),
			IsCurrent: this.IsCurrent.build(),
			InstallationStatus: this.InstallationStatus.build(),
			TimeInstallationAdded: this.TimeInstallationAdded.build(),
			QualifiedMachineName: this.QualifiedMachineName.build(),
			Domain: this.Domain.build(),
			AppID: this.AppID.build(),
			AppKey: this.AppKey.build(),
			AppName: this.AppName.build(),
			AppType: this.AppType.build(),
			VersionName: this.VersionName.build(),
			VersionRelease: this.VersionRelease.build(),
			VersionKey: this.VersionKey.build(),
			VersionStatus: this.VersionStatus.build(),
			VersionType: this.VersionType.build(),
			LastRequestTime: this.LastRequestTime.build(),
			LastRequestDaysAgo: this.LastRequestDaysAgo.build(),
			RequestCount: this.RequestCount.build()
		} as ODataColumns<IExpandedInstallation>;
	}
}