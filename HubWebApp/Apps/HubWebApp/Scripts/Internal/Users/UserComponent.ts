import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { UserComponentView } from "./UserComponentView";

type Events = {
    editRequested: number;
    changePasswordRequested: number;
}

export class UserComponent {
    private userID: number;
    private readonly alert: MessageAlert;
    private readonly userNameFormGroup: FormGroupText;
    private readonly fullNameFormGroup: FormGroupText;
    private readonly emailFormGroup: FormGroupText;
    private readonly timeDeactivatedFormGroup: FormGroupText;
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
        private readonly hubClient: HubAppClient,
        private readonly view: UserComponentView
    ) {
        this.alert = new CardAlert(this.view.alert).alert;
        this.userNameFormGroup = new FormGroupText(view.userName);
        this.userNameFormGroup.setCaption('User Name');
        this.fullNameFormGroup = new FormGroupText(view.fullName);
        this.fullNameFormGroup.setCaption('Name');
        this.emailFormGroup = new FormGroupText(view.email);
        this.emailFormGroup.setCaption('Email');
        this.timeDeactivatedFormGroup = new FormGroupText(view.timeDeactivated);
        this.timeDeactivatedFormGroup.setCaption('Time Deactivated');
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
            () => this.hubClient.UserMaintenance.DeactivateUser(this.userID)
        );
        this.loadUser(user);
    }

    private async reactivate() {
        const user = await this.alert.infoAction(
            'Reactivating...',
            () => this.hubClient.UserMaintenance.ReactivateUser(this.userID)
        );
        this.loadUser(user);
    }

    async refresh() {
        const user = await this.getUser(this.userID);
        if (this.canEdit === null) {
            const access = await this.hubClient.getUserAccess({
                canEdit: this.hubClient.getAccessRequest(api => api.UserMaintenance.EditUserAction)
            });
            this.canEdit = access.canEdit;
        }
        this.loadUser(user);
    }

    private loadUser(user: IAppUserModel) {
        this.userNameFormGroup.setValue(user.UserName.DisplayText);
        this.fullNameFormGroup.setValue(user.Name.DisplayText);
        this.emailFormGroup.setValue(user.Email);
        if (user.TimeDeactivated.isMaxYear) {
            this.view.timeDeactivated.hide();
            this.timeDeactivatedFormGroup.setValue('');
        }
        else {
            this.view.timeDeactivated.show();
            this.timeDeactivatedFormGroup.setValue(user.TimeDeactivated.format());
        }
        if (this.canEdit) {
            if (user.TimeDeactivated.isMaxYear) {
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

    private getUser(userID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.UserInquiry.GetUser(userID)
        );
    }
}