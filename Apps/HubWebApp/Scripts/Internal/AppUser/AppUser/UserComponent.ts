import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponentView } from "./UserComponentView";

export class UserComponent {
    private userID: number;
    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserComponentView
    ) {
        new CardTitleHeader('User', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    setUserID(userID) {
        this.userID = userID;
    }

    async refresh() {
        let user = await this.getUser(this.userID);
        this.view.setUserName(user.UserName);
        this.view.setFullName(user.Name);
        this.view.showCardBody();
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