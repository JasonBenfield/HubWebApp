import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";
import { AppUserName } from "./AppUserName";
import { PersonName } from "./PersonName";

export class AppUser {
    readonly id: number;
    readonly userName: AppUserName;
    readonly name: PersonName;
    readonly email: string;
    readonly timeDeactivated: DateTimeOffset;

    constructor(source?: IAppUserModel) {
        this.id = source ? source.ID : 0;
        this.userName = new AppUserName(source && source.UserName);
        this.name = new PersonName(source && source.Name);
        this.email = source ? source.Email : "";
        this.timeDeactivated = source ? source.TimeDeactivated : DateTimeOffset.max();
    }

    get isActive() { return this.timeDeactivated.compareTo(DateTimeOffset.now()) > 0; }
}