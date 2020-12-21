import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';
import { Alert } from '../../Shared/Alert';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { AppListItem } from './AppListItem';
import { MappedArray } from '../../Shared/Enumerable';

@singleton()
class MainPage {
    constructor(
        private readonly vm: MainPageViewModel,
        private readonly hub: HubAppApi
    ) {
        this.refreshAllApps();
    }

    readonly alert = new Alert(this.vm.alert);

    private async refreshAllApps() {
        let apps = await this.allApps();
        this.vm.apps(apps);
    }
    
    private async allApps() {
        let apps: AppListItem[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                let appsFromSource = await this.hub.Apps.All();
                apps = new MappedArray(
                    appsFromSource,
                    a => new AppListItem(a)
                )
                .value();
            }
        );
        return apps;
    }
}
startup(MainPageViewModel, MainPage);