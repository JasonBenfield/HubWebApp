// Generated code

import { AppApiGroup } from "../../Hub/AppApiGroup";
import { AppApiAction } from "../../Hub/AppApiAction";
import { AppApiView } from "../../Hub/AppApiView";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { AppResourceUrl } from "../../Hub/AppResourceUrl";

export class AuthGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Auth');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.VerifyAction = this.createAction<ILoginCredentials,IEmptyActionResult>('Verify', 'Verify');
		this.Login = this.createView<ILoginModel>('Login');
		this.Logout = this.createView<IEmptyRequest>('Logout');
	}

	readonly Index: AppApiView<IEmptyRequest>;
	private readonly VerifyAction: AppApiAction<ILoginCredentials,IEmptyActionResult>;
	readonly Login: AppApiView<ILoginModel>;
	readonly Logout: AppApiView<IEmptyRequest>;

	Verify(model: ILoginCredentials, errorOptions?: IActionErrorOptions) {
		return this.VerifyAction.execute(model, errorOptions || {});
	}
}