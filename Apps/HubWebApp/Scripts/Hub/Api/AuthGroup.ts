// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";
import { VerifyLoginForm } from "./VerifyLoginForm";

export class AuthGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Auth');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.VerifyLoginAction = this.createAction<VerifyLoginForm,IEmptyActionResult>('VerifyLogin', 'Verify Login');
		this.VerifyLoginForm = this.createView<IEmptyRequest>('VerifyLoginForm');
		this.Login = this.createView<ILoginModel>('Login');
		this.Logout = this.createView<IEmptyRequest>('Logout');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly VerifyLoginAction: AppApiAction<VerifyLoginForm,IEmptyActionResult>;
	readonly VerifyLoginForm: AppApiView<IEmptyRequest>;
	readonly Login: AppApiView<ILoginModel>;
	readonly Logout: AppApiView<IEmptyRequest>;
	
	VerifyLogin(model: VerifyLoginForm, errorOptions?: IActionErrorOptions) {
		return this.VerifyLoginAction.execute(model, errorOptions || {});
	}
}