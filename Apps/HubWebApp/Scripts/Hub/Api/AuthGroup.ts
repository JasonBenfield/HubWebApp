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
		this.Start = this.createView<IStartRequest>('Start');
		this.LoginAction = this.createAction<ILoginModel,ILoginResult>('Login', 'Login');
		this.Logout = this.createView<IEmptyRequest>('Logout');
	}

	readonly Index: AppApiView<IEmptyRequest>;
	readonly Start: AppApiView<IStartRequest>;
	private readonly LoginAction: AppApiAction<ILoginModel,ILoginResult>;
	readonly Logout: AppApiView<IEmptyRequest>;

	Login(model: ILoginModel, errorOptions?: IActionErrorOptions) {
		return this.LoginAction.execute(model, errorOptions || {});
	}
}