import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { AppUser } from "../../Lib/AppUser";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { UserComponentView } from "./UserComponentView";

type Events = {
    editRequested: number;
    changePasswordRequested: number;
}

export class UserComponent {
    private userID: number;
    private readonly alert: IMessageAlert;
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
        this.alert = new CardAlert(this.view.alertView);
        this.userNameFormGroup = new FormGroupText(view.userNameFormGroupView);
        this.userNameFormGroup.setCaption('User Name');
        this.fullNameFormGroup = new FormGroupText(view.fullNameFormGroupView);
        this.fullNameFormGroup.setCaption('Name');
        this.emailFormGroup = new FormGroupText(view.emailFormGroupView);
        this.emailFormGroup.setCaption('Email');
        this.timeDeactivatedFormGroup = new FormGroupText(view.timeDeactivatedFormGroupView);
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
        this.reset();
        const sourceUser = await this.alert.infoAction(
            'Deactivating...',
            () => this.hubClient.UserMaintenance.DeactivateUser(this.userID)
        );
        this.loadUser(new AppUser(sourceUser));
    }

    private async reactivate() {
        this.reset();
        const sourceUser = await this.alert.infoAction(
            'Reactivating...',
            () => this.hubClient.UserMaintenance.ReactivateUser(this.userID)
        );
        this.loadUser(new AppUser(sourceUser));
    }

    async refresh() {
        this.reset();
        const user = await this.getUser(this.userID);
        if (this.canEdit === null) {
            const access = await this.hubClient.getUserAccess({
                canEdit: this.hubClient.getAccessRequest(api => api.UserMaintenance.EditUserAction)
            });
            this.canEdit = access.canEdit;
        }
        this.loadUser(new AppUser(user));
    }

    private reset() {
        this.emailFormGroup.hide();
        this.timeDeactivatedFormGroup.hide();
        this.editCommand.hide();
        this.changePasswordCommand.hide();
        this.deactivateCommand.hide();
        this.reactivateCommand.hide();
    }

    private loadUser(user: AppUser) {
        this.userNameFormGroup.setValue(user.userName.displayText);
        this.fullNameFormGroup.setValue(user.name.displayText);
        this.emailFormGroup.setValue(user.email);
        if (user.email) {
            this.emailFormGroup.show();
        }
        if (user.isActive) {
            this.timeDeactivatedFormGroup.setValue('');
        }
        else {
            this.timeDeactivatedFormGroup.setValue(user.timeDeactivated.format());
            this.view.timeDeactivatedFormGroupView.show();
        }
        if (this.canEdit) {
            if (user.isActive) {
                this.editCommand.show();
                this.changePasswordCommand.show();
                this.deactivateCommand.show();
            }
            else {
                this.reactivateCommand.show();
            }
        }
    }

    private getUser(userID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.UserInquiry.GetUser({ UserID: userID })
        );
    }
}