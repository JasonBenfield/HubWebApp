import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { UserListPanel } from './UserListPanel';

@singleton()
class MainPage {
    constructor(
        private readonly vm: MainPageViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.userListPanel.refresh();
    }

    readonly userListPanel = new UserListPanel(this.vm.userListPanel, this.hubApi);
}
startup(MainPageViewModel, MainPage);