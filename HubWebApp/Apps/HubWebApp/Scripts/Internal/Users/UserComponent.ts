import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Forms/TextValueFormGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { UserComponentView } from "./UserComponentView";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";

type Events = {
    editRequested: number;
    changePasswordRequested: number;
}

export class UserComponent {
    private userID: number;
    private readonly alert: MessageAlert;
    private readonly userName: TextValueFormGroup;
    private readonly fullName: TextValueFormGroup;
    private readonly email: TextValueFormGroup;
    private readonly timeDeactivated: TextValueFormGroup;
    private readonly editCommand: Command;
    private readonly changePasswordCommand: Command;
    private readonly deactivateCommand: AsyncCommand;
    private readonly reactivateCommand: AsyncCommand;

    private readonly eventSource = new EventSource<Events>(this, {
        editRequested: null as number,
        changePasswordRequested: null as number
    });
    readonly when = this.eventSource.when;
    private canEdit: boolean = null;

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
        this.timeDeactivated = new TextValueFormGroup(view.timeDeactivated);
        this.timeDeactivated.setCaption('Time Deactivated');
        this.editCommand = new Command(this.requestEdit.bind(this));
        this.editCommand.add(this.view.editButton);
        this.editCommand.hide();
        this.changePasswordCommand = new Command(this.requestChangePassword.bind(this));
        this.changePasswordCommand.add(view.changePasswordButton);
        this.changePasswordCommand.hide();
        this.deactivateCommand = new AsyncCommand(this.deactivate.bind(this));
        this.deactivateCommand.add(view.deactivateButton);
        this.deactivateCommand.hide();
        this.reactivateCommand = new AsyncCommand(this.reactivate.bind(this));
        this.reactivateCommand.add(view.reactivateButton);
        this.reactivateCommand.hide();
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private requestEdit() {
        this.eventSource.events.editRequested.invoke(this.userID);
    }

    private requestChangePassword() {
        this.eventSource.events.changePasswordRequested.invoke(this.userID);
    }

    private async deactivate() {
        const user = await this.alert.infoAction(
            'Deactivating...',
            () => this.hubApi.UserMaintenance.DeactivateUser(this.userID)
        );
        this.loadUser(user);
    }

    private async reactivate() {
        const user = await this.alert.infoAction(
            'Reactivating...',
            () => this.hubApi.UserMaintenance.ReactivateUser(this.userID)
        );
        this.loadUser(user);
    }

    async refresh() {
        const user = await this.getUser(this.userID);
        if (this.canEdit === null) {
            const access = await this.hubApi.getUserAccess({
                canEdit: this.hubApi.getAccessRequest(api => api.UserMaintenance.EditUserAction)
            });
            this.canEdit = access.canEdit;
        }
        this.loadUser(user);
    }

    private loadUser(user: IAppUserModel) {
        this.userName.setValue(user.UserName.DisplayText);
        this.fullName.setValue(user.Name.DisplayText);
        this.email.setValue(user.Email);
        if (user.TimeDeactivated.getFullYear() === 9999) {
            this.view.timeDeactivated.hide();
            this.timeDeactivated.setValue('');
        }
        else {
            this.view.timeDeactivated.show();
            this.timeDeactivated.setValue(new FormattedDate(user.TimeDeactivated).formatDateTime());
        }
        if (this.canEdit) {
            if (user.TimeDeactivated.getFullYear() === 9999) {
                this.editCommand.show();
                this.changePasswordCommand.show();
                this.deactivateCommand.show();
                this.reactivateCommand.hide();
            }
            else {
                this.editCommand.hide();
                this.changePasswordCommand.hide();
                this.deactivateCommand.hide();
                this.reactivateCommand.show();
            }
        }
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