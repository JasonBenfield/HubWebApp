import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";
import { AppUserName } from "./AppUserName";
import { PersonName } from "./PersonName";

export class AppUser {
	readonly id: number;
	readonly userName: AppUserName;
	readonly name: PersonName;
	readonly email: string;
	readonly timeDeactivated: DateTimeOffset;

	constructor(readonly source: IAppUserModel) {
		this.id = source.ID;
		this.userName = new AppUserName(source.UserName);
		this.name = new PersonName(source.Name);
		this.email = source.Email;
		this.timeDeactivated = source.TimeDeactivated;
	}

	get isActive() { return this.timeDeactivated.compareTo(DateTimeOffset.now()) > 0; }
}