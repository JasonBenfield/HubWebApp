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
		this.VerifyLoginAction = this.createAction<VerifyLoginForm,string>('VerifyLogin', 'Verify Login');
		this.VerifyLoginForm = this.createView<IEmptyRequest>('VerifyLoginForm');
		this.Login = this.createView<ILoginModel>('Login');
		this.LoginReturnKeyAction = this.createAction<ILoginReturnModel,string>('LoginReturnKey', 'Login Return Key');
	}
	
	readonly VerifyLoginAction: AppApiAction<VerifyLoginForm,string>;
	readonly VerifyLoginForm: AppApiView<IEmptyRequest>;
	readonly Login: AppApiView<ILoginModel>;
	readonly LoginReturnKeyAction: AppApiAction<ILoginReturnModel,string>;
	
	VerifyLogin(model: VerifyLoginForm, errorOptions?: IActionErrorOptions) {
		return this.VerifyLoginAction.execute(model, errorOptions || {});
	}
	LoginReturnKey(model: ILoginReturnModel, errorOptions?: IActionErrorOptions) {
		return this.LoginReturnKeyAction.execute(model, errorOptions || {});
	}
}