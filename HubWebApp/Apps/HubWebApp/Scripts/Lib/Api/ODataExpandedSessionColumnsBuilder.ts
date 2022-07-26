// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedSessionColumnViewsBuilder {
	readonly SessionID = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
	readonly RemoteAddress = new ODataColumnViewBuilder();
	readonly UserAgent = new ODataColumnViewBuilder();
	readonly TimeStarted = new ODataColumnViewBuilder();
	readonly TimeEnded = new ODataColumnViewBuilder();
	readonly LastRequestTime = new ODataColumnViewBuilder();
	readonly RequestCount = new ODataColumnViewBuilder();
}

export class ODataExpandedSessionColumnsBuilder {
	constructor(views: ODataExpandedSessionColumnViewsBuilder) {
		this.SessionID = new ODataColumnBuilder('SessionID', new SourceType('Int32'), views.SessionID);
		this.SessionID.setDisplayText('Session ID');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
		this.RemoteAddress = new ODataColumnBuilder('RemoteAddress', new SourceType('String'), views.RemoteAddress);
		this.RemoteAddress.setDisplayText('Remote Address');
		this.UserAgent = new ODataColumnBuilder('UserAgent', new SourceType('String'), views.UserAgent);
		this.UserAgent.setDisplayText('User Agent');
		this.TimeStarted = new ODataColumnBuilder('TimeStarted', new SourceType('DateTimeOffset'), views.TimeStarted);
		this.TimeStarted.setDisplayText('Time Started');
		this.TimeEnded = new ODataColumnBuilder('TimeEnded', new SourceType('DateTimeOffset'), views.TimeEnded);
		this.TimeEnded.setDisplayText('Time Ended');
		this.LastRequestTime = new ODataColumnBuilder('LastRequestTime', new SourceType('Nullable`1'), views.LastRequestTime);
		this.LastRequestTime.setDisplayText('Last Request Time');
		this.RequestCount = new ODataColumnBuilder('RequestCount', new SourceType('Int32'), views.RequestCount);
		this.RequestCount.setDisplayText('Request Count');
	}
	readonly SessionID: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	readonly RemoteAddress: ODataColumnBuilder;
	readonly UserAgent: ODataColumnBuilder;
	readonly TimeStarted: ODataColumnBuilder;
	readonly TimeEnded: ODataColumnBuilder;
	readonly LastRequestTime: ODataColumnBuilder;
	readonly RequestCount: ODataColumnBuilder;
	
	build() {
		return {
			SessionID: this.SessionID.build(),
			UserName: this.UserName.build(),
			UserGroupName: this.UserGroupName.build(),
			RemoteAddress: this.RemoteAddress.build(),
			UserAgent: this.UserAgent.build(),
			TimeStarted: this.TimeStarted.build(),
			TimeEnded: this.TimeEnded.build(),
			LastRequestTime: this.LastRequestTime.build(),
			RequestCount: this.RequestCount.build()
		} as ODataColumns<IExpandedSession>;
	}
}