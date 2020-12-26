import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';
import { Alert } from '../../Shared/Alert';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { AppListItem } from './AppListItem';
import { MappedArray } from '../../Shared/Enumerable';
import { AppListItemViewModel } from './AppListItemViewModel';

@singleton()
class MainPage {
    constructor(
        private readonly vm: MainPageViewModel,
        private readonly hub: HubAppApi
    ) {
        this.refreshAllApps();
    }

    readonly alert = new Alert(this.vm.alert);
    readonly appAlert = new Alert(this.vm.appAlert);

    private async refreshAllApps() {
        this.appAlert.clear();
        let apps = await this.allApps();
        this.vm.apps(apps);
        if (apps.length === 0) {
            this.appAlert.danger('No apps were found');
        }
    }
    
    private async allApps() {
        let apps: AppListItemViewModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                let appsFromSource = await this.hub.Apps.All();
                apps = new MappedArray(
                    appsFromSource,
                    a => {
                        let vm = new AppListItemViewModel();
                        new AppListItem(a, this.hub, vm);
                        return vm;
                    }
                )
                .value();
            }
        );
        return apps;
    }
}
startup(MainPageViewModel, MainPage);