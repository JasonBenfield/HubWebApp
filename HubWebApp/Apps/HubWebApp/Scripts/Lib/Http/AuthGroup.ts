// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";
import { VerifyLoginForm } from "./VerifyLoginForm";

export class AuthGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Auth');
		this.Login = this.createView<IAuthenticatedLoginRequest>('Login');
		this.LoginReturnKeyAction = this.createAction<ILoginReturnModel,string>('LoginReturnKey', 'Login Return Key');
		this.VerifyLoginAction = this.createAction<VerifyLoginForm,IAuthenticatedLoginResult>('VerifyLogin', 'Verify Login');
		this.VerifyLoginForm = this.createView<IEmptyRequest>('VerifyLoginForm');
	}
	
	readonly Login: AppClientView<IAuthenticatedLoginRequest>;
	readonly LoginReturnKeyAction: AppClientAction<ILoginReturnModel,string>;
	readonly VerifyLoginAction: AppClientAction<VerifyLoginForm,IAuthenticatedLoginResult>;
	readonly VerifyLoginForm: AppClientView<IEmptyRequest>;
	
	LoginReturnKey(requestData: ILoginReturnModel, errorOptions?: IActionErrorOptions) {
		return this.LoginReturnKeyAction.execute(requestData, errorOptions || {});
	}
	VerifyLogin(requestData: VerifyLoginForm, errorOptions?: IActionErrorOptions) {
		return this.VerifyLoginAction.execute(requestData, errorOptions || {});
	}
}