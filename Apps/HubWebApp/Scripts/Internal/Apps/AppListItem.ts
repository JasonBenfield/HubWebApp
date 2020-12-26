import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { AppListItemViewModel } from './AppListItemViewModel';

export class AppListItem {
    constructor(
        source: IAppModel,
        hubApi: HubAppApi,
        vm: AppListItemViewModel
    ) {
        vm.appName(source ? source.AppName : '');
        vm.title(source ? source.Title : '');
        vm.type(source ? source.Type.DisplayText : '');
        vm.url(hubApi.Apps.RedirectToApp.getUrl(source.ID).getUrl());
    }
}