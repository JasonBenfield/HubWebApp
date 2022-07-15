import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Forms/TextValueFormGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
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
        this.alert = new CardAlert(this.view.alert).alert;
        this.userName = new TextValueFormGroup(view.userName);
        this.userName.setCaption('User Name');
        this.fullName = new TextValueFormGroup(view.fullName);
        this.fullName.setCaption('Name');
        this.email = new TextValueFormGroup(view.email);
        this.email.setCaption('Email');
        this.editCommand.add(this.view.editButton);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private requestEdit() {
        this._editRequested.invoke(this.userID);
    }

    async refresh() {
        const user = await this.getUser(this.userID);
        this.userName.setValue(user.UserName.DisplayText);
        this.fullName.setValue(user.Name.DisplayText);
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