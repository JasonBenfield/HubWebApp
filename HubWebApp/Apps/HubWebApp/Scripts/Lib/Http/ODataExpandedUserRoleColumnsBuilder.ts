// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedUserRoleColumnViewsBuilder {
	readonly UserRoleID = new ODataColumnViewBuilder();
	readonly UserGroupDisplayText = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly ModCategoryName = new ODataColumnViewBuilder();
	readonly RoleDisplayText = new ODataColumnViewBuilder();
	readonly AppKey = new ODataColumnViewBuilder();
	readonly AppID = new ODataColumnViewBuilder();
	readonly UserGroupID = new ODataColumnViewBuilder();
	readonly UserID = new ODataColumnViewBuilder();
	readonly RoleID = new ODataColumnViewBuilder();
	readonly ModCategoryID = new ODataColumnViewBuilder();
	readonly ModifierID = new ODataColumnViewBuilder();
}

export class ODataExpandedUserRoleColumnsBuilder {
	constructor(views: ODataExpandedUserRoleColumnViewsBuilder) {
		this.UserRoleID = new ODataColumnBuilder('UserRoleID', new SourceType('Int32'), views.UserRoleID);
		this.UserRoleID.setDisplayText('User Role ID');
		this.UserGroupDisplayText = new ODataColumnBuilder('UserGroupDisplayText', new SourceType('String'), views.UserGroupDisplayText);
		this.UserGroupDisplayText.setDisplayText('User Group Display Text');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.ModCategoryName = new ODataColumnBuilder('ModCategoryName', new SourceType('String'), views.ModCategoryName);
		this.ModCategoryName.setDisplayText('Mod Category Name');
		this.RoleDisplayText = new ODataColumnBuilder('RoleDisplayText', new SourceType('String'), views.RoleDisplayText);
		this.RoleDisplayText.setDisplayText('Role Display Text');
		this.AppKey = new ODataColumnBuilder('AppKey', new SourceType('String'), views.AppKey);
		this.AppKey.setDisplayText('App Key');
		this.AppID = new ODataColumnBuilder('AppID', new SourceType('Int32'), views.AppID);
		this.AppID.setDisplayText('App ID');
		this.UserGroupID = new ODataColumnBuilder('UserGroupID', new SourceType('Int32'), views.UserGroupID);
		this.UserGroupID.setDisplayText('User Group ID');
		this.UserID = new ODataColumnBuilder('UserID', new SourceType('Int32'), views.UserID);
		this.UserID.setDisplayText('User ID');
		this.RoleID = new ODataColumnBuilder('RoleID', new SourceType('Int32'), views.RoleID);
		this.RoleID.setDisplayText('Role ID');
		this.ModCategoryID = new ODataColumnBuilder('ModCategoryID', new SourceType('Int32'), views.ModCategoryID);
		this.ModCategoryID.setDisplayText('Mod Category ID');
		this.ModifierID = new ODataColumnBuilder('ModifierID', new SourceType('Int32'), views.ModifierID);
		this.ModifierID.setDisplayText('Modifier ID');
	}
	readonly UserRoleID: ODataColumnBuilder;
	readonly UserGroupDisplayText: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly ModCategoryName: ODataColumnBuilder;
	readonly RoleDisplayText: ODataColumnBuilder;
	readonly AppKey: ODataColumnBuilder;
	readonly AppID: ODataColumnBuilder;
	readonly UserGroupID: ODataColumnBuilder;
	readonly UserID: ODataColumnBuilder;
	readonly RoleID: ODataColumnBuilder;
	readonly ModCategoryID: ODataColumnBuilder;
	readonly ModifierID: ODataColumnBuilder;
	
	build() {
		return {
			UserRoleID: this.UserRoleID.build(),
			UserGroupDisplayText: this.UserGroupDisplayText.build(),
			UserName: this.UserName.build(),
			ModCategoryName: this.ModCategoryName.build(),
			RoleDisplayText: this.RoleDisplayText.build(),
			AppKey: this.AppKey.build(),
			AppID: this.AppID.build(),
			UserGroupID: this.UserGroupID.build(),
			UserID: this.UserID.build(),
			RoleID: this.RoleID.build(),
			ModCategoryID: this.ModCategoryID.build(),
			ModifierID: this.ModifierID.build()
		} as ODataColumns<IExpandedUserRole>;
	}
}