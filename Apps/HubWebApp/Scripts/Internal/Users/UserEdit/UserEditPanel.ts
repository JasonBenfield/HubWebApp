import { Awaitable } from "XtiShared/Awaitable";
import { AsyncCommand, Command } from "XtiShared/Command";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserEditPanelViewModel } from "./UserEditPanelViewModel";
import { EditUserForm } from '../../../Hub/Api/EditUserForm';
import { Alert } from "XtiShared/Alert";
import { DelayedAction } from 'XtiShared/DelayedAction';

export class UserEditPanel {
    public static readonly ResultKeys = {
        canceled: 'canceled',
        saved: 'saved'
    };

    constructor(
        private readonly vm: UserEditPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let cancelIcon = this.cancelCommand.icon();
        cancelIcon.setName('fa-times');
        this.cancelCommand.setText('Cancel');
        this.cancelCommand.makeDanger();
        let saveIcon = this.saveCommand.icon();
        saveIcon.setName('fa-check');
        this.saveCommand.setText('Save');
        this.saveCommand.makePrimary();
    }

    private readonly editUserForm = new EditUserForm(this.vm.editUserForm);

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let userForm = await this.getUserForEdit(this.userID);
        this.editUserForm.import(userForm);
        new DelayedAction(
            () => this.editUserForm.PersonName.setFocus(),
            1000
        ).execute();
    }

    private async getUserForEdit(userID: number) {
        let userForm: Record<string,object>;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                userForm = await this.hubApi.UserMaintenance.GetUserForEdit(userID);
            }
        );
        return userForm;
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly cancelCommand = new Command(this.vm.cancelCommand, this.cancel.bind(this));

    private cancel() {
        this.awaitable.resolve(
            new Result(UserEditPanel.ResultKeys.canceled)
        );
    }

    private readonly saveCommand = new AsyncCommand(this.vm.saveCommand, this.save.bind(this));

    private async save() {
        let result = await this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction);
        if (result.succeeded()) {
            this.awaitable.resolve(
                new Result(UserEditPanel.ResultKeys.saved)
            );
        }
    }
}