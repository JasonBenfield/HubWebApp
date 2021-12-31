import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Html/TextValueFormGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponentView } from "./UserComponentView";

export class UserComponent {
    private userID: number;
    private readonly _editRequested = new DefaultEvent<number>(this);
    readonly editRequested = this._editRequested.handler();
    private readonly editCommand = new Command(this.requestEdit.bind(this));
    private readonly alert: MessageAlert;
    private readonly userName: TextValueFormGroup;
    private readonly fullName: TextValueFormGroup;
    private readonly email: TextValueFormGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserComponentView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.userName = new TextValueFormGroup(view.userName);
        this.userName.setCaption('User Name');
        this.userName.syncValueTitleWithText();
        this.fullName = new TextValueFormGroup(view.fullName);
        this.fullName.setCaption('Name');
        this.fullName.syncValueTitleWithText();
        this.email = new TextValueFormGroup(view.email);
        this.email.setCaption('Email');
        this.email.syncValueTitleWithText();
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
        this.userName.setValue(user.UserName);
        this.fullName.setValue(user.Name);
        this.email.setValue(user.Email);
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