import { Alert } from "XtiShared/Alert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponentViewModel } from "./UserComponentViewModel";

export class UserComponent {
    constructor(
        private readonly vm: UserComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let user = await this.getUser(this.userID);
        this.vm.userName(user.UserName);
        this.vm.name(user.Name);
        this.vm.email(user.Email);
    }

    private async getUser(userID: number) {
        let user: IAppUserModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                user = await this.hubApi.UserInquiry.GetUser(userID);
            }
        );
        return user;
    }
}