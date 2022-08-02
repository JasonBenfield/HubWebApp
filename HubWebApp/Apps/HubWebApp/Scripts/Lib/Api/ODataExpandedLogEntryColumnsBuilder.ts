// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedLogEntryColumnViewsBuilder {
	readonly EventID = new ODataColumnViewBuilder();
	readonly TimeOccurred = new ODataColumnViewBuilder();
	readonly SeverityText = new ODataColumnViewBuilder();
	readonly Caption = new ODataColumnViewBuilder();
	readonly Message = new ODataColumnViewBuilder();
	readonly Detail = new ODataColumnViewBuilder();
	readonly Path = new ODataColumnViewBuilder();
	readonly ActualCount = new ODataColumnViewBuilder();
	readonly AppID = new ODataColumnViewBuilder();
	readonly AppName = new ODataColumnViewBuilder();
	readonly AppTypeText = new ODataColumnViewBuilder();
	readonly ResourceGroupName = new ODataColumnViewBuilder();
	readonly ResourceName = new ODataColumnViewBuilder();
	readonly ModCategoryName = new ODataColumnViewBuilder();
	readonly ModKey = new ODataColumnViewBuilder();
	readonly ModTargetKey = new ODataColumnViewBuilder();
	readonly ModDisplayText = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly UserGroupID = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
	readonly UserGroupDisplayText = new ODataColumnViewBuilder();
	readonly RequestTimeStarted = new ODataColumnViewBuilder();
	readonly RequestTimeEnded = new ODataColumnViewBuilder();
	readonly RequestTimeElapsed = new ODataColumnViewBuilder();
	readonly VersionName = new ODataColumnViewBuilder();
	readonly VersionKey = new ODataColumnViewBuilder();
	readonly VersionStatusText = new ODataColumnViewBuilder();
	readonly VersionTypeText = new ODataColumnViewBuilder();
	readonly InstallLocation = new ODataColumnViewBuilder();
	readonly IsCurrentInstallation = new ODataColumnViewBuilder();
}

export class ODataExpandedLogEntryColumnsBuilder {
	constructor(views: ODataExpandedLogEntryColumnViewsBuilder) {
		this.EventID = new ODataColumnBuilder('EventID', new SourceType('Int32'), views.EventID);
		this.EventID.setDisplayText('Event ID');
		this.TimeOccurred = new ODataColumnBuilder('TimeOccurred', new SourceType('DateTimeOffset'), views.TimeOccurred);
		this.TimeOccurred.setDisplayText('Time Occurred');
		this.SeverityText = new ODataColumnBuilder('SeverityText', new SourceType('String'), views.SeverityText);
		this.SeverityText.setDisplayText('Severity Text');
		this.Caption = new ODataColumnBuilder('Caption', new SourceType('String'), views.Caption);
		this.Message = new ODataColumnBuilder('Message', new SourceType('String'), views.Message);
		this.Detail = new ODataColumnBuilder('Detail', new SourceType('String'), views.Detail);
		this.Path = new ODataColumnBuilder('Path', new SourceType('String'), views.Path);
		this.ActualCount = new ODataColumnBuilder('ActualCount', new SourceType('Int32'), views.ActualCount);
		this.ActualCount.setDisplayText('Actual Count');
		this.AppID = new ODataColumnBuilder('AppID', new SourceType('Int32'), views.AppID);
		this.AppID.setDisplayText('App ID');
		this.AppName = new ODataColumnBuilder('AppName', new SourceType('String'), views.AppName);
		this.AppName.setDisplayText('App Name');
		this.AppTypeText = new ODataColumnBuilder('AppTypeText', new SourceType('String'), views.AppTypeText);
		this.AppTypeText.setDisplayText('App Type Text');
		this.ResourceGroupName = new ODataColumnBuilder('ResourceGroupName', new SourceType('String'), views.ResourceGroupName);
		this.ResourceGroupName.setDisplayText('Resource Group Name');
		this.ResourceName = new ODataColumnBuilder('ResourceName', new SourceType('String'), views.ResourceName);
		this.ResourceName.setDisplayText('Resource Name');
		this.ModCategoryName = new ODataColumnBuilder('ModCategoryName', new SourceType('String'), views.ModCategoryName);
		this.ModCategoryName.setDisplayText('Mod Category Name');
		this.ModKey = new ODataColumnBuilder('ModKey', new SourceType('String'), views.ModKey);
		this.ModKey.setDisplayText('Mod Key');
		this.ModTargetKey = new ODataColumnBuilder('ModTargetKey', new SourceType('String'), views.ModTargetKey);
		this.ModTargetKey.setDisplayText('Mod Target Key');
		this.ModDisplayText = new ODataColumnBuilder('ModDisplayText', new SourceType('String'), views.ModDisplayText);
		this.ModDisplayText.setDisplayText('Mod Display Text');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.UserGroupID = new ODataColumnBuilder('UserGroupID', new SourceType('Int32'), views.UserGroupID);
		this.UserGroupID.setDisplayText('User Group ID');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
		this.UserGroupDisplayText = new ODataColumnBuilder('UserGroupDisplayText', new SourceType('String'), views.UserGroupDisplayText);
		this.UserGroupDisplayText.setDisplayText('User Group Display Text');
		this.RequestTimeStarted = new ODataColumnBuilder('RequestTimeStarted', new SourceType('DateTimeOffset'), views.RequestTimeStarted);
		this.RequestTimeStarted.setDisplayText('Request Time Started');
		this.RequestTimeEnded = new ODataColumnBuilder('RequestTimeEnded', new SourceType('DateTimeOffset'), views.RequestTimeEnded);
		this.RequestTimeEnded.setDisplayText('Request Time Ended');
		this.RequestTimeElapsed = new ODataColumnBuilder('RequestTimeElapsed', new SourceType('String'), views.RequestTimeElapsed);
		this.RequestTimeElapsed.setDisplayText('Request Time Elapsed');
		this.VersionName = new ODataColumnBuilder('VersionName', new SourceType('String'), views.VersionName);
		this.VersionName.setDisplayText('Version Name');
		this.VersionKey = new ODataColumnBuilder('VersionKey', new SourceType('String'), views.VersionKey);
		this.VersionKey.setDisplayText('Version Key');
		this.VersionStatusText = new ODataColumnBuilder('VersionStatusText', new SourceType('String'), views.VersionStatusText);
		this.VersionStatusText.setDisplayText('Version Status Text');
		this.VersionTypeText = new ODataColumnBuilder('VersionTypeText', new SourceType('String'), views.VersionTypeText);
		this.VersionTypeText.setDisplayText('Version Type Text');
		this.InstallLocation = new ODataColumnBuilder('InstallLocation', new SourceType('String'), views.InstallLocation);
		this.InstallLocation.setDisplayText('Install Location');
		this.IsCurrentInstallation = new ODataColumnBuilder('IsCurrentInstallation', new SourceType('Boolean'), views.IsCurrentInstallation);
		this.IsCurrentInstallation.setDisplayText('Is Current Installation');
	}
	readonly EventID: ODataColumnBuilder;
	readonly TimeOccurred: ODataColumnBuilder;
	readonly SeverityText: ODataColumnBuilder;
	readonly Caption: ODataColumnBuilder;
	readonly Message: ODataColumnBuilder;
	readonly Detail: ODataColumnBuilder;
	readonly Path: ODataColumnBuilder;
	readonly ActualCount: ODataColumnBuilder;
	readonly AppID: ODataColumnBuilder;
	readonly AppName: ODataColumnBuilder;
	readonly AppTypeText: ODataColumnBuilder;
	readonly ResourceGroupName: ODataColumnBuilder;
	readonly ResourceName: ODataColumnBuilder;
	readonly ModCategoryName: ODataColumnBuilder;
	readonly ModKey: ODataColumnBuilder;
	readonly ModTargetKey: ODataColumnBuilder;
	readonly ModDisplayText: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly UserGroupID: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	readonly UserGroupDisplayText: ODataColumnBuilder;
	readonly RequestTimeStarted: ODataColumnBuilder;
	readonly RequestTimeEnded: ODataColumnBuilder;
	readonly RequestTimeElapsed: ODataColumnBuilder;
	readonly VersionName: ODataColumnBuilder;
	readonly VersionKey: ODataColumnBuilder;
	readonly VersionStatusText: ODataColumnBuilder;
	readonly VersionTypeText: ODataColumnBuilder;
	readonly InstallLocation: ODataColumnBuilder;
	readonly IsCurrentInstallation: ODataColumnBuilder;
	
	build() {
		return {
			EventID: this.EventID.build(),
			TimeOccurred: this.TimeOccurred.build(),
			SeverityText: this.SeverityText.build(),
			Caption: this.Caption.build(),
			Message: this.Message.build(),
			Detail: this.Detail.build(),
			Path: this.Path.build(),
			ActualCount: this.ActualCount.build(),
			AppID: this.AppID.build(),
			AppName: this.AppName.build(),
			AppTypeText: this.AppTypeText.build(),
			ResourceGroupName: this.ResourceGroupName.build(),
			ResourceName: this.ResourceName.build(),
			ModCategoryName: this.ModCategoryName.build(),
			ModKey: this.ModKey.build(),
			ModTargetKey: this.ModTargetKey.build(),
			ModDisplayText: this.ModDisplayText.build(),
			UserName: this.UserName.build(),
			UserGroupID: this.UserGroupID.build(),
			UserGroupName: this.UserGroupName.build(),
			UserGroupDisplayText: this.UserGroupDisplayText.build(),
			RequestTimeStarted: this.RequestTimeStarted.build(),
			RequestTimeEnded: this.RequestTimeEnded.build(),
			RequestTimeElapsed: this.RequestTimeElapsed.build(),
			VersionName: this.VersionName.build(),
			VersionKey: this.VersionKey.build(),
			VersionStatusText: this.VersionStatusText.build(),
			VersionTypeText: this.VersionTypeText.build(),
			InstallLocation: this.InstallLocation.build(),
			IsCurrentInstallation: this.IsCurrentInstallation.build()
		} as ODataColumns<IExpandedLogEntry>;
	}
}