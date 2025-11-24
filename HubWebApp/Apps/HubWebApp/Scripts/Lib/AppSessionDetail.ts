import { AppSession } from "./AppSession";
import { AppUser } from "./AppUser";
import { AppUserGroup } from "./AppUserGroup";

export class AppSessionDetail {
	readonly session: AppSession;
	readonly userGroup: AppUserGroup;
	readonly user: AppUser;

	constructor(source?: IAppSessionDetailModel) {
		this.session = new AppSession(source && source.Session);
		this.userGroup = new AppUserGroup(source && source.UserGroup);
		this.user = new AppUser(source && source.User);
    }
}