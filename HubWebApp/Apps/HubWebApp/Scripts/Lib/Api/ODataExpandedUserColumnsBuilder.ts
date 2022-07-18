// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedUserColumnViewsBuilder {
	readonly UserID = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly PersonName = new ODataColumnViewBuilder();
	readonly Email = new ODataColumnViewBuilder();
	readonly TimeUserAdded = new ODataColumnViewBuilder();
	readonly UserGroupID = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
}

export class ODataExpandedUserColumnsBuilder {
	constructor(views: ODataExpandedUserColumnViewsBuilder) {
		this.UserID = new ODataColumnBuilder('UserID', new SourceType('Int32'), views.UserID);
		this.UserID.setDisplayText('User ID');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.PersonName = new ODataColumnBuilder('PersonName', new SourceType('String'), views.PersonName);
		this.PersonName.setDisplayText('Person Name');
		this.Email = new ODataColumnBuilder('Email', new SourceType('String'), views.Email);
		this.TimeUserAdded = new ODataColumnBuilder('TimeUserAdded', new SourceType('DateTimeOffset'), views.TimeUserAdded);
		this.TimeUserAdded.setDisplayText('Time User Added');
		this.UserGroupID = new ODataColumnBuilder('UserGroupID', new SourceType('Int32'), views.UserGroupID);
		this.UserGroupID.setDisplayText('User Group ID');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
	}
	readonly UserID: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly PersonName: ODataColumnBuilder;
	readonly Email: ODataColumnBuilder;
	readonly TimeUserAdded: ODataColumnBuilder;
	readonly UserGroupID: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	
	build() {
		return {
			UserID: this.UserID.build(),
			UserName: this.UserName.build(),
			PersonName: this.PersonName.build(),
			Email: this.Email.build(),
			TimeUserAdded: this.TimeUserAdded.build(),
			UserGroupID: this.UserGroupID.build(),
			UserGroupName: this.UserGroupName.build()
		} as ODataColumns<IExpandedUser>;
	}
}