import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { UserListPanel } from './UserList/UserListPanel';
import { SingleActivePanel } from '../Panel/SingleActivePanel';
import { UserPanel } from './User/UserPanel';
import { UserEditPanel } from './UserEdit/UserEditPanel';

@singleton()
class MainPage {
    constructor(
        private readonly vm: MainPageViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.activateUserListPanel();
    }

    private readonly panels = new SingleActivePanel();
    private readonly userListPanel = this.panels.add(
        this.vm.userListPanel,
        vm => new UserListPanel(vm, this.hubApi)
    );
    private readonly userPanel = this.panels.add(
        this.vm.userPanel,
        vm => new UserPanel(vm, this.hubApi)
    );
    private readonly userEditPanel = this.panels.add(
        this.vm.userEditPanel,
        vm => new UserEditPanel(vm, this.hubApi)
    );

    private async activateUserListPanel() {
        this.panels.activate(this.userListPanel);
        this.userListPanel.content.refresh();
        let result = await this.userListPanel.content.start();
        if (result.key === UserListPanel.ResultKeys.userSelected) {
            let user: IAppUserModel = result.data;
            this.activateUserPanel(user.ID);
        }
    }

    private async activateUserPanel(userID: number) {
        this.panels.activate(this.userPanel);
        this.userPanel.content.setUserID(userID);
        this.userPanel.content.refresh();
        let result = await this.userPanel.content.start();
        if (result.key === UserPanel.ResultKeys.backRequested) {
            this.activateUserListPanel();
        }
        else if (result.key === UserPanel.ResultKeys.editRequested) {
            this.activateUserEditPanel(userID);
        }
    }

    private async activateUserEditPanel(userID: number) {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.content.setUserID(userID);
        this.userEditPanel.content.refresh();
        let result = await this.userEditPanel.content.start();
        if (result.key === UserEditPanel.ResultKeys.canceled ||
            result.key === UserEditPanel.ResultKeys.saved) {
            this.activateUserPanel(userID);
        }
    }
}
startup(MainPageViewModel, MainPage);