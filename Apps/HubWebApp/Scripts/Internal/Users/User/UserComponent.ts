import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponentView } from "./UserComponentView";

export class UserComponent {
    private userID: number;
    private readonly _editRequested = new DefaultEvent<number>(this);
    readonly editRequested = this._editRequested.handler();
    private readonly editCommand = new Command(this.requestEdit.bind(this));
    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserComponentView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.editCommand.add(this.view.editButton);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private requestEdit() {
        this._editRequested.invoke(this.userID);
    }

    async refresh() {
        let user = await this.getUser(this.userID);
        this.view.setUserName(user.UserName);
        this.view.setFullName(user.Name);
        this.view.setEmail(user.Email);
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