// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class InstallTemplatesGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'InstallTemplates');
		this.GetInstallTemplatesAction = this.createAction<IEmptyRequest,IInstallConfigurationTemplateModel[]>('GetInstallTemplates', 'Get Install Templates');
		this.Index = this.createView<IEmptyRequest>('Index');
	}
	
	readonly GetInstallTemplatesAction: AppClientAction<IEmptyRequest,IInstallConfigurationTemplateModel[]>;
	readonly Index: AppClientView<IEmptyRequest>;
	
	GetInstallTemplates(errorOptions?: IActionErrorOptions) {
		return this.GetInstallTemplatesAction.execute({}, errorOptions || {});
	}
}