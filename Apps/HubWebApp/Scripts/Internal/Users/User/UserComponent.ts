import { Alert } from "XtiShared/Alert";
import { DefaultEvent } from "XtiShared/Events";
import { Command } from "../../../../Imports/Shared/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponentViewModel } from "./UserComponentViewModel";

export class UserComponent {
    constructor(
        private readonly vm: UserComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let icon = this.editCommand.icon();
        icon.setName('fa-edit');
        this.editCommand.setText('Edit');
        this.editCommand.makePrimary();
    }

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    private readonly _editRequested = new DefaultEvent<number>(this);
    readonly editRequested = this._editRequested.handler();

    private readonly editCommand = new Command(this.vm.editCommand, this.requestEdit.bind(this));

    private requestEdit() {
        this._editRequested.invoke(this.userID);
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